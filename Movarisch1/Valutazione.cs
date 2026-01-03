using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movarisch1
{
    [Serializable]
    public class Valutazione
    {
        //dati generali
        public int id;
        public int idLavoratore;
        public String nomeFileOriginale;
        public DateTime data;
        
        //Agente chimico
        public AgenteChimico ac;

        //Esposizione Inalatoria
        public float einal{ get; set; }
        int statoFisicoInal;
        public String statoFisicoInalHR;
        public float quantita;
        public int tipoUsoInal;
        public String tipoUsoInalHR;
        public int tipoControllo{ get; set; }
        public String tipoControlloHR;
        public int tempoInal{ get; set; }
        public float distanza;

        //esposizione cutanea
        public float ecute;
        public Boolean esposizioneCutanea;
        //public int tipoUsoCut;
        //public String tipoUsoCutHR;
        public int livelliContattoCutaneo;
        public String livelliContattoCutaneoHR;

        //processi
        int tipoControlloProc{ get; set; }
        float quantitaProc{ get; set; }
        int tempoProc{ get; set; }
        

        //esito
        public float rInal { get; set; }
        public float rCute { get; set; }

        private float rischio;

        public Valutazione (int id, int l) {
            this.id = id;
            this.idLavoratore = l;
            this.data = DateTime.Now;
        }

        public float getRisch() {

            float score = this.ac.getScore();
            this.rInal = score * this.einal;
            this.rCute = score * this.ecute;
            
            this.rischio = (float) Math.Sqrt((Math.Pow(this.rInal,2))+(Math.Pow(this.rCute,2)));
            return this.rischio = (float) Math.Round(this.rischio, 2, MidpointRounding.AwayFromZero);

        }

        public String getFraseValutazione() {
            float risch = this.getRisch();
            if (risch < 15) return @"<br /><b>Rischio <i>irrilevante per la salute</i></b><br />";
            if (risch < 21) return @"<br /><b>Intervallo di incertezza;<br />è necessario, prima della classificazione in <i>irrilevante per la salute</i>, rivedere con scrupolo l'assegnazione dei vari punteggi, rivedere le misure di prevenzione e protezione adottate, e <i>consultare il medico competente per la decisione finale.</i></b><br />";
            if (risch < 41) return @"<br /><b>Rischio superiore al <i>rischio chimico irrilevante per la salute</i>. Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08</b><br />";
            if (risch < 80) return @"<br /><b>Rischio superiore al <i>rischio chimico irrilevante per la salute</i>. Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08</b><br /><br /><b>Zona di rischio elevato</b><br />";
            return @"<br /><b>Rischio superiore al <i>rischio chimico irrilevante per la salute</i>. Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08</b><br /><br /><b>Zona di grave rischio. Riconsiderare il percorso dell'identificazione delle misure di prevenzione e pre+otezione ai fini di una loro eventuale implementazione.<br /> Intensificare i controlli quali la sorveglianza sanitaria, la misurazione degli agenti chimici e la periodicità della manutenzione</b><br />";
        }

        public String getTitoloValutazione()
        {
            float risch = this.getRisch();
            if (risch < 15) return @"Rischio irrilevante per la salute";
            if (risch < 21) return @"Intervallo di incertezza";
            if (risch < 41) return @"Rischio superiore al rischio chimico irrilevante per la salute.";
            if (risch < 80) return @"Zona di rischio elevato";
            return @"Zona di grave rischio";
        }

        public String getDescrizioneValutazione()
        {
            float risch = this.getRisch();
            if (risch < 15) return @"";
            if (risch < 21) return "è necessario, prima della classificazione in irrilevante per la salute, \r\n rivedere con scrupolo l'assegnazione dei vari punteggi, \r\n rivedere le misure di prevenzione e protezione adottate, \r\n e consultare il medico competente per la decisione finale.";
            if (risch < 41) return @"Applicare gli articoli 225, 226, 229, e 230 D.Lgs. 81/08";
            if (risch < 80) return @"";
            return "Riconsiderare il percorso dell'identificazione delle misure di prevenzione\r\n e protezione ai fini di una loro eventuale implementazione. \r\nIntensificare i controlli quali la sorveglianza sanitaria, \r\nla misurazione degli agenti chimici e la periodicità della manutenzione";
        }




        public string Nome
        {
            get
            {
                return this.ac.nome;
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
