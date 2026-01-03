using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movarisch1
{
    public class Area
    {
        //parametri
        public int id;
        public int azienda;
        public String nome;
        public String note;

        //costruttore
        public Area(
            int id,
            int az,
            String nom,
            String not)
        {
            this.id = id;
            this.nome = nom;
            this.note = not;
            this.azienda = az;
        }

    }
}
