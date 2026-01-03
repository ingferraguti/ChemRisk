using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;

namespace Movarisch1
{
    public partial class FormSostanza : Form
    {
        //variabili di load
        private Form1 father;
        private String tipo;

        //int l;//id lavoraore
        Lavoratore lavoratore;
        //private int idValutazione = 0;

        //dati esterni
        public int frasiHindex = 0;
        public string[,] frasih = new string[60,3];

        //variabili calcolate internamente
        private float score = 0;
        public int matrice2 = -1;
        private int matrice3 = -1;
        private AgenteChimico agenteChimico = new AgenteChimico();
        
        //output
        public Valutazione valutazione;

        //grafica
        private Panel conteiner;
        private Panel conteiner2;
        private Panel conteiner3;
        private Panel conteiner4;

        struct AgentiTemp
        {
            public int id;
            public string nome;
        }

        public FormSostanza(Form1 f, int idLavoratore, String t)
        {
            InitializeComponent();
            this.father = f;
            this.lavoratore = Utils.findLavoratore(idLavoratore);
            treeView2.Nodes.Add("lavoratore","Lavoratore: "+this.lavoratore.nome+" "+this.lavoratore.cognome);
            this.tipo = t;
            int id = IncrementalIndex.getNewIndex("nuova valutazione");//SICURI???
            this.valutazione = new Valutazione(id,idLavoratore);

           
        }

        private void FormSostanza_Load(object sender, EventArgs e)
        {
            this.inizializzazione();
            TreeNode nodeTipo;

            if (this.tipo == "sostanza")
            {
                nodeTipo = new TreeNode("Tipologia: Sostanza Pericolosa");
                //groupBox1.Visible = false;
                button1.Visible = false;
            }
            else if (this.tipo == "miscelaP")
            {
                nodeTipo = new TreeNode("Tipologia: Miscela Pericolosa");
                this.Text = "Valutazione guidata: Miscela pericolosa";
                label4.Text = "Miscela Pericolosa";

                //cas+vlep
                label6.Visible = false;
                textBox2.Visible = false;
                checkBox3.Visible = false;

                //this.panel3.Visible = false;
                button12.Text = "Carica Miscela Salvata";
                //groupBox1.Visible = false;

                button1.Visible = false;
            }
            else if (this.tipo == "miscelaNP")
            {
                nodeTipo = new TreeNode("Tipologia: Miscela Non Pericolosa");
                this.Text = "Valutazione guidata: Miscela Non pericolosa";
                label4.Text = "Miscela non Pericolosa che contiene sostanze pericolose";
                button12.Text = "Carica Miscela Salvata";

                //groupBox1.Visible = false;

                //cas+vlep
                label6.Visible = false;
                textBox2.Visible = false;
                checkBox3.Visible = false;

                label7.Text = "Sostanze";
                

                /*
                this.conteiner = new Panel();
                ControlloMiscele cm = new ControlloMiscele(this);
                this.conteiner.Controls.Add(cm);
                this.conteiner.Location = new Point(260, 20);
                this.conteiner.Size = new Size(700, 600);
                this.Controls.Add(conteiner);
                this.panel3.Visible = false;
                nodeTipo = new TreeNode("Tipologia: Miscela non Pericolosa");
                this.Text = "Valutazione guidata: Miscela non pericolosa";
                 * */
            }
            else if (this.tipo == "processo")
            {
                nodeTipo = new TreeNode("Tipologia: Agente chimico rilasciato da un processo");
                this.Text = "Valutazione guidata: Agente chimico rilasciato da un processo";
                label4.Text = "Processo che rilascia sostanze pericolose";
                button12.Text = "Carica Processo Salvato";

                //cas+vlep
                label6.Visible = false;
                textBox2.Visible = false;
                checkBox3.Visible = false;

                label7.Text = "Sostanze";

                groupBox1.Visible = true;

                /*
                this.conteiner = new Panel();
                ControlloProcesso cm = new ControlloProcesso(this);
                this.conteiner.Controls.Add(cm);
                this.conteiner.Location = new Point(260, 20);
                this.conteiner.Size = new Size(700, 600);
                this.Controls.Add(conteiner);
                this.panel3.Visible = false;
                nodeTipo = new TreeNode("Tipologia: derivante da Processo"); 
                this.Text = "Valutazione guidata: Agente chimico derivato da processo";
                 */

            }
            else {
                nodeTipo = new TreeNode("errore sul tipo");
                this.panel3.Visible = false;
            }

            //treeView2.Nodes.Add(nodeTipo);

            int numline = 0;
            String line;

            //PREPARA LISTBOX
            if(this.tipo=="sostanza"|this.tipo=="miscelaP")
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
                agenti = DbAgentiChimici.getOfType("sostanza");

                comboBox1.DisplayMember = "Nome";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = agenti;

                listBox4.ValueMember = "Id";
                listBox4.DisplayMember = "Nome";
               
            }
        }


        public void indietroASostanze()
        {
            this.conteiner2.Hide();
            this.treeView2.Nodes.Clear();
            this.panel3.Show();
            treeView2.Nodes.Add("lavoratore", "Lavoratore: " + this.lavoratore.nome + " " + this.lavoratore.cognome);
        }

        public void inizializzazione() {

            ControlloProcessoEsposizioneInalatoria inalProcesso = new ControlloProcessoEsposizioneInalatoria(this);
            this.conteiner2 = new Panel();
            this.conteiner2.Controls.Add(inalProcesso);

            ControlloEsposizioneInalatoria inal = new ControlloEsposizioneInalatoria(this);
            this.conteiner4 = new Panel();
            this.conteiner4.Controls.Add(inal);

            ControlloEsposizioneInalatoria2 inal2 = new ControlloEsposizioneInalatoria2(this);
            this.conteiner3 = new Panel();
            this.conteiner3.Controls.Add(inal2);

            ControlloEsposizioneCutanea cute = new ControlloEsposizioneCutanea(this);
            this.conteiner = new Panel();
            this.conteiner.Controls.Add(cute);
        }

        public void faseInalatoriaProcesso() 
        {
            this.conteiner.Visible = false;
            this.conteiner4.Visible = false;
            this.conteiner2.Visible = true;
            this.conteiner3.Visible = false;

            

            this.conteiner2.Location = new Point(this.panel4.Size.Width + 30, 10);
            this.conteiner2.Size = new Size(640, 640);
            this.Controls.Add(conteiner2);
            this.treeView2.ExpandAll();

        }

        public void faseInalatoria(){
            //faccio vedere solo il 4
            this.conteiner.Visible = false;
            this.conteiner4.Visible = true;
            this.conteiner2.Visible = false;
            this.conteiner3.Visible = false;

            Size sizriepilogo = this.panel4.Size;
            

            this.conteiner4.Location = new Point(sizriepilogo.Width + 30, 10);
            this.conteiner4.Size = new Size(640, 640);
            this.Controls.Add(this.conteiner4);
            this.treeView2.ExpandAll();

        }
        public void faseInalatoria2()
        {
            //faccio vedere solo il 3
            this.conteiner.Visible = false;
            this.conteiner4.Visible = false;
            this.conteiner2.Visible = false;
            this.conteiner3.Visible = true;

            Size sizriepilogo = this.panel4.Size;

            this.conteiner3.Location = new Point(sizriepilogo.Width + 30, 10);
            this.conteiner3.Size = new Size(640, 640);
            this.Controls.Add(conteiner3);
            
            this.treeView2.ExpandAll();

        }
        public void faseCutanea() {

            this.conteiner.Visible = true;
            this.conteiner4.Visible = false;
            this.conteiner2.Visible = false;
            this.conteiner3.Visible = false;

            Size sizriepilogo = this.panel4.Size;


            this.conteiner.Location = new Point(sizriepilogo.Width + 30, 0);
            this.conteiner.Size = new Size(640, 640);//450
           
            this.Controls.Add(conteiner);
            this.treeView2.ExpandAll();
        }

        public void returnResultInal1(String statofisico,decimal chilogrammi, int tipoUso, String tipoUsoHR, int matricedue) { 
            //
            treeView2.Nodes["inalatoria"].Nodes.Add("Stato fisico: "+statofisico);
            treeView2.Nodes["inalatoria"].Nodes.Add("Quantità: " + chilogrammi.ToString() + " Kg");
            treeView2.Nodes["inalatoria"].Nodes.Add("Tipologia di utilizzo: " + tipoUsoHR);
           // MessageBox.Show("matricedue=" + matricedue);
            this.matrice2 = matricedue;
           // MessageBox.Show("this.matrice2=" + this.matrice2);
            this.valutazione.statoFisicoInalHR = statofisico;
            this.valutazione.quantita = (float) chilogrammi;
            this.valutazione.tipoUsoInalHR = tipoUsoHR;
            this.valutazione.tipoUsoInal = tipoUso;
            //matricedue

        }

        public void returnResultInal2(int matricetre, int matricequattro,decimal distanza, decimal tempo,String tipocontrollo, float einal)
        {
            //SECONDA PARTE INALATORIO
            treeView2.Nodes["inalatoria"].Nodes.Add("Distanza: " + distanza.ToString()+" mt");
            treeView2.Nodes["inalatoria"].Nodes.Add("Tempo: " + tempo.ToString() + " min");
            treeView2.Nodes["inalatoria"].Nodes.Add("Tipologia di controllo: " + tipocontrollo);
            //indice finale inalatorio??
            //MessageBox.Show("Esposizione inalatoria: "+einal);
            treeView2.Nodes["inalatoria"].Nodes.Add("Livello di esposizione inalatoria: " + einal);
            this.matrice3 = matricetre;
            //matrice4

            this.valutazione.distanza = (float) distanza;
            this.valutazione.tempoInal = (int) tempo;
            this.valutazione.tipoControlloHR = tipocontrollo;
            this.valutazione.einal = einal;
            //matrice tre
            //matrice 4
        }

        public void returnResultCute(int contatto, String contattoHR, int tipoUso,bool sussiste,int eCute) 
        {
            this.valutazione.esposizioneCutanea = sussiste;
            this.valutazione.livelliContattoCutaneo = contatto;
            this.valutazione.livelliContattoCutaneoHR = contattoHR;
            //this.valutazione.tipoUsoCut = tipoUso;
            //this.valutazione.tipoUsoCutHR = contattoHR;
            this.valutazione.ecute = (float)eCute;

            /*
           * finire i calcoli
           */
            float rischio=this.valutazione.getRisch();

            //salvare la valutazione.
            //salvare il file html/doc
           // ValutazioneDoc.salvaFile(this.valutazione, Utils.pathDatabase() + @"valutazioni\");


         
            if (this.valutazione.id <= 0 | this.valutazione.id == null) this.valutazione.id = IncrementalIndex.getNewIndex("nuova valutazione");


            String p = Utils.pathDatabase() + @"valutazioni\valutazione" + DateTime.Now.ToString("_yyyy-MM-dd_HH-mm-ss-fff") + ".doc";

            valutazione.nomeFileOriginale = p;

            DbValutazione.append(this.valutazione);



            //schermata valutazione
            FormValutazione formVal = new FormValutazione(this.valutazione,this);
            formVal.Show();
            formVal.Focus();

            this.Visible = false;
        }

        public void reshowFormSostanza()
        {
            this.Visible = true;
        }

        public void closeFormSostanza() {
            this.Close();
        }

        public void fineValutazioneProcesso(float einal,float metri, int quantita, int tempo, String tipoControlloHR) {
            this.valutazione.einal = einal;
            this.valutazione.distanza = metri;
            this.valutazione.quantita = quantita;
            this.valutazione.tempoInal = tempo;
            this.valutazione.tipoControlloHR = tipoControlloHR;



            float rischio = this.valutazione.getRisch();

            
            //salvare la valutazione.
            if (this.valutazione.id <= 0 | this.valutazione.id == null) this.valutazione.id = IncrementalIndex.getNewIndex("nuova valutazione");

            String p = Utils.pathDatabase() + @"valutazioni\valutazione" + DateTime.Now.ToString("_yyyy-MM-dd_HH-mm-ss-fff") + ".doc";

            valutazione.nomeFileOriginale = p;

            ValutazioneDoc.salvaFile(this.valutazione, p);
            

            DbValutazione.append(this.valutazione);

            //schermata valutazione
            FormValutazione formVal = new FormValutazione(this.valutazione,this);
            formVal.Show();
            formVal.Focus();

            this.Visible=false;
        }


        //_____VALIDAZIONE CAMPI________

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            Match match = Regex.Match(textBox1.Text, @"([^A-Za-z0-9\ \-\.\,\(\)])", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                MessageBox.Show("Nome sostanza contiene un carattere non valido");
                e.Cancel = true;
            }
            else {
                //ok
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //abbandona sostanze
            DialogResult r1 = MessageBox.Show("Sei davvero sicuro?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if(r1==DialogResult.Yes)this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(this.tipo=="sostanza"|this.tipo=="miscelaP")
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
                    AgenteChimico a= (AgenteChimico)comboBox1.SelectedItem;
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

       
        private void FormSostanza_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.father.aggiornaValutazioni(int idlavoratore)
            this.father.RiprendiPossesso();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button7.Enabled = true;
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            button8.Enabled = true;
        }

         private void button12_Click(object sender, EventArgs e)
        {
            CaricaSostanza carica = new CaricaSostanza(this, this.tipo);
            carica.Show();
        }

         private void button10_Click(object sender, EventArgs e)
         {
             if (this.textBox1.Text == "")
             {
                 MessageBox.Show("Il nome della sostanza è vuoto!");
             }
           //  else if (this.textBox2.Text == "" && this.tipo == "sostanza")
           //  {
           //      MessageBox.Show("I'identificativo della sostanza è vuoto!");
           //  }
             else if (listBox4.Items.Count == 0 & checkBox3.Checked==false)
             {
                 MessageBox.Show("Non hai inserito gli elementi necessari a valutare la pericoosità dell'agente chimico. Se l'agente chimico non è pericoloso e non ha valori limite di esposizione professionale allora non è necessaria questa valutazione.");
             }
             else if (!radioButton2.Checked && !radioButton1.Checked && this.tipo == "processo")
             {
                 MessageBox.Show("Non hai indicato il tipo di emissione");
             }
             else
             {
                 this.fineFaseSostanza();
             }
         }

         private void fineFaseSostanza()
         {
             //Avanti - sostanza pericolosa - calcolo del pericolo

             this.agenteChimico.tipo = this.tipo;
             this.agenteChimico.vlep = checkBox3.Checked;
             this.agenteChimico.nome = textBox1.Text;
             this.agenteChimico.identificativo = textBox2.Text;
             //this.agenteChimico.bassaEmissione = !(radioButton1.Checked);
            
             //_____Raccogli le frasi h selezionate______________________
             List<String[]> frasiHAgenteChimico = new List<String[]>(); 
             if(this.tipo == "sostanza" | this.tipo=="miscelaP")
             {
                foreach (string fr in listBox4.Items)//per ogni frase lezionata ne prendo un alla volta
                {
                    string fras = fr.Split('(')[0];//prendo solo il codice senza descrizione
                    for (int i = 0; i < frasiHindex; i++)//per ogni frase h esistente
                    {
                        if (fras == frasih[i, 0]) //se ho trovato quella giusta
                        {
                            String[] frasehTemp = new String[3];
                            frasehTemp[0]=frasih[i, 0];
                            frasehTemp[1]=frasih[i, 1];
                            frasehTemp[2]=frasih[i, 2];
                            frasiHAgenteChimico.Add(frasehTemp);

                            /*
                            f = float.Parse(frasih[i, 2]);//prendo lo score della frase
                            if (f > this.score)//controllo se è più alto 
                            {
                                this.score = f;//se è più alto lo assegno
                            }
                            */
                        }
                    }
                 }
                 this.agenteChimico.frasiH = frasiHAgenteChimico;
             }
             else if (this.tipo == "processo" | this.tipo == "miscelaNP")
             {
                 List<int> componenti = new List<int>();
                 foreach (AgenteChimico ac in listBox4.Items)
                 {
                     componenti.Add(ac.id);
                 }
                 this.agenteChimico.sostanze = componenti.ToArray();
             }

             if (this.tipo == "processo")
             {
                 
                 this.agenteChimico.altaemissione = radioButton1.Checked;

                 /*
                 List<KeyValuePair<int, Boolean>> emissione = new List<KeyValuePair<int, bool>>();
                 int index = 0;

                 
                 FormEmissione fm = new FormEmissione(this, this.agenteChimico);
                 DialogResult dr = fm.ShowDialog();
                 if(dr == DialogResult.OK){
                     MessageBox.Show("E' stato assegnato un valore di emissione ad ogni agente chimico");
                 }
                 */
             }
            
             //CALCOLO DELLO SCORE
             this.score = this.agenteChimico.getScore();

             //CONTROLLO VLEP
             // se è una sostanza allora è minimo 3 se è una miscela minimo 2,25

             
             //___________________aggiunte nel treeview_________________________
             if (this.tipo == "sostanza")
             {
                treeView2.Nodes.Add("sostanza", textBox1.Text);
                treeView2.Nodes["sostanza"].Nodes.Add("Identificativo: " + textBox2.Text);
                treeView2.Nodes["sostanza"].Nodes.Add("Score: " + this.score.ToString());
                if (checkBox3.Checked == true)
                 {
                     //è un vlep
                     treeView2.Nodes["sostanza"].Nodes.Add("VLEP: " + "Sì");
                 }
                else
                 {
                     //non è un vlep
                     treeView2.Nodes["sostanza"].Nodes.Add("VLEP: " + "No");
                 }
                treeView2.Nodes["sostanza"].Nodes.Add("frasi", "Frasi H:");
                foreach (String fr in listBox4.Items)
                 {
                     treeView2.Nodes["sostanza"].Nodes["frasi"].Nodes.Add(fr);
                 }
                treeView2.Nodes.Add("inalatoria", "Esposizione inalatoria:");
                treeView2.ExpandAll();
             }
             else if (this.tipo == "miscelaP")
             {
                 treeView2.Nodes.Add("sostanza", textBox1.Text);
                 treeView2.Nodes["sostanza"].Nodes.Add("Score: " + this.score.ToString());
                 treeView2.Nodes["sostanza"].Nodes.Add("frasi", "Frasi H:");
                 foreach (String fr in listBox4.Items)
                 {
                     treeView2.Nodes["sostanza"].Nodes["frasi"].Nodes.Add(fr);
                 }
                 treeView2.Nodes.Add("inalatoria", "Esposizione inalatoria:");
                 treeView2.ExpandAll();
             }
             else if (this.tipo == "miscelaNP")
             {
                 treeView2.Nodes.Add("sostanza", textBox1.Text);
                 treeView2.Nodes["sostanza"].Nodes.Add("Score: " + this.score.ToString());
                 treeView2.Nodes["sostanza"].Nodes.Add("frasi", "Sostanze:");
                 foreach (AgenteChimico ac in listBox4.Items)
                 {
                     treeView2.Nodes["sostanza"].Nodes["frasi"].Nodes.Add(ac.nome);
                 }
                 treeView2.Nodes.Add("inalatoria", "Esposizione inalatoria:");
                 treeView2.ExpandAll();
             }
             else if (this.tipo == "processo")
             {
                 treeView2.Nodes.Add("sostanza", textBox1.Text);
                 treeView2.Nodes["sostanza"].Nodes.Add("Score: " + this.score.ToString());
                 treeView2.Nodes["sostanza"].Nodes.Add("frasi", "Sostanze:");
                 foreach (AgenteChimico ac in listBox4.Items)
                 {
                     treeView2.Nodes["sostanza"].Nodes["frasi"].Nodes.Add(ac.nome);
                 }
                 treeView2.Nodes.Add("inalatoria", "Esposizione inalatoria:");
                 treeView2.ExpandAll();
             }
             else MessageBox.Show("errore nel tipo di valutazione");


             //_SALVA LA SOSTANZA NEL DATABASE PERSONALE_
             if (checkBox1.Checked == true)
             {
                 //se è nuovo 
                 if (this.agenteChimico.id==0) 
                 {
                     this.agenteChimico.id = IncrementalIndex.getNewIndex("nuovo agente chimico");
                 }
                 DbAgentiChimici.append(this.agenteChimico);
             }

             panel3.Visible = false;

             this.valutazione.ac = this.agenteChimico;

             //____AVANTI____________________________
             if (this.tipo == "sostanza" | this.tipo == "miscelaP" | this.tipo == "miscelaNP")
             {
                 this.faseInalatoria();
             }
             else if (this.tipo == "processo")
             {
                 this.faseInalatoriaProcesso();
             }
             else MessageBox.Show("errore nel tipo di valutazione");
         }

         public void setAgenteChimico(AgenteChimico ac) 
         {
             this.agenteChimico = ac;
         }

         public void caricaAgenteChimico(AgenteChimico ac)
         {
             if (ac == null) return;
             textBox1.Text = ac.nome;
             textBox2.Text = ac.identificativo;
             checkBox3.Checked = ac.vlep;
             //radioButton1.Checked = (!ac.bassaEmissione)&(ac.tipo=="processo");

             checkBox1.Checked = false;

             if (this.tipo == "sostanza" | this.tipo == "miscelaP")
             {
                 //Aggiungi frasi h
                 foreach (String[] frase in ac.frasiH)
                 {
                     if (frase[0] != "") { listBox4.Items.Add(frase[0]); }
                 }
             }
             else if (this.tipo=="miscelaNP" | this.tipo == "processo")
             {
                 listBox4.ValueMember = "Id";
                 listBox4.DisplayMember = "Nome";
                 listBox4.DataSource = ac.getComponentiMiscela();
             }
         }

         private void button8_Click(object sender, EventArgs e)
         {
             //rimuovi selezionato frasih
             listBox4.Items.Remove(listBox4.SelectedItem);
         }

         public void aggiornaTreeView(String nome,String identificativo,String score,Boolean vlep){

             treeView2.Nodes.Add("sostanza", nome);
             if(identificativo!="" && identificativo!=null)treeView2.Nodes["sostanza"].Nodes.Add("Identificativo: " + identificativo);
             treeView2.Nodes["sostanza"].Nodes.Add("Score: " + score);
             if (vlep)//checkBox3.Checked == true
                 {
                     //è un vlep
                     treeView2.Nodes["sostanza"].Nodes.Add("VLEP: " + "Sì");
                 }
             else
                 {
                     //non è un vlep
                     treeView2.Nodes["sostanza"].Nodes.Add("VLEP: " + "No");
                 }
            treeView2.Nodes["sostanza"].Nodes.Add("frasi", "Frasi H:");
            foreach (String fr in listBox4.Items)
                 {
                     treeView2.Nodes["sostanza"].Nodes["frasi"].Nodes.Add(fr);
                 }
            treeView2.Nodes.Add("inalatoria", "Esposizione inalatoria:");
            treeView2.ExpandAll();
        }




         public void aggiornaSostanze () 
         {
             if (this.tipo == "processo" | this.tipo == "miscelaNP")
             {
                 
                 List<AgenteChimico> agenti = new List<AgenteChimico>();
                 agenti = DbAgentiChimici.getOfType("sostanza");

                 comboBox1.DisplayMember = "Nome";
                 comboBox1.ValueMember = "Id";
                 comboBox1.DataSource = agenti;

                 listBox4.ValueMember = "Id";
                 listBox4.DisplayMember = "Nome";

             }
         }




















        private void button10_Validating(object sender, CancelEventArgs e)
        {
        }
        private void FormSostanza_Click(object sender, EventArgs e)
        {
        }
        private void FormSostanza_Leave(object sender, EventArgs e)
        {
        }
        private void button11_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button10_Validated(object sender, EventArgs e)
        {
        }
        private void label8_Click(object sender, EventArgs e)
        {
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void button9_Click(object sender, EventArgs e)
        {
            //RIMUOVI TUTTO
            listBox4.Items.Clear();
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //EVENTUALE CONTROLLO NOMI DUPLICATI
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCreaSostanza fcs = new FormCreaSostanza(this,"sostanza");
            fcs.Show();
        }

       
    }
}
