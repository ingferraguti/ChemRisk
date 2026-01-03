using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Movarisch1
{
    public partial class Copyright : Form
    {
        public Copyright()
        {
            InitializeComponent();
        }

        private void Copyright_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = @" L'utilizzo del presente software a scopi personali o di lucro è gratuito

e non richiede l'acquisto di licenze a pagamento, tuttavia tutti i

diritti sono riservati all'Azienda Usl di Modena come, in parte, qui di

seguito esplicitato:

 

E’ diritto esclusivo dell’Azienda USL di Modena di diffondere e

mettere a disposizione del pubblico la sua opera con l'utilizzo dei

mezzi di diffusione a distanza (art. 16 L.d.A.), la diffusione

dell'opera tutelate nell’Internet potrà dunque essere legittimamente

effettuata solo con l’autorizzazione dell’autore.

Il diritto di comunicazione al pubblico non si esaurisce con nessuno

degli atti sopra indicati (art. 16, comma 2 L.d.A.). Per ogni diffusione

dell’opera è, quindi, necessaria la relativa autorizzazione degli autori

e di tutti coloro che hanno partecipato alla realizzazione dell’opera.

 

Qualsiasi modificazione o trasformazione di un’opera, nel senso di

apportare alla stessa cambiamenti che, pur lasciando inalterato il senso

originale, ne cambino la struttura o la forma, deve essere autorizzata.

L’Azienda USL di Modena ha il diritto di opporsi a qualsiasi

deformazione o modifica dell’opera che possa danneggiare la sua

reputazione; questo diritto tutela non solo le modifiche dell'opera ma

anche qualsiasi modalità di comunicazione che ne possa cambiare la

percezione e il giudizio da parte del pubblico, come ad esempio citare

l'opera o l'Azienda USL di Modena per la promozione e pubblicità di

prodotti e servizi.";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
