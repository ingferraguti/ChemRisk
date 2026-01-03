using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Movarisch1
{
    public partial class FormCreaSostanza : Form
    {
        private AgenteChimico agente;
        private String tipo;
        private String mode;
        private FormSostanza father1;
        private CaricaSostanza father2;


        public string[,] frasih = new string[60, 3];
        public int frasiHindex = 0;


        public FormCreaSostanza()
        {
            InitializeComponent();
        }
        
        public FormCreaSostanza(FormSostanza father,String tipo)
        {
            //NUOVO

            InitializeComponent();
            this.tipo = tipo;
            this.father1 = father;
            this.setVisibility();

            this.mode = "nuovo";

           

        }


        public FormCreaSostanza(CaricaSostanza father, String tipo)
        {
            //NUOVO

            InitializeComponent();
            this.tipo = tipo;
            this.father2 = father;
            this.setVisibility();

            this.mode = "nuovo";



        }

        public FormCreaSostanza(CaricaSostanza father, AgenteChimico a)
        {
            //MODIFICA

            InitializeComponent();
            if (a == null) return;

            this.agente = a;
            this.tipo = a.tipo;
            this.setVisibility();

            textBox1.Text = this.agente.nome;
            textBox2.Text = this.agente.identificativo;
            checkBox3.Checked = this.agente.vlep;

            radioButton1.Checked = (a.altaemissione) & (a.tipo == "processo");
            radioButton2.Checked = (!a.altaemissione) & (a.tipo == "processo");

            this.mode = "modifica";

            if (this.tipo == "sostanza" | this.tipo == "miscelaP")
            {
                //Aggiungi frasi h
                foreach (String[] frase in a.frasiH)
                {
                    if (frase[0] != "") { listBox4.Items.Add(frase[0]+"("+frase[1]+")"); }
                }
            }
            else if (this.tipo == "miscelaNP" | this.tipo == "processo")
            {
                listBox4.ValueMember = "Id";
                listBox4.DisplayMember = "Nome";
                listBox4.DataSource = a.getComponentiMiscela();
            }

        }

        private void FormCreaSostanza_Load(object sender, EventArgs e)
        {
            int numline = 0;
            String line;


            //PREPARA LISTBOX
            if (this.tipo == "sostanza" | this.tipo == "miscelaP")
            {
                //______CARICA FRASI H ___________________________________________________________
                using (FileStream streamAziende = File.Open("frasi_h.dat", FileMode.Open))
                using (StreamReader r = new StreamReader(streamAziende))
                {
                    while ((line = r.ReadLine()) != null)
                    {
                        //this.aziende[this.aziendeIndex] = nuova;
                        //this.aziendeIndex++;
                        string[] frase = new string[3];
                        frase = line.Split(';');
                        this.frasih[numline, 0] = frase[0];
                        this.frasih[numline, 1] = frase[1];
                        this.frasih[numline, 2] = frase[2];

                        numline++;

                        comboBox1.Items.Add(frase[0]);
                    }
                    this.frasiHindex = numline;
                    //MessageBox.Show("trovate "+numline.ToString()+" frasi h");
                }
            }
            else if (this.tipo == "processo" | this.tipo == "miscelaNP")
            {


                List<AgenteChimico> agenti = new List<AgenteChimico>();
                //agenti = DbAgentiChimici.retrieve();
                agenti = DbAgentiChimici.getOfType("sostanza");

                comboBox1.DisplayMember = "Nome";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = agenti;

                listBox4.ValueMember = "Id";
                listBox4.DisplayMember = "Nome";

            }
        }

        private void setVisibility() 
        {
            if (this.tipo == "sostanza")
            {
              
            }
            else if (this.tipo == "miscelaP")
            {
               
                label4.Text = "Miscela Pericolosa";

                //cas+vlep
                label6.Visible = false;
                textBox2.Visible = false;
                checkBox3.Visible = false;

                //this.panel3.Visible = false;
              
                //groupBox1.Visible = false;

            }
            else if (this.tipo == "miscelaNP")
            {
              
                label4.Text = "Miscela non Pericolosa che contiene sostanze pericolose";
              
                //groupBox1.Visible = false;

                //cas+vlep
                label6.Visible = false;
                textBox2.Visible = false;
                checkBox3.Visible = false;

                label7.Text = "Sostanze";

            }
            else if (this.tipo == "processo")
            {
                
                label4.Text = "Processo che rilascia sostanze pericolose";
               

                //cas+vlep
                label6.Visible = false;
                textBox2.Visible = false;
                checkBox3.Visible = false;

                label7.Text = "Sostanze";

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.tipo == "sostanza" | this.tipo == "miscelaP")
            {
                for (int i = 0; i < frasiHindex; i++)
                {
                    if (comboBox1.SelectedItem.ToString() == frasih[i, 0])
                    {
                        listBox4.Items.Add(frasih[i, 0] + "(" + frasih[i, 1] + ")");
                    }
                    else { }
                }
            }
            else if (this.tipo == "processo" | this.tipo == "miscelaNP")
            {
                //INSERISCI LA SOSTANZA
                if (comboBox1.SelectedItem != null)
                {
                    listBox4.Items.Add(comboBox1.SelectedItem);
                    AgenteChimico a = (AgenteChimico)comboBox1.SelectedItem;
                    //MessageBox.Show(a.id.ToString()+" - ");
                }
                else
                {
                    MessageBox.Show("Non hai selezionato nulla nel menù a tendina");
                }
            }
            else
            {
                MessageBox.Show("errore nel tipo di agente chimico");
            }


            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //rimuovi selezionato frasih
            listBox4.Items.Remove(listBox4.SelectedItem);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //RIMUOVI TUTTO
            listBox4.Items.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("Il nome della sostanza è vuoto!");
            }
            //else if (this.textBox2.Text == "" && this.tipo == "sostanza")
            //{
            //    MessageBox.Show("I'identificativo della sostanza è vuoto!");
           // }
            else if (listBox4.Items.Count == 0 & checkBox3.Checked == false)
            {
                MessageBox.Show("Non hai inserito elementi di pericolosità nell'agente chimico. Se l'agente chimico non è pericoloso e non ha valori limite di esposizione professionale allora non è necessaria questa valutazione.");
            }
            else
            {
                AgenteChimico nuovo = new AgenteChimico();
                if (this.mode=="nuovo"){
                    nuovo.id = IncrementalIndex.getNewIndex("nuovo agente chimico");
                }
                else if (this.mode == "modifica")
                {
                    nuovo.id = this.agente.id;
                }
                else { MessageBox.Show("Errore 1492. Contattare assistenza"); }

                nuovo.nome = textBox1.Text;
                nuovo.identificativo = textBox2.Text;
                nuovo.altaemissione = radioButton1.Checked;
                nuovo.tipo=this.tipo;
                nuovo.vlep=checkBox3.Checked;

                if (this.tipo == "sostanza" | this.tipo == "miscelaP")
                {
                    List<String[]> frasiHAgenteChimico = new List<String[]>(); 
                    foreach (string fr in listBox4.Items)//per ogni frase lezionata ne prendo un alla volta
                    {
                        string fras = fr.Split('(')[0];//prendo solo il codice senza descrizione
                        for (int i = 0; i < frasiHindex; i++)//per ogni frase h esistente
                        {
                            if (fras == frasih[i, 0]) //se ho trovato quella giusta
                            {
                                String[] frasehTemp = new String[3];
                                frasehTemp[0] = frasih[i, 0];
                                frasehTemp[1] = frasih[i, 1];
                                frasehTemp[2] = frasih[i, 2];
                                frasiHAgenteChimico.Add(frasehTemp);
                            }
                        }
                    }
                    nuovo.frasiH = frasiHAgenteChimico;
                }
                else if (this.tipo == "processo" | this.tipo == "miscelaNP")
                {
                    List<int> componenti = new List<int>();
                    foreach (AgenteChimico ac in listBox4.Items)
                    {
                        componenti.Add(ac.id);
                    }
                    nuovo.sostanze = componenti.ToArray();
                }

                if (this.mode == "nuovo")
                {
                    nuovo.id = IncrementalIndex.getNewIndex("nuovo agente chimico");
                    DbAgentiChimici.append(nuovo);
                }
                else if (this.mode == "modifica")
                {
                    nuovo.id = this.agente.id;
                    if (DbAgentiChimici.elimina(this.agente.id, true) == 2) {
                        if (this.agente.nome == nuovo.nome) {
                            nuovo.nome = "Copia di " + nuovo.nome;
                        }
                        nuovo.id = IncrementalIndex.getNewIndex("nuovo agente chimico");
                    }
                    System.Threading.Thread.Sleep(500);
                    DbAgentiChimici.append(nuovo);
                }
                else { MessageBox.Show("Errore 1789. Contattare assistenza"); }




                //CHIUDI
                this.Close();
                if (this.father1!=null) {
                    this.father1.aggiornaSostanze();
                }
                

            }

          

            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button7.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //CHIUDI
            this.Close();
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            button8.Enabled = true;
        }
    }
}
