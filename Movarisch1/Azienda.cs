using System;

namespace Movarisch1
{
    public class Azienda
    {
        public int id;
        public String denominazione;
        public String indirizzo;
        public String cap;
        public String comune;
        public String provincia;
        public String contatto;
        public String telefono;
        public String email;
        public String piva;

        public Azienda(
            int id,
            String denominazione,
            String indirizzo,
            String cap,
            String comune,
            String provincia,
            String contatto,
            String telefono,
            String email,
            String piva)
        {
            this.id = id;
            this.denominazione = denominazione;
            this.indirizzo = indirizzo;
            this.cap = cap;
            this.comune = comune;
            this.provincia = provincia;
            this.contatto = contatto;
            this.telefono = telefono;
            this.email = email;
            this.piva = piva;
        }

        public String getDenominazione() {
            return this.denominazione;
        }


        public string Nome
        {
            get
            {
                return this.denominazione;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
        }
        
       
        
    }
}