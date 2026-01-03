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
    public partial class ControlloEsposizioneCutanea : UserControl
    {

        //proprietà
       
        private FormSostanza father;
        private int tipoUso;

        public int getVal(int tipo, int livello)
        {

            if (checkBox1.Checked == true) return 0;
            else if ((livello == 0) | ((livello == 1) & (tipo == 0))) return 1;
            else if ((livello == 1 & tipo == 1) | (livello ==1 & tipo == 2) | (livello == 2 & tipo == 0) | (livello == 2 & tipo == 1)) return 3;
            else if ((livello == 1 & tipo == 3) | (livello == 2 & tipo == 2) | (livello == 2 & tipo == 3) | (livello == 3 & tipo == 0) | (livello == 3 & tipo == 1)) return 7;
            else if ((livello == 3 & tipo == 2) | (livello == 3 & tipo == 3)) return 10;
            else
            {
                //ERROR
                return -1;
            }
        }


        public ControlloEsposizioneCutanea(FormSostanza f)
        {
            InitializeComponent();
            this.father = f;
            label2.Text = "";
            //this.tipoUso = tu;
        }

        //CLICCATO NO ESPOSIZIONE CUTANEA
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
            }
        }

        //PREMUTO AVANTI
        private void button1_Click(object sender, EventArgs e)
        {
           

            String contattoHR="Non definito";
            int contatto=-1;
            foreach (Control c in groupBox2.Controls)
            {
                if ( (c.GetType()==typeof(RadioButton)) && (((RadioButton)c).Checked) ){
                    contatto=((RadioButton)c).TabIndex;
                    contattoHR=((RadioButton)c).Text;
                }
            }




            this.tipoUso = this.father.valutazione.tipoUsoInal;
            
            //if (!(this.einal > 0)) MessageBox.Show("Errore: oggetto valutazione esposizione inalatoria uguale a zero non ammesso");
            //if (this.esposizioneCutanea & (this.ecute <= 0)) MessageBox.Show("Errore: oggetto valutazione esposizione cutanea uguale a zero ma non è stata dichiarata l'impossibilita del contatto cutaneo");
            if (contatto == -1 & !checkBox1.Checked) 
            {
                MessageBox.Show("Non hai complato la scheda per la valutazione del rischio cutaneo");
            }
            else 
            { 
                //
                this.father.returnResultCute(contatto, contattoHR, tipoUso, !checkBox1.Checked, this.getVal(this.tipoUso, contatto));

            
            }


            
           

        }









        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //abbandona 
            DialogResult r1 = MessageBox.Show("Sei davvero sicuro?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r1 == DialogResult.Yes) this.FindForm().Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ControlloEsposizioneCutanea_Load(object sender, EventArgs e)
        {
            //ATTENZIONE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            this.tipoUso=this.father.valutazione.tipoUsoInal;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Non più di un evento al giorno,\r\n divuto a spruzzi o rilasci \r\n occasionali (come per esempio \r\n nella preparazione \r\n di una vernice).";
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Da 2 a 10 eventi al giorno,\r\n dovute alle caratteristiche\r\n proprie del processo.";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Il numero di eventi \r\n giornalieri è superiore a 10";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.father.indietroASostanze();
        }
    }
}
