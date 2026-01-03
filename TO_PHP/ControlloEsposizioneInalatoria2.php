<?php

class ControlloEsposizioneInalatoria2
{
    public int $matrice2 = -2;
    public int $matrice3 = -1;
    public int $matrice4 = -1;

    public function __construct(int $matrice2)
    {
        $this->matrice2 = $matrice2;
    }

    public function getLabelTempoEsposizione(): string
    {
        return "Per tempo di esposizione si intende\n"
            . "il tempo (espresso in minuti) durante il quale\n"
            . "il lavoratore si trova esposto all’agente\n"
            . "chimico pericoloso su base giornaliera";
    }

    public function getLabelDistanzaSorgente(): string
    {
        return "Per distanza degli esposti dalla sorgente\n"
            . "si intende la distanza, espressa in metri,\n"
            . "tra la posizione del lavoratore ed\n"
            . "il punto di rilascio (sorgente)";
    }

    public function calcola(
        int $tipocontrollo,
        string $tipoCont,
        float $distanza,
        float $tempo
    ): ?array {
        if ($tipocontrollo === -1) {
            trigger_error('Prima di proseguire specifica la tipologia di controllo', E_USER_WARNING);
            return null;
        }

        $this->matrice3 = $this->calcolaMatrice3($this->matrice2, $tipocontrollo);
        $this->matrice4 = $this->calcolaMatrice4($this->matrice3, $tempo);
        $einal = $this->calcolaEinal($this->matrice4, $distanza);

        return [
            'matrice3' => $this->matrice3,
            'matrice4' => $this->matrice4,
            'distanza' => $distanza,
            'tempo' => $tempo,
            'tipoControllo' => $tipocontrollo,
            'tipoCont' => $tipoCont,
            'einal' => $einal,
        ];
    }

    private function calcolaMatrice3(int $matrice2, int $tipocontrollo): int
    {
        if ($matrice2 === 1) {
            if ($tipocontrollo === 3 || $tipocontrollo === 4) {
                return 2;
            }

            return 1;
        }

        if ($matrice2 === 2) {
            if ($tipocontrollo === 0) {
                return 1;
            }

            if ($tipocontrollo === 1 || $tipocontrollo === 2) {
                return 2;
            }

            return 3;
        }

        if ($matrice2 === 3) {
            if ($tipocontrollo === 0) {
                return 1;
            }

            if ($tipocontrollo === 1) {
                return 2;
            }

            return 3;
        }

        trigger_error('ERRORE di calcolo nel modello alla matrice 3', E_USER_WARNING);
        return -1;
    }

    private function calcolaMatrice4(int $matrice3, float $tempo): int
    {
        $quindici = 16.0;
        $centoventi = 121.0;
        $trecentosessanta = 361.0;

        if ($matrice3 === 1) {
            if ($tempo < $centoventi) {
                return 1;
            }

            if ($tempo < $trecentosessanta) {
                return 3;
            }

            return 7;
        }

        if ($matrice3 === 2) {
            if ($tempo < $quindici) {
                return 1;
            }

            if ($tempo < $centoventi) {
                return 3;
            }

            if ($tempo < $trecentosessanta) {
                return 7;
            }

            return 10;
        }

        if ($matrice3 === 3) {
            if ($tempo < $quindici) {
                return 3;
            }

            if ($tempo < $centoventi) {
                return 7;
            }

            return 10;
        }

        trigger_error(
            'ERRORE di calcolo nel modello alla matrice 4, matrice 3:'
            . $matrice3
            . ' matrice4:'
            . $this->matrice4,
            E_USER_WARNING
        );
        return -1;
    }

    private function calcolaEinal(int $matrice4, float $distanza): float
    {
        $uno = 1.0;
        $tre = 3.0;
        $cinque = 5.0;
        $dieci = 10.0;

        if ($distanza <= $uno) {
            return 1.0 * $matrice4;
        }

        if ($distanza <= $tre) {
            return 0.75 * $matrice4;
        }

        if ($distanza <= $cinque) {
            return 0.5 * $matrice4;
        }

        if ($distanza <= $dieci) {
            return 0.25 * $matrice4;
        }

        if ($distanza >= $dieci) {
            return 0.1 * $matrice4;
        }

        trigger_error('Errore nella distanza, controllare il valore inserito', E_USER_WARNING);
        return 1.0;
    }
}
