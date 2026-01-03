using System;

public class Azienda
{
    public String denominazione;
    public String indirizzo;
    public String cap;
    public String comune;
    public String provincia;
    public String contatto;
    public String email;
    public String piva;

	public Azienda(
        String denominazione, 
        String indirizzo,
        String cap,
        String comune,
        String provincia,
        String contatto,
        String email,
        String piva)
	{
        this.denominazione = denominazione;
        this.indirizzo = indirizzo;
        this.cap = cap;
        this.comune = comune;
        this.provincia = provincia;
        this.contatto = contatto;
        this.email = email;
        this.piva = piva;
	}
}
