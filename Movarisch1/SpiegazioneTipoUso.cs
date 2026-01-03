using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movarisch1
{
    public partial class SpiegazioneTipoUso : Form
    {
        public SpiegazioneTipoUso()
        {
            InitializeComponent();
            //webBrowser1.
        }

        private void SpiegazioneTipoUso_Load(object sender, EventArgs e)
        {
            label1.Text = @"
Uso in sistema chiuso:

la sostanza/miscela è usata e/o conservata in reattori o contenitori a tenuta stagna 
e trasferita da un contenitore all’altro attraverso tubazioni stagne. 
Questa categoria non può essere applicata a situazioni in cui, 
in una qualsiasi sezione del processo produttivo, 
possano aversi rilasci nell’ambiente. 
In altre parole il sistema chiuso deve essere tale in tutte le sue parti.


Uso in inclusione in matrice:

la sostanza/miscela viene incorporata in materiali o prodotti da cui è impedita 
o limitata la dispersione nell’ambiente.  
Questa categoria include l’uso di materiali in “pellet”, 
la dispersione di solidi in un fluido non pericoloso con limitazione del rilascio 
di polveri e in genere l’inglobamento della sostanza/miscela in esame in matrici 
che tendano a trattenerla.


Uso controllato e non dispersivo:

questa categoria include le lavorazioni in cui sono coinvolti solo limitati gruppi 
selezionati di lavoratori, adeguatamente esperti dello specifico processo, e in cui 
sono disponibili sistemi di controllo adeguati a controllare e contenere l’esposizione.


Uso con dispersione significativa: 

questa categoria include lavorazioni ed attività che possono comportare 
un’esposizione sostanzialmente incontrollata non solo degli addetti, 
ma anche di altri lavoratori ed eventualmente della popolazione generale.  
Possono essere classificati in questa categoria processi come l’irrorazione 
di prodotti fitosanitari, l’uso di vernici ed altre analoghe attività svolte all’esterno.";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
