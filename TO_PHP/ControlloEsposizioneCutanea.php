<?php

class ControlloEsposizioneCutanea
{
    private $father;
    private int $tipoUso = 0;
    private bool $noExposure = false;

    public function __construct($father = null)
    {
        $this->father = $father;
    }

    public function setTipoUso(int $tipoUso): void
    {
        $this->tipoUso = $tipoUso;
    }

    public function setNoExposure(bool $noExposure): void
    {
        $this->noExposure = $noExposure;
    }

    public function getVal(int $tipo, int $livello): int
    {
        if ($this->noExposure) {
            return 0;
        }

        if ($livello === 0 || ($livello === 1 && $tipo === 0)) {
            return 1;
        }

        if (
            ($livello === 1 && $tipo === 1)
            || ($livello === 1 && $tipo === 2)
            || ($livello === 2 && $tipo === 0)
            || ($livello === 2 && $tipo === 1)
        ) {
            return 3;
        }

        if (
            ($livello === 1 && $tipo === 3)
            || ($livello === 2 && $tipo === 2)
            || ($livello === 2 && $tipo === 3)
            || ($livello === 3 && $tipo === 0)
            || ($livello === 3 && $tipo === 1)
        ) {
            return 7;
        }

        if (
            ($livello === 3 && $tipo === 2)
            || ($livello === 3 && $tipo === 3)
        ) {
            return 10;
        }

        return -1;
    }

    /**
     * @return array{contatto:int, contattoHR:string, tipoUso:int, esposizioneCutanea:bool, valore:int}
     */
    public function valuta(int $contatto, string $contattoHR): array
    {
        if ($contatto === -1 && !$this->noExposure) {
            throw new InvalidArgumentException('Non hai complato la scheda per la valutazione del rischio cutaneo');
        }

        $valore = $this->getVal($this->tipoUso, $contatto);

        return [
            'contatto' => $contatto,
            'contattoHR' => $contattoHR,
            'tipoUso' => $this->tipoUso,
            'esposizioneCutanea' => !$this->noExposure,
            'valore' => $valore,
        ];
    }

    public function getDettaglioEvento(string $radioKey): string
    {
        if ($radioKey === 'radioButton6') {
            return "Non più di un evento al giorno,\n dovuto a spruzzi o rilasci \n occasionali (come per esempio \n nella preparazione \n di una vernice).";
        }

        if ($radioKey === 'radioButton7') {
            return "Da 2 a 10 eventi al giorno,\n dovute alle caratteristiche\n proprie del processo.";
        }

        if ($radioKey === 'radioButton8') {
            return "Il numero di eventi \n giornalieri è superiore a 10";
        }

        return '';
    }
}
