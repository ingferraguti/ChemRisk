<?php

class Azienda
{
    public string $denominazione;
    public string $indirizzo;
    public string $cap;
    public string $comune;
    public string $provincia;
    public string $contatto;
    public string $email;
    public string $piva;

    public function __construct(
        string $denominazione,
        string $indirizzo,
        string $cap,
        string $comune,
        string $provincia,
        string $contatto,
        string $email,
        string $piva
    ) {
        $this->denominazione = $denominazione;
        $this->indirizzo = $indirizzo;
        $this->cap = $cap;
        $this->comune = $comune;
        $this->provincia = $provincia;
        $this->contatto = $contatto;
        $this->email = $email;
        $this->piva = $piva;
    }
}
