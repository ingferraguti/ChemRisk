<?php

class AgenteChimico
{
    public int $id;
    public string $nome;
    public string $identificativo;
    public bool $vlep;
    /** @var array<int, array<int, string>> */
    public array $frasiH = [];
    /** @var array<int, int> */
    public array $sostanze = [];
    public bool $altaemissione;
    public string $tipo;

    private float $score = 0.0;

    public function __construct()
    {
        //
    }

    /** @return array<int, string> */
    public function getArrayFrasiH(): array
    {
        if ($this->tipo === 'processo' || $this->tipo === 'miscelaNP') {
            return $this->getAllFrasiHComponenti();
        }

        $frasi = [];
        foreach ($this->frasiH as $f) {
            $frasi[] = $f[0] . ';' . $f[1] . ';' . $f[2];
        }

        return $frasi;
    }

    /** @return array<int, AgenteChimico> */
    public function getComponentiMiscela(): array
    {
        $componenti = [];
        $tutti = DbAgentiChimici::retrieve();

        foreach ($this->sostanze as $s) {
            foreach ($tutti as $agent) {
                if ($agent->id === $s) {
                    $componenti[] = $agent;
                }
            }
        }

        return $componenti;
    }

    /** @return array<int, string> */
    public function getAllFrasiHComponenti(): array
    {
        $componenti = $this->getComponentiMiscela();
        $frasi = [];
        foreach ($componenti as $ac) {
            $frasi = array_merge($frasi, $ac->getArrayFrasiH());
        }

        return $frasi;
    }

    public function getScore(): float
    {
        $this->score = 1.0;

        if ($this->tipo === 'sostanza' || $this->tipo === 'miscelaP') {
            foreach ($this->frasiH as $fraseh) {
                $raw = $fraseh[2] ?? '';
                $parsed = str_replace(',', '.', $raw);
                $f = (float) $parsed;

                if (!is_numeric($parsed)) {
                    $f = 1.0;
                    trigger_error('non riesco a interpretare: ' . $raw, E_USER_WARNING);
                }

                if ($f > $this->score) {
                    $this->score = $f;
                }
            }

            if ($this->vlep) {
                if ($this->score < 3.0) {
                    $this->score = 3.0;
                }
            }

            return $this->score;
        }

        if ($this->tipo === 'miscelaNP') {
            $score = CasiParticolari::misceleNonPericolose($this->getAllFrasiHComponenti());
            $contieneVlep = false;
            $componenti = $this->getComponentiMiscela();
            foreach ($componenti as $ag) {
                if ($ag->vlep === true) {
                    $contieneVlep = true;
                }
            }

            if ($contieneVlep && $score < 2.25) {
                return 2.25;
            }

            return $score;
        }

        if ($this->tipo === 'processo') {
            $value = 0.0;
            foreach ($this->sostanze as $i) {
                $ag = DbAgentiChimici::getById($i);
                if ($ag === null) {
                    trigger_error('agente chimico ' . $i . ' non trovato', E_USER_WARNING);
                    continue;
                }

                if ($this->altaemissione) {
                    $temp = CasiParticolari::processiElevataEmissione($ag->getArrayFrasiH());
                } else {
                    $temp = CasiParticolari::processiBassaEmissione($ag->getArrayFrasiH());
                }

                if ($temp > $value) {
                    $value = $temp;
                }
            }

            return $value;
        }

        trigger_error('Errore nel tipo di agente chimico', E_USER_WARNING);
        return 1.0;
    }

    public function getNome(): string
    {
        return $this->nome;
    }

    public function getId(): int
    {
        return $this->id;
    }
}
