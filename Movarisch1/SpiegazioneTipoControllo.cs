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
    public partial class SpiegazioneTipoControllo : Form
    {
        public SpiegazioneTipoControllo()
        {
            InitializeComponent();
        }

        private void SpiegazioneTipoControllo_Load(object sender, EventArgs e)
        {
            label1.Text = @"
Contenimento completo: 
corrisponde ad una situazione a ciclo chiuso. Dovrebbe, almeno teoricamente, 
rendere trascurabile l’esposizione, ove si escluda il caso di anomalie, incidenti, errori.

Ventilazione - aspirazione locale delle emissioni (LEV): 
questo sistema rimuove il contaminante alla sua sorgente di rilascio, 
impedendone la dispersione nelle aree con presenza umana, dove potrebbe essere inalato.

Segregazione - separazione: 
il lavoratore è separato dalla sorgente di rilascio del contaminante da un 
appropriato spazio di sicurezza, o vi sono adeguati intervalli di tempo fra 
la presenza del contaminante nell’ambiente e la presenza del personale nella stessa area. 
Questa procedura si riferisce soprattutto all’adozione di metodi e comportamenti appropriati, 
controllati in modo adeguato, piuttosto che ad una separazione fisica effettiva 
(come nel caso del contenimento completo). 
Il fattore dominante diviene quindi il comportamento finalizzato alla prevenzione dell’esposizione. 

Diluizione - ventilazione: 
questa può essere naturale o meccanica. Questo metodo è applicabile 
nei casi in cui esso consenta di minimizzare l’esposizione e renderla 
trascurabile in rapporto alla pericolosità intrinseca del fattore di rischio, 
tramite un’adeguata progettazione del ricircolo dell’aria. 
Richiede generalmente un adeguato monitoraggio continuativo.

Manipolazione diretta: 
in questo caso il lavoratore opera a diretto contatto con il materiale pericoloso; 
non essendo possibile l’applicazione delle misure generali di tutela, 
si adottano unicamente dispositivi di protezione individuale. 
Si può assumere che in queste condizioni le esposizioni possano essere anche relativamente elevate.";
       
        }

        private void label2_Click(object sender, EventArgs e)
        {
        
        }
    }
}
