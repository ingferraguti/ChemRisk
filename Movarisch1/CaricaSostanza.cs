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
    public partial class CaricaSostanza : Form
    {
        FormSostanza father;
        String[,] elencoSostanze;
        String[,] elencoMiscele;
        int numSostanze;
        String[] frasi;
        String tipo;

        Boolean nofather = false;

        public CaricaSostanza(FormSostanza f, String t)
        {
            InitializeComponent();
            this.father = f;
            this.tipo = t;

            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
        }



        public CaricaSostanza(String t)
        {
            InitializeComponent();
            this.tipo = t;
            this.nofather = true;
            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
        }

        private void aggiorna() 
        {
            listBox1.Items.Clear();
            List<AgenteChimico> listaAgenti = DbAgentiChimici.getOfType(this.tipo);
            listBox1.DisplayMember = "Nome";
            listBox1.ValueMember = "Id";
            if (listaAgenti != null) listBox1.Items.AddRange(listaAgenti.ToArray());
        }

        private void CaricaSostanza_Load(object sender, EventArgs e)
        {
            this.aggiorna();

            if(this.tipo=="sostanza"){
                label7.Text = "Sostanze pericolose salvate";
            }
	        else if (this.tipo=="miscelaP"){
                label7.Text = "Miscele pericolose salvate";
                label8.Visible = false;
                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;

            }
	        else if (this.tipo=="miscelaNP"){
                label7.Text = "Miscele non pericolose salvate";
                label8.Visible = false;
                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label3.Text = "Sostanze:";

            }
            else if (this.tipo == "processo" ){
                label7.Text = "Agenti chimici derivanti da attività produttive salvati";
                label8.Visible = false;
                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label3.Text = "Sostanze:";
            }





            /*______________
            comboBox1.Items.Add("Sostanze Pericolose");
            comboBox1.Items.Add("Miscele Pericolose");
            comboBox1.Items.Add("Miscele non Pericolose");
            comboBox1.Items.Add("Agenti chimici derivati da attività lavorative");
             * ________*/
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            
            AgenteChimico selected = (AgenteChimico)listBox1.SelectedItem;

            if (selected == null) { return; }

            label4.Text = selected.nome;
            label5.Text = selected.identificativo;
            label9.Text = "";
            if(selected.vlep==false){
                label6.Text = "No";
            }
            else {
                label6.Text = "Si";
            }

            if (this.tipo == "processo" | this.tipo == "miscelaNP") 
            { 
                foreach(AgenteChimico a in selected.getComponentiMiscela())
                {
                    label9.Text += a.nome+" ( "+a.identificativo+" )" + " \r\n";
                }
            }
            else
            {
                foreach (String frase in selected.getArrayFrasiH())
                {
                    label9.Text += frase + " \r\n";
                }
            }
            button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CARICA
            //father.caricaSostanza(elencoSostanze[1, listBox1.SelectedIndex], elencoSostanze[2, listBox1.SelectedIndex], Boolean.Parse(elencoSostanze[3, listBox1.SelectedIndex]), this.frasi);
            AgenteChimico selected = (AgenteChimico)listBox1.SelectedItem;
            if(!this.nofather)father.caricaAgenteChimico(selected);
            this.Close();
        }









        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //sei davvero sicuro???????
            DialogResult r1 = MessageBox.Show("Vuoi veramente eliminare", "Conferma di eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r1 == DialogResult.Yes)
            {
                //Elimina agente chimico selezionato
                AgenteChimico selected = (AgenteChimico)listBox1.SelectedItem;
                if (listBox1.SelectedItem != null)
                {
                    if (selected.id != 0)
                    {
                        DbAgentiChimici.elimina(selected.id);
                    }
                    else
                    {
                        MessageBox.Show("Non hai selezionato nulla");
                    }
                }
                else {
                    MessageBox.Show("Non hai selezionato nulla");
                }
                
            }
            else
            {
                //non succede nulla e si può continuare con l'inserimento dati
            }
           
            //ricarica
            this.aggiorna();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AgenteChimico selected = (AgenteChimico)listBox1.SelectedItem;
            FormCreaSostanza fcs = new FormCreaSostanza(this,selected);
            DialogResult r = fcs.ShowDialog();
            //non controllo il risultato ma fermo l'esecuzione per eseguire il seguente codice DOPO che il form è stato chiuso.
            this.aggiorna();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormCreaSostanza fcs = new FormCreaSostanza(this,this.tipo);
            DialogResult r = fcs.ShowDialog();
            //non controllo il risultato ma fermo l'esecuzione per eseguire il seguente codice DOPO che il form è stato chiuso.
            this.aggiorna();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
