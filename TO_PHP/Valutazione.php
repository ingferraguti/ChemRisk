<?php

class Valutazione
{
    // dati generali
    public int $id;
    public int $idLavoratore;
    public string $nomeFileOriginale = '';
    public DateTimeImmutable $data;

    // Agente chimico
    public ?AgenteChimico $ac = null;

    // Esposizione Inalatoria
    public float $einal = 0.0;
    private int $statoFisicoInal = 0;
    public string $statoFisicoInalHR = '';
    public float $quantita = 0.0;
    public int $tipoUsoInal = 0;
    public string $tipoUsoInalHR = '';
    public int $tipoControllo = 0;
    public string $tipoControlloHR = '';
    public int $tempoInal = 0;
    public float $distanza = 0.0;

    // esposizione cutanea
    public float $ecute = 0.0;
    public bool $esposizioneCutanea = false;
    public int $livelliContattoCutaneo = 0;
    public string $livelliContattoCutaneoHR = '';

    // processi
    private int $tipoControlloProc = 0;
    private float $quantitaProc = 0.0;
    private int $tempoProc = 0;

    // esito
    public float $rInal = 0.0;
    public float $rCute = 0.0;

    private float $rischio = 0.0;

    public function __construct(int $id, int $l)
    {
        $this->id = $id;
        $this->idLavoratore = $l;
        $this->data = new DateTimeImmutable();
    }

    public function getRisch(): float
    {
        if ($this->ac === null) {
            trigger_error('Agente chimico non impostato', E_USER_WARNING);
            return 0.0;
        }

        $score = $this->ac->getScore();
        $this->rInal = $score * $this->einal;
        $this->rCute = $score * $this->ecute;

        $this->rischio = sqrt(pow($this->rInal, 2) + pow($this->rCute, 2));
        $this->rischio = round($this->rischio, 2, PHP_ROUND_HALF_UP);

        return $this->rischio;
    }

    public function getFraseValutazione(): string
    {
        $risch = $this->getRisch();
        if ($risch < 15) {
            return "<br /><b>Rischio <i>irrilevante per la salute</i></b><br />";
        }
        if ($risch < 21) {
            return "<br /><b>Intervallo di incertezza;<br />è necessario, prima della classificazione in <i>irrilevante per la salute</i>, rivedere con scrupolo l'assegnazione dei vari punteggi, rivedere le misure di prevenzione e protezione adottate, e <i>consultare il medico competente per la decisione finale.</i></b><br />";
        }
        if ($risch < 41) {
            return "<br /><b>Rischio superiore al <i>rischio chimico irrilevante per la salute</i>. Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08</b><br />";
        }
        if ($risch < 80) {
            return "<br /><b>Rischio superiore al <i>rischio chimico irrilevante per la salute</i>. Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08</b><br /><br /><b>Zona di rischio elevato</b><br />";
        }

        return "<br /><b>Rischio superiore al <i>rischio chimico irrilevante per la salute</i>. Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08</b><br /><br /><b>Zona di grave rischio. Riconsiderare il percorso dell'identificazione delle misure di prevenzione e pre+otezione ai fini di una loro eventuale implementazione.<br /> Intensificare i controlli quali la sorveglianza sanitaria, la misurazione degli agenti chimici e la periodicità della manutenzione</b><br />";
    }

    public function getTitoloValutazione(): string
    {
        $risch = $this->getRisch();
        if ($risch < 15) {
            return 'Rischio irrilevante per la salute';
        }
        if ($risch < 21) {
            return 'Intervallo di incertezza';
        }
        if ($risch < 41) {
            return 'Rischio superiore al rischio chimico irrilevante per la salute.';
        }
        if ($risch < 80) {
            return 'Zona di rischio elevato';
        }

        return 'Zona di grave rischio';
    }

    public function getDescrizioneValutazione(): string
    {
        $risch = $this->getRisch();
        if ($risch < 15) {
            return '';
        }
        if ($risch < 21) {
            return "è necessario, prima della classificazione in irrilevante per la salute, \r\n rivedere con scrupolo l'assegnazione dei vari punteggi, \r\n rivedere le misure di prevenzione e protezione adottate, \r\n e consultare il medico competente per la decisione finale.";
        }
        if ($risch < 41) {
            return 'Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08';
        }
        if ($risch < 80) {
            return '';
        }

        return "Riconsiderare il percorso dell'identificazione delle misure di prevenzione\r\n e protezione ai fini di una loro eventuale implementazione. \r\nIntensificare i controlli quali la sorveglianza sanitaria, \r\nla misurazione degli agenti chimici e la periodicità della manutenzione";
    }

    public function getNome(): string
    {
        return $this->ac?->nome ?? '';
    }

    public function getId(): int
    {
        return $this->id;
    }
}
