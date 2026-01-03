<?php

class ControlloProcessoEsposizioneInalatoria
{
    public static function calcolaEinal(float $qtaInUso, int $tipologiaControllo, float $tempo, float $distanza): float
    {
        if ($tipologiaControllo === -1) {
            throw new InvalidArgumentException('Tipologia di controllo non definita.');
        }

        $dieci = 10.0;
        $cento = 100.0;
        $c = 0;

        if ($qtaInUso < $dieci) {
            if ($tipologiaControllo === 0 || $tipologiaControllo === 1 || $tipologiaControllo === 2) {
                $c = 1;
            } elseif ($tipologiaControllo === 3) {
                $c = 2;
            }
        } elseif ($qtaInUso >= 10 || $qtaInUso <= 100) {
            if ($tipologiaControllo === 0) {
                $c = 1;
            } elseif ($tipologiaControllo === 1 || $tipologiaControllo === 2) {
                $c = 2;
            } elseif ($tipologiaControllo === 3) {
                $c = 3;
            }
        } elseif ($qtaInUso > 100) {
            if ($tipologiaControllo === 0) {
                $c = 1;
            } elseif ($tipologiaControllo === 1) {
                $c = 2;
            } elseif ($tipologiaControllo === 2 || $tipologiaControllo === 3) {
                $c = 3;
            }
        }

        $matrice2Bis = -1;
        $quindici = 15.0;
        $centoventi = 120.0;
        $trecentosessanta = 360.0;

        if ($c === 1) {
            if ($tempo < $centoventi) {
                $matrice2Bis = 1;
            } elseif ($tempo < $trecentosessanta) {
                $matrice2Bis = 3;
            } else {
                $matrice2Bis = 7;
            }
        } elseif ($c === 2) {
            if ($tempo < $quindici) {
                $matrice2Bis = 1;
            } elseif ($tempo < $centoventi) {
                $matrice2Bis = 3;
            } elseif ($tempo < $trecentosessanta) {
                $matrice2Bis = 7;
            } else {
                $matrice2Bis = 10;
            }
        } elseif ($c === 3) {
            if ($tempo < $quindici) {
                $matrice2Bis = 3;
            } elseif ($tempo < $centoventi) {
                $matrice2Bis = 7;
            } else {
                $matrice2Bis = 10;
            }
        }

        $uno = 1.0;
        $tre = 3.0;
        $cinque = 5.0;

        if ($distanza < $uno) {
            return 1.0 * $matrice2Bis;
        }

        if ($distanza < $tre) {
            return 0.75 * $matrice2Bis;
        }

        if ($distanza < $cinque) {
            return 0.5 * $matrice2Bis;
        }

        if ($distanza < $dieci) {
            return 0.25 * $matrice2Bis;
        }

        if ($distanza >= $dieci) {
            return 0.1 * $matrice2Bis;
        }

        throw new RuntimeException('Errore nel calcolo della distanza.');
    }
}
