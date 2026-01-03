<?php

class CasiParticolari
{
    public static function processiElevataEmissione(array $frasih): float
    {
        // ______________________________
        // _PROCESSI______________________
        // ____ELEVATA EMISSIONE ________

        $elevataUno = [
            "H330 cat.2",
            "H330 cat.1",
            "H334 cat.1A",
            "H334 cat.1B",
            "H370",
            "H371",
            "H372",
            "H373",
            "H341",
            "H351",
            "H361",
            "H361f",
            "H361d",
            "H361fd",
            "EUH207",
            "EUH071",
            "EUH204",
            "EUH380",
            "EUH381",
        ];
        $elevataUnoScore = 5.0;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataUno as $a) {
                if ($code === $a) {
                    return $elevataUnoScore;
                }
            }
        }

        // ____________________________

        $elevataDue = [
            "H332",
            "H331",
            "H317 cat.1A",
            "H317 cat.1B",
            "H362",
            "EUH201",
            "EUH201A",
            "EUH203",
            "EUH205",
            "EUH212",
            "H304",
            "EUH211",
        ];
        $elevataDueScore = 3.0;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataDue as $a) {
                if ($code === $a) {
                    return $elevataDueScore;
                }
            }
        }

        // ____________________________

        $elevataQuattro = ["H310 cat.1"];
        $elevataQuattroScore = 3.0;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataQuattro as $a) {
                if ($code === $a) {
                    return $elevataQuattroScore;
                }
            }
        }

        // ____________________________

        $elevataTre = [
            "H336",
            "H335",
            "EUH206",
            "EUH029",
            "EUH031",
            "EUH032",
        ];
        $elevataTreScore = 2.25;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataTre as $a) {
                if ($code === $a) {
                    return $elevataTreScore;
                }
            }
        }

        // ____________________________

        $elevataCinque = [
            "H311",
            "H310 cat.2",
            "H314 cat.1A",
            "H314 cat.1B",
            "H314 cat.1C",
            "H318",
            "EUH070",
            "EUH202",
        ];
        $elevataCinqueScore = 2.25;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataCinque as $a) {
                if ($code === $a) {
                    return $elevataCinqueScore;
                }
            }
        }

        // ____________________________

        $elevataSei = [
            "H312",
            "H300 cat.1",
            "H319",
        ];
        $elevataSeiScore = 2.0;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataSei as $a) {
                if ($code === $a) {
                    return $elevataSeiScore;
                }
            }
        }

        // ____________________________

        $elevataSette = [
            "H302",
            "H301",
            "H315",
            "H300 cat.2",
            "EUH066",
        ];
        $elevataSetteScore = 1.75;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($elevataSette as $a) {
                if ($code === $a) {
                    return $elevataSetteScore;
                }
            }
        }

        // se non è nessuno di questi casi?
        return 1.0;
    }

    // _______Bassa EMISSIONE _______________________________________________________
    public static function processiBassaEmissione(array $frasih): float
    {
        $scoreUno = 2.50;
        $bassaUno = [
            "H330 cat.2",
            "H330 cat.1",
            "H334 cat.1A",
            "H334 cat.1B",
            "H370",
            "H371",
            "H372",
            "H373",
            "H341",
            "H351",
            "H361",
            "H361f",
            "H361d",
            "H361fd",
            "EUH207",
            "EUH071",
            "EUH204",
            "EUH380",
            "EUH381",
        ];

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($bassaUno as $a) {
                if ($code === $a) {
                    return $scoreUno;
                }
            }
        }

        $scoreDue = 2.0;
        $bassaDue = [
            "H332",
            "H331",
            "H317 cat.1A",
            "H317 cat.1B",
            "H362",
            "EUH201",
            "EUH201A",
            "EUH203",
            "EUH205",
            "H304",
            "EUH211",
            "EUH212",
        ];
        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($bassaDue as $a) {
                if ($code === $a) {
                    return $scoreDue;
                }
            }
        }

        $scoreTre = 1.75;
        $bassaTre = [
            "H336",
            "H335",
            "EUH206",
            "EUH029",
            "EUH031",
            "EUH032",
        ];
        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($bassaTre as $a) {
                if ($code === $a) {
                    return $scoreTre;
                }
            }
        }

        $scoreQuattro = 1.25;
        $bassaQuattro = [
            "H310 cat.1",
            "H311",
            "H310 cat.2",
            "H314 cat.1A",
            "H314 cat.1B",
            "H314 cat.1C",
            "H318",
            "EUH070",
            "EUH202",
            "H312",
            "H300 cat.1",
            "H319",
            "H302",
            "H301",
            "H300 cat.2",
            "H315",
            "EUH066",
        ];
        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($bassaQuattro as $a) {
                if ($code === $a) {
                    return $scoreQuattro;
                }
            }
        }

        return 1.0;
    }

    public static function misceleNonPericolose(array $frasih): float
    {
        $uno = [
            "EUH207",
            "H330 cat.1",
            "H334 cat.1A",
            "H334 cat.1B",
            "H370",
            "H371",
            "H372",
            "H341",
            "H351",
            "H361",
            "H361fd",
            "EUH380",
            "EUH381",
        ];
        $unoScore = 5.50;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($uno as $a) {
                if ($code === $a) {
                    return $unoScore;
                }
            }
        }

        $due = [
            "H331",
            "H330 cat.2",
            "H373",
            "H304",
            "H361d",
            "H361f",
            "H362",
            "EUH071",
            "H317 cat.1A",
            "EUH204",
            "EUH212",
            "EUH203",
            "EUH205",
            "EUH201",
            "EUH201A",
            "EUH202",
            "H317 cat.1B",
            "EUH211",
        ];
        $dueScore = 4.0;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($due as $a) {
                if ($code === $a) {
                    return $dueScore;
                }
            }
        }

        $tre = [
            "H332",
            "H336",
            "H335",
            "EUH206",
            "EUH029",
            "EUH031",
            "EUH032",
        ];
        $treScore = 2.50;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($tre as $a) {
                if ($code === $a) {
                    return $treScore;
                }
            }
        }

        $quattro = [
            "H312",
            "H311",
            "H310 cat.2",
            "EUH070",
            "H310 cat.1",
            "H300 cat.1",
            "H314 cat.1A",
            "H314 cat.1B",
            "H314 cat.1C",
            "H318",
            "H319",
        ];
        $quattroScore = 2.25;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($quattro as $a) {
                if ($code === $a) {
                    return $quattroScore;
                }
            }
        }

        $cinque = [
            "H302",
            "H301",
            "H300 cat.2",
            "H315",
            "EUH066",
        ];
        $cinqueScore = 1.75;

        foreach ($frasih as $h) {
            $code = explode(';', $h)[0];
            foreach ($cinque as $a) {
                if ($code === $a) {
                    return $cinqueScore;
                }
            }
        }

        return 1.0;
    }
}
