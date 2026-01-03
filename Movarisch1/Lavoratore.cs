using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movarisch1
{
    public class Lavoratore
    {
        public int id;
        public String nome;
        public String cognome;
        public int idazienda;
        public int area;
        public String note;

        public Lavoratore(int id, int azienda, int area, String nome, String cognome, String note)
        {
            this.id = id;
            this.idazienda = azienda;
            this.area = area;
            this.nome = nome;
            this.cognome = cognome;
            this.note = note;
        }

    }
}
