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

    public partial class Modello : Form
    {
        public event System.Windows.Forms.LinkClickedEventHandler LinkClicked;

        public Modello()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Link_Clicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", e.LinkText);
        }


        private void Modello_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = @"
L MODELLO DI VALUTAZIONE DEL RISCHIO DA AGENTI CHIMICI PERICOLOSI PER LA SALUTE AD USO DELLE PICCOLE E MEDIE IMPRESE prodotto dalle Regioni Emilia-Romagna, Lombardia e Toscana contiene le modalità applicative per l'applicazione dell’art.223 comma 1 del Titolo IX Capo I del D.Lgs.81/08.

Il Modello in parola è conforme alle Linee Guida delle Regioni e delle province autonome per l’applicazione del D.Lgs.25/2002 e s.m.i. ed è conforme al recente D.Lgs.39/2016.

In passato la fornitura gratuita ed incondizionata del software costruito dal modello e liberamente scaricabile da rete ha creato applicazioni non conformi del Titolo VII-bis D.Lgs.626/94 in tutta Italia.

Dal momento in cui si è verificato questa criticità la Regione Emilia-Romagna ha richiesto che il software da esso costruito potesse essere fornito solo a chi partecipasse ad un corso specifico per apprendere le modalità corrette per l’applicazione del processo valutazione del rischio da agenti chimici pericolosi per la salute.

A partire dal 2008, dopo la formazione offerta per gli obiettivi di uso conforme del modello e dopo la creazione dei presupposti per la formazione degli RSPP e ASPP dedicati alla valutazione del rischio chimico sono stati formati circa 2000 discenti fra consulenti, RSPP, ASPP, Medici competenti e professionisti in materia di salute e sicurezza nei luoghi di lavoro. 
Questi dopo la partecipazione al corso e dopo una valutazione di apprendimento hanno dimostrato di avere appreso e conpreso le modalità applicative del modello in conformità alle Linee guida proposte dal Coordinamento Tecnico per la prevenzione nei luoghi di lavoro delle REGIONI e delle Province Autonome. 

Dalla validazione del modello predisposta dalla Regione Toscana pubblicata nel 2008, l'Azienda USL di Modena ha organizzato numerosi corsi,  anche di natura propedeutica, che  produrrà, su richiesta, circa ogni mese anche nel futuro, soprattutto per chiarire gli aspetti connessi all'applicazione del Regolamento CLP nei luoghi di lavoro. 

Vi è inoltre da sottolineare che il suddetto corso di formazione, nonostante non sia organizzato per fornire software, concede la possibilità di sperimentare l’applicativo alla luce delle attuali indicazioni normative del D.Lgs.39/2016 ed inoltre viene fornita ampia e necessaria documentazione di studio per l’applicazione corretta del nuovo Titolo IX Capo I D.Lgs.81/08.
 
SI RACCOMANDA DI LEGGERE ATTENTAMENTE IL MODELLO DI VALUTAZIONE SCARICABILE DALLE PAGINE:
http://www.ausl.mo.it/dsp/MoVaRisCh
";
        }
    }
}
