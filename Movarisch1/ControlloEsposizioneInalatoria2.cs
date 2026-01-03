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
    public partial class ControlloEsposizioneInalatoria2 : UserControl
    {
        private FormSostanza father;
        private int matricedue=-2;
        private int matrice3 = -1;
        private int matrice4 = -1;

        private bool debug = false;

        public ControlloEsposizioneInalatoria2(FormSostanza f)
        {
            InitializeComponent();
            father = f;
           // this.matricedue=matrice2;
        }

        private void ControlloEsposizioneInalatoria2_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("this.father.matrice2 =" + this.father.matrice2);
            this.matricedue = this.father.matrice2;
            
            label1.Text = @"Per tempo di esposizione si intende 
il tempo (espresso in minuti) durante il quale 
il lavoratore si trova esposto all’agente 
chimico pericoloso su base giornaliera";

            label2.Text = @"Per distanza degli esposti dalla sorgente
si intende la distanza, espressa in metri, 
tra la posizione del lavoratore ed 
il punto di rilascio (sorgente)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TIPOLOGIA DI CONTROLLO
            int tipocontrollo = -1;
            String tipoCont = "Non specificato";
            foreach (Control c in groupBox2.Controls)
            {
                if ((c.GetType() == typeof(RadioButton)) && (((RadioButton)c).Checked))
                {
                    tipoCont = ((RadioButton)c).Text;
                    tipocontrollo = ((RadioButton)c).TabIndex;
                }
            }

          

            //DISTANZA
            decimal distanza = numericUpDown3.Value;

            //TEMPO
            decimal tempo = numericUpDown2.Value;


            this.matricedue = this.father.matrice2;

            //MATRICE 3
            if(this.matricedue==1){
                if (tipocontrollo == 3 || tipocontrollo == 4)
                {
                    this.matrice3 = 2;
                }
                else {
                    this.matrice3 = 1;
                }
            }
            else if(this.matricedue==2){
                if(tipocontrollo==0){
                    this.matrice3 = 1;
                }
                else if (tipocontrollo == 1 || tipocontrollo == 2)
                {
                    this.matrice3 = 2;
                }
                else {
                    this.matrice3 = 3;
                }
            }
            else if(this.matricedue==3){
                if(tipocontrollo==0){
                    this.matrice3 = 1;
                }
                else if (tipocontrollo == 1) {
                    this.matrice3 = 2;
                }
                else{
                    this.matrice3 = 3;
                }
            }
            else {
                MessageBox.Show("ERRORE di calcolo nel modello alla matrice 3 ");
            }

            //MATRICE 4
            if (debug) MessageBox.Show("Matrice 3 [1-3] : "+this.matrice3);

            decimal quindici=16;
            decimal centoventi=121;//2h
            //decimal duecentoquaranta=240;
            decimal trecentosessanta=361; //3h

            if (this.matrice3 == 1)
            {
                if (decimal.Compare(tempo, centoventi)< 0) {
                    //MENO DI 2h
                    this.matrice4 = 1;

                }//decimal.Compare(chilogrammi, dieci) < 0
                else if (decimal.Compare(tempo, trecentosessanta) < 0)
                {
                    this.matrice4 = 3;
                }
                else {
                    this.matrice4 = 7;
                }
            }
            else if (this.matrice3 == 2)
            {
                if(decimal.Compare(tempo, quindici)< 0){
                    this.matrice4 = 1;
                }
                else if(decimal.Compare(tempo, centoventi)< 0){
                    this.matrice4 = 3;
                }
                else if (decimal.Compare(tempo, trecentosessanta) < 0) {
                    this.matrice4 = 7;
                }
                else{
                    this.matrice4 = 10;
                }
            }
            else if (this.matrice3 == 3)
            {
                if(decimal.Compare(tempo, quindici)< 0){
                    this.matrice4 = 3;
                }
                else if (decimal.Compare(tempo, centoventi) < 0) {
                    this.matrice4 = 7;
                }
                else{
                    this.matrice4 = 10;
                }
            }
            else { MessageBox.Show("ERRORE di calcolo nel modello alla matrice 4, matrice 3:" + this.matrice3.ToString() + " matrice4:" + this.matrice4.ToString()); }

            if (debug) MessageBox.Show("Matrice 4 [1,3,7,10]" + this.matrice4);
            // Einal = matrice4 x indice distanza
            //1-1 3-0,75 5-0,5 10-0,25 +-0,1

            float einal = 1;
            
            decimal uno =  1;
            decimal tre = 3;
            decimal cinque = 5;
            decimal dieci = 10;

            if(decimal.Compare(distanza, uno) <= 0){
                einal = (float)1 * this.matrice4;
            }
            else if(decimal.Compare(distanza, tre) <= 0){
                einal = (float)0.75 * this.matrice4;
            }
            else if (decimal.Compare(distanza, cinque) <= 0){
                einal = (float)0.5 * this.matrice4;
            }
            else if (decimal.Compare(distanza, dieci) <= 0){
                einal = (float)0.25 * this.matrice4;
            }
            else if (decimal.Compare(distanza, dieci) >= 0) {
                einal = (float)0.1 * this.matrice4;
            }
            else{
                MessageBox.Show("Errore nella distanza, controllare il valore inserito");
            }


            //CONTROLLO SPECIFICATO TIPOLOGIA DI CONTROLLO
            if (tipocontrollo == -1)
            {
                MessageBox.Show("Prima di proseguire specifica la tipologia di controllo");
            } else {
                this.father.returnResultInal2(this.matrice3, this.matrice4, distanza, tempo, tipoCont, einal);
                this.father.faseCutanea();
            }


           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //abbandona 
            DialogResult r1 = MessageBox.Show("Sei davvero sicuro?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r1 == DialogResult.Yes) this.FindForm().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SpiegazioneTipoControllo c = new SpiegazioneTipoControllo();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //INDIETRO
            this.father.indietroASostanze();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
