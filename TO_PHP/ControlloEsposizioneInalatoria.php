<?php

class ControlloEsposizioneInalatoria
{
    public const STATO_SOLIDO = 0;
    public const STATO_LIQUIDO_BASSA_VOLATILITA = 1;
    public const STATO_LIQUIDO_ALTA_VOLATILITA = 2;
    public const STATO_GASSOSO = 3;
    public const STATO_NEBBIE = 4;
    public const STATO_LIQUIDO_MEDIA_VOLATILITA = 5;
    public const STATO_POLVERI_FINI = 6;

    public const USO_SISTEMA_CHIUSO = 0;
    public const USO_INCLUSIONE_IN_MATRICE = 1;
    public const USO_CONTROLLATO_NON_DISPERSIVO = 2;
    public const USO_CON_DISPERSIONE_SIGNIFICATIVA = 3;

    public static function calcola(int $statoFisicoTabIndex, int $tipologiaUsoTabIndex, float $chilogrammi): array
    {
        if ($statoFisicoTabIndex < 0 || $tipologiaUsoTabIndex < 0) {
            throw new InvalidArgumentException('Compila tutti i campi prima di proseguire');
        }

        $statoFisico = self::normalizzaStatoFisico($statoFisicoTabIndex);
        $matriceUno = self::calcolaMatriceUno($statoFisico, $chilogrammi);
        $matriceDue = self::calcolaMatriceDue($matriceUno, $tipologiaUsoTabIndex);

        return [
            'matrice_uno' => $matriceUno,
            'matrice_due' => $matriceDue,
        ];
    }

    private static function normalizzaStatoFisico(int $statoFisico): int
    {
        if ($statoFisico === self::STATO_NEBBIE) {
            return self::STATO_SOLIDO;
        }

        if ($statoFisico === self::STATO_LIQUIDO_MEDIA_VOLATILITA || $statoFisico === self::STATO_POLVERI_FINI) {
            return self::STATO_LIQUIDO_ALTA_VOLATILITA;
        }

        return $statoFisico;
    }

    private static function calcolaMatriceUno(int $statoFisico, float $chilogrammi): int
    {
        if ($chilogrammi <= 0.1) {
            if ($statoFisico === self::STATO_GASSOSO) {
                return 2;
            }

            return 1;
        }

        if ($chilogrammi <= 1.0) {
            if ($statoFisico === self::STATO_SOLIDO) {
                return 1;
            }

            if ($statoFisico === self::STATO_LIQUIDO_BASSA_VOLATILITA) {
                return 2;
            }

            return 3;
        }

        if ($chilogrammi <= 10.0) {
            if ($statoFisico === self::STATO_SOLIDO) {
                return 1;
            }

            if ($statoFisico === self::STATO_GASSOSO) {
                return 4;
            }

            return 3;
        }

        if ($chilogrammi <= 100.0) {
            if ($statoFisico === self::STATO_SOLIDO) {
                return 2;
            }

            if ($statoFisico === self::STATO_LIQUIDO_BASSA_VOLATILITA) {
                return 3;
            }

            return 4;
        }

        if ($statoFisico === self::STATO_SOLIDO) {
            return 2;
        }

        return 4;
    }

    private static function calcolaMatriceDue(int $matriceUno, int $tipologiaUso): int
    {
        if ($matriceUno === 1) {
            if ($tipologiaUso === self::USO_CON_DISPERSIONE_SIGNIFICATIVA) {
                return 2;
            }

            return 1;
        }

        if ($matriceUno === 2) {
            if ($tipologiaUso === self::USO_SISTEMA_CHIUSO) {
                return 1;
            }

            if ($tipologiaUso === self::USO_CON_DISPERSIONE_SIGNIFICATIVA) {
                return 3;
            }

            return 2;
        }

        if ($matriceUno === 3) {
            if ($tipologiaUso === self::USO_SISTEMA_CHIUSO) {
                return 1;
            }

            if ($tipologiaUso === self::USO_INCLUSIONE_IN_MATRICE) {
                return 2;
            }

            return 3;
        }

        if ($tipologiaUso === self::USO_SISTEMA_CHIUSO) {
            return 2;
        }

        return 3;
    }
}
