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
    public partial class ControlloEsposizioneInalatoria : UserControl
    {
        private  FormSostanza father;

        private bool debug = false;


        public ControlloEsposizioneInalatoria(FormSostanza f)
        {
            InitializeComponent();
            father = f;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            //VALIDAZIONE


            int statofisico=-1;
            int tipologiauso=-1;
            

            //STATO FISICO
            String statof="Non definito";
            foreach (Control c in groupBox3.Controls)
            {
                if ( (c.GetType()==typeof(RadioButton)) && (((RadioButton)c).Checked) ){
                    statofisico=((RadioButton)c).TabIndex;
                    statof=((RadioButton)c).Text;
                   //MessageBox.Show(((RadioButton)c).TabIndex.ToString());
                }
            }
            if (statofisico == 4) { statofisico = 0; }
            if (statofisico == 5) { statofisico = 2; }
            if (statofisico == 6) { statofisico = 2; }

            //TIPOLOGIA USO
            String tipoUsoHR = "Non definito";
            foreach (Control c in groupBox1.Controls)
            {
                if ((c.GetType() == typeof(RadioButton)) && (((RadioButton)c).Checked))
                {
                    tipologiauso=((RadioButton)c).TabIndex;
                    tipoUsoHR = ((RadioButton)c).Text;
                }
            }

            //QUANTITA iN USO
            decimal chilogrammi = numericUpDown1.Value;


           



            decimal zerouno = 0.1m;
            decimal uno=1;
            decimal dieci=10;
            decimal cento=100;

            int matriceuno=-1;


           // MessageBox.Show("matrice1:" + matriceuno + " tipoUsoHR:"+tipoUsoHR+" tipouso:"+tipologiauso);

            //MATRICE 1
            if(decimal.Compare(chilogrammi,zerouno) <= 0){
                //meno di 0.1
                if(statofisico==3){
                    matriceuno = 2;
                }
                else{
                    matriceuno = 1;
                }
            }
            else if (decimal.Compare(chilogrammi, uno) <= 0) { 
                //meno di 1
                if(statofisico==0){
                    matriceuno = 1;
                }
                else if(statofisico==1){
                    matriceuno = 2;
                }
                else{
                    matriceuno = 3;
                }
            }
            else if(decimal.Compare(chilogrammi, dieci) <= 0){
                //meno di 10
                if(statofisico==0){
                    matriceuno=1;
                }
                else if(statofisico==3){
                    matriceuno=4;
                }
                else{
                    matriceuno=3;
                }
            }
            else if(decimal.Compare(chilogrammi, cento) <= 0){
                //meno di 100
                if(statofisico==0){
                    matriceuno=2;
                }
                else if(statofisico==1){
                    matriceuno=3;
                }
                else{
                    matriceuno=4;
                }
            }
            else {
                //più di 100
                if(statofisico==0){
                    matriceuno = 2;
                }
                else{
                    matriceuno = 4;
                }
            }

            if (debug) MessageBox.Show("Matrice 1 [1-4]:" + matriceuno + " tipoUso:" + tipoUsoHR + " tipouso:" + tipologiauso);
            int matricedue = -1;

            //MessageBox.Show("matrice2:" + matricedue + " tipoUsoHR:" + tipoUsoHR + " tipouso:" + tipologiauso);
            //MATRICE 2
            if(matriceuno==1){
                if(tipologiauso==3){
                    matricedue=2;
                }
                else{
                    matricedue=1;
                }
            }
            else if(matriceuno==2){
                 if(tipologiauso==0){
                    matricedue=1;
                 }
                 else if(tipologiauso==3) {
                    matricedue=3;
                 }
                 else{
                    matricedue=2;
                 }
            }
            else if(matriceuno==3){
                 if(tipologiauso==0){
                    matricedue=1;
                 }
                 else if(tipologiauso==1){
                    matricedue=2;
                 }
                 else{
                    matricedue=3;
                 }
            }
            else{
                 if(tipologiauso==0){
                     matricedue = 2;
                 }
                 else{
                    matricedue = 3;
                 }
            }

            if (debug) MessageBox.Show("Matrice 2 [1-3]:" + matricedue + " tipoUso:" + tipoUsoHR + " tipouso:" + tipologiauso);

            //VALIDAZIONE
            if (statofisico == -1 | tipologiauso == -1)
            {
                MessageBox.Show("Compila tutti i campi prima di proseguire");
            }
            else
            {

                this.father.returnResultInal1(statof, chilogrammi, tipologiauso, tipoUsoHR, matricedue);
                this.father.faseInalatoria2();
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //abbandona 
            DialogResult r1 = MessageBox.Show("Sei davvero sicuro?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r1 == DialogResult.Yes) this.FindForm().Close();
        }





        private void ControlloEsposizioneInalatoria_Load(object sender, EventArgs e)
        {
            label5.Text = "Per quantità in uso si intende la quantità di \r\n agente chimico  o del preparato \r\n effettivamente presente e destinato, \r\n con qualunque modalità, \r\n all'uso nell'ambiente di lavoro \r\n su base giornaliera";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SpiegazioneTipoUso b = new SpiegazioneTipoUso();
            b.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GraficoStatoFisico a = new GraficoStatoFisico();
            a.Show();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //INDIETRO
            this.father.indietroASostanze();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
