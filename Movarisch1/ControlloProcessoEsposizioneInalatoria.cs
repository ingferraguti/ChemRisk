using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movarisch1
{
    public partial class ControlloProcessoEsposizioneInalatoria : UserControl
    {
        private FormSostanza father;
        public ControlloProcessoEsposizioneInalatoria(FormSostanza f)
        {
            InitializeComponent();
            this.father = f;
        }

        private void ControlloProcessoEsposizioneInalatoria_Load(object sender, EventArgs e)
        {
            label2.Text = "Per quantità in uso si intende la quantità di \r\n agente chimico  o del preparato \r\n effettivamente presente e destinato, \r\n con qualunque modalità, \r\n all'uso nell'ambiente di lavoro \r\n su base giornaliera";
            label5.Text = @"Per tempo di esposizione si intende 
il tempo (espresso in minuti) durante il quale 
il lavoratore si trova esposto all’agente 
chimico pericoloso su base giornaliera";

            label10.Text = @"Per distanza degli esposti dalla sorgente
si intende la distanza, espressa in metri, 
tra la posizione del lavoratore ed 
il punto di rilascio (sorgente)";


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //abbandona 
            DialogResult r1 = MessageBox.Show("Sei davvero sicuro?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r1 == DialogResult.Yes) this.FindForm().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CALCOLO e poi AVANTI ovvero SALVO e chiudo
            
            decimal tempo;
            
            //matrice1
            decimal dieci = 10;
            decimal cento = 100;

            decimal qtaInUso = numericUpDown1.Value;
            
            int c=0;
            int tipologiaControllo=-1;
            
            String descTipologiaControllo="Non definito";
            foreach (Control cc in groupBox2.Controls)
            {
                if ( (cc.GetType()==typeof(RadioButton)) && (((RadioButton)cc).Checked) ){
                    tipologiaControllo=((RadioButton)cc).TabIndex;
                    descTipologiaControllo=((RadioButton)cc).Text;
                   //MessageBox.Show(((RadioButton)c).TabIndex.ToString());
                }
            }

            

            if(qtaInUso<dieci){
                if(tipologiaControllo==0||tipologiaControllo==1||tipologiaControllo==2){
                    c = 1;
                }
                else if (tipologiaControllo==3){
                    c = 2;
                }
                else{
                    //Errore
                }
            }
            else if(qtaInUso>=10 || qtaInUso<=100){
                if(tipologiaControllo==0){
                    c = 1;
                }
                else if(tipologiaControllo==1||tipologiaControllo==2){
                    c = 2;
                }
                else if(tipologiaControllo==3){
                    c = 3;
                }
                else{
                    //Errore
                }
            }
            else if(qtaInUso>100){
                if(tipologiaControllo==0){
                    c = 1;
                }
                else if(tipologiaControllo==1){
                    c = 2;
                }
                else if (tipologiaControllo == 2 || tipologiaControllo == 3)
                {
                    c = 3;
                }
                else {
                    //Errore
                }
            }
            else{
                //Errore
            }

            int matrice2Bis = -1;

            decimal quindici = 15;
            decimal centoventi = 120;//2h
            //decimal duecentoquaranta=240;
            decimal trecentosessanta = 360; //3h

            tempo = numericUpDown2.Value;

            if (c == 1)
            {
                if (decimal.Compare(tempo, centoventi) < 0)
                {
                    //MENO DI 2h
                    matrice2Bis = 1;

                }//decimal.Compare(chilogrammi, dieci) < 0
                else if (decimal.Compare(tempo, trecentosessanta) < 0)
                {
                    matrice2Bis = 3;
                }
                else
                {
                    matrice2Bis = 7;
                }
            }
            else if (c == 2)
            {
                if (decimal.Compare(tempo, quindici) < 0)
                {
                    matrice2Bis = 1;
                }
                else if (decimal.Compare(tempo, centoventi) < 0)
                {
                    matrice2Bis = 3;
                }
                else if (decimal.Compare(tempo, trecentosessanta) < 0)
                {
                    matrice2Bis = 7;
                }
                else
                {
                    matrice2Bis = 10;
                }
            }
            else if (c == 3)
            {
                if (decimal.Compare(tempo, quindici) < 0)
                {
                    matrice2Bis = 3;
                }
                else if (decimal.Compare(tempo, centoventi) < 0)
                {
                    matrice2Bis = 7;
                }
                else
                {
                    matrice2Bis = 10;
                }
            }
            else 
            { 
                //ERRORE
            }

            //____________________
            decimal distanza = numericUpDown3.Value;
            decimal uno = 1;
            decimal tre = 3;
            decimal cinque = 5;
            float einal = -1;


            if (decimal.Compare(distanza, uno) < 0)
            {
                einal = (float)1 * matrice2Bis;
            }
            else if (decimal.Compare(distanza, tre) < 0)
            {
                einal = (float)0.75 * matrice2Bis;
            }
            else if (decimal.Compare(distanza, cinque) < 0)
            {
                einal = (float)0.5 * matrice2Bis;
            }
            else if (decimal.Compare(distanza, dieci) < 0)
            {
                einal = (float)0.25 * matrice2Bis;
            }
            else if (decimal.Compare(distanza, dieci) >= 0)
            {
                einal = (float)0.1 * matrice2Bis;
            }
            else
            {
                MessageBox.Show("Errore");
            }

            if (tipologiaControllo == -1) { MessageBox.Show("Non hai inserito la \"Tipologia di controllo\"."); }
            else
            {
                //FINE
                this.father.fineValutazioneProcesso(einal, (int)distanza, (int)qtaInUso, (int)tempo, descTipologiaControllo);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //INDIETRO
            this.father.indietroASostanze();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SpiegazioneTipoControllo c = new SpiegazioneTipoControllo();
            c.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
