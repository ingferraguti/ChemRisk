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
using System.Web;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movarisch1
{
    public partial class Form1 : Form
    {
        //proprietà
        private Form2 due = new Form2();
        private FormAreaLavoratore formArea;
        private FormLavoratore formLav;
        private FormSostanza formSostanza;
        //private FormVal formVal;
        //private ValutazioneGuidata formVG;

        //private Azienda[] aziende=new Azienda[99];
        //private int aziendeIndex;

        int idAvvio = IncrementalIndex.getNewIndex("avvio applicazione");

        String selectedArea = null;

        
      

        //costruttore
        public Form1()
        {
            InitializeComponent();
        }


        //_____LOAD_____
        private void Form1_Load(object sender, EventArgs e)
        {
            //ridimensiona
            //this.Size = new Size(900, 500);
            this.ricaricaAziende();

            //Backup b = new Backup();
            //b.Show();



            //____________________


            /*
            try
            {

                var url = "http://www.ferragutimatteo.it/mova/control.php?id=" + DbConf.getID();//Paste ur url here  

                if (DbConf.test())
                {
                    MessageBox.Show(url);
                }

                WebClient webClient = new WebClient();

                string reply = webClient.DownloadString(url);
                if(DbConf.test()){
                    MessageBox.Show(reply);
                }
                
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        if (DbConf.test())
                        {
                            MessageBox.Show("HTTP Status Code: " + (int)response.StatusCode);
                        }
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
            }
            */

            //Console.WriteLine(reply);

/*
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HostURI);
request.Method = "GET";
String test = String.Empty;
using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
{
    Stream dataStream = response.GetResponseStream();
    StreamReader reader = new StreamReader(dataStream);
    test = reader.ReadToEnd();
    reader.Close();
    dataStream.Close();
 }

            */

            

          
        }

        private void ricaricaAziende() 
        {
            listBox1.Items.Clear();
            Azienda[] aziendeCaricate = new Azienda[0];
            try {
                aziendeCaricate = DbAziende.retrieve().ToArray();
            }
            catch {
                System.Threading.Thread.Sleep(100);
            }
            
            //this.aziendeIndex = Utils.numAziende();
            listBox1.DisplayMember = "Nome";
            listBox1.ValueMember = "Id";
            if (aziendeCaricate != null) listBox1.Items.AddRange(aziendeCaricate);
            //TOOLTIP????note
            listBox1.Sorted = true;
        }

       


        //_____BOTTONI__________________________________________________


        private void button5_Click(object sender, EventArgs e)
        {
            //aggiungi azienda
            this.due = new Form2(this);
            this.due.Show();
            this.Enabled = false;
        }



        //____VALITAZIONE GUIDATA______________________________________________
        private void button6_Click(object sender, EventArgs e)
        {
            int result;


            if (int.TryParse(treeView1.SelectedNode.Tag.ToString(), out result))
            {
               
                //Chiamo valutazione guidata
                panel1.Visible = true;

                //panel2.Visible = false;
                panel2.Enabled = false;
                panel1.Location = new Point(250, 29);
            }
            else {
                MessageBox.Show("Non è stato selezionato un lavoratore");
            }
        }

        //_______SOSTANZA PERICOLOSA____________________________________________
        private void button1_Click(object sender, EventArgs e)
        {
            //Chiamo procedura guidata sostanza periocolosa
            if (treeView1.SelectedNode.Level==1){
                //______________

                this.formSostanza = new FormSostanza(this, int.Parse(treeView1.SelectedNode.Tag.ToString()),"sostanza");
                this.formSostanza.Show();
                this.panel1.Enabled = false;
            }
           
            //this.formVG = new ValutazioneGuidata();
            //this.formVG.Show();

            //this.Enabled = false;
        }

        //_____MISCELA PERICOLOSA____
        private void button2_Click(object sender, EventArgs e)
        {
            //chiamo procedura guidata miscela pericolosa
            //frasi h

            if (treeView1.SelectedNode.Level == 1)
            {
                //______________

                this.formSostanza = new FormSostanza(this, int.Parse(treeView1.SelectedNode.Tag.ToString()),"miscelaP");
                this.formSostanza.Show();
                this.panel1.Enabled = false;
            }

        }

        //___________MISCELA NON PERICOLOSA_________________________
        private void button3_Click(object sender, EventArgs e)
        {
            //chiamo procedura guidata
            this.formSostanza = new FormSostanza(this, int.Parse(treeView1.SelectedNode.Tag.ToString()), "miscelaNP");
            this.formSostanza.Show();
            this.panel1.Enabled = false;

        }

        //___________PROCESSO________________________________________
        private void button4_Click(object sender, EventArgs e)
        {
            //chiamo procedura guidata
            this.formSostanza = new FormSostanza(this, int.Parse(treeView1.SelectedNode.Tag.ToString()), "processo");
            this.formSostanza.Show();
            this.panel1.Enabled = false;
        }

        //__________________________________________________________________________



        private void button8_Click(object sender, EventArgs e)
        {
            //aggiungi lavoratore
            Azienda a = (Azienda)listBox1.SelectedItem;
            //int idAzienda= Utils.findIdAzienda(listBox1.SelectedItem.ToString());
            int idArea = Utils.findIdArea(this.selectedArea, a.id);
            this.formLav = new FormLavoratore(this, a.id, idArea);
            this.formLav.Show();
            this.Enabled = false;
           
        }
        private void button7_Click(object sender, EventArgs e)
        {
            // aggiungi area
            //USARE ID
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Non ha selezionato l'area");
            }
            else
            {
                Azienda a = (Azienda)listBox1.SelectedItem;
                this.formArea = new FormAreaLavoratore(this, a.id);//Utils.findIdAzienda(listBox1.SelectedItem.ToString())
                formArea.Show();
                this.Enabled = false;
            }
        }

        private void ricaricaAree()
        {
            //if (listBox1.SelectedIndex == -1) MessageBox.Show("non hai selezionato nulla");
            if (! (listBox1.SelectedIndex == -1) )
            {
                Azienda selezionata = (Azienda)listBox1.SelectedItem;

                //cancella tutto
                treeView1.Nodes.Clear();

                //RICARICA aree
                //Area[] aree = Utils.caricaAree();
                //int numAree = Utils.contaAree();
                List<Area> aree = DbAree.retrieve();

                Lavoratore[] lavoratori = Utils.caricaLavoratori();


                if (listBox1.SelectedItem != null)
                {
                    foreach (Area a in aree)
                    {
                        if (a.azienda == selezionata.id)
                        {
                            //MessageBox.Show(a.id.ToString());
                            treeView1.Nodes.Add(a.id.ToString(), a.nome);
                            //bisogna già mettere i lavoratori

                            //int numLavoratori = Utils.contaLavoratori();
                            TreeNode[] lav = new TreeNode[49];
                            int indxLav = 0;

                            //for (int j = 0; j < numLavoratori; j++)
                            for (int j = 0; j < lavoratori.Length; j++)
                            {
                                if(lavoratori[j]!=null)
                                {
                                    if (lavoratori[j].area == a.id)
                                    {
                                        String nome = lavoratori[j].cognome + " " + lavoratori[j].nome;

                                        TreeNode l = new TreeNode();
                                        l.Tag = lavoratori[j].id;
                                        l.Text = nome;

                                        treeView1.Nodes[a.id.ToString()].Nodes.Add(l);
                                        //lav[indxLav]=new TreeNode();
                                        indxLav++;
                                    }
                                }
                            }
                        }
                    }
                    treeView1.Sort();
                    treeView1.ExpandAll();
                }
                else
                {
                    //niente
                }
            }
        }





        // AFTER SELECT

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            //selezione nel tree view
            if(treeView1.SelectedNode.Level==0){
                //se è selezionata una area

                button8.Visible = true;
                button6.Visible = false;

                listBox2.Items.Clear();

                selectedArea = treeView1.SelectedNode.Text;
                //MessageBox.Show("area!");
            }
            else if (treeView1.SelectedNode.Level==1)
            {
                //se è selezionato un lavoratore
                button8.Visible = false;
                button6.Visible = true;
                //MessageBox.Show(treeView1.SelectedNode.Index.ToString());
                this.aggiornaValutazioni();//lavoratore selezionato invece di zero int.Parse()

            }


        }

        private void aggiornaValutazioni()
        {
            //MessageBox.Show("LAvoratore!");
            if (!(treeView1.SelectedNode == null))
            {
                int idlavoratore = int.Parse(treeView1.SelectedNode.Tag.ToString());
                //MessageBox.Show(""+idlavoratore.ToString());
                if (idlavoratore == 0) { }
                else
                {
                    listBox2.Items.Clear();

                    //mostro le valutazioni
                    listBox2.ValueMember = "Id";
                    listBox2.DisplayMember = "Nome";

                    List<Valutazione> listV = DbValutazione.retrieve();
                    if (listV != null && listV.Count > 0)
                    {
                        foreach (Valutazione v in listV)
                        {
                            if (v.idLavoratore == idlavoratore)//  == idlavoratore invece di !=0
                            {
                                listBox2.Items.Add(v);
                            }
                        }
                    }
                }
            }
    
        }

        private void aggiornaValutazioni(String boh)
        {
            if (!(treeView1.SelectedImageIndex == -1))
            {
                int idlavoratore = 0;
                if (treeView1.SelectedNode.Level == 1) idlavoratore = int.Parse(treeView1.SelectedNode.Tag.ToString());

                listBox2.Items.Clear();

                //mostro le valutazioni
                listBox2.ValueMember = "Id";
                listBox2.DisplayMember = "Nome";

                List<Valutazione> listV = DbValutazione.retrieve();
                if (listV != null && listV.Count > 0)
                {
                    foreach (Valutazione v in listV)
                    {
                        if (v.idLavoratore == idlavoratore)//  == idlavoratore invece di !=0
                        {
                            listBox2.Items.Add(v);

                        }
                    }
                }

            }
            //else MessageBox.Show("tree view non selezionato");



        }



        // _____MIE FUNZIONI_____
        //______SALVA NEI FILE___
        public void salvaAziendaModificata() 
        { 
            Azienda newAzienda = due.getAzienda();
            if (!newAzienda.Equals(null))
            {
                //this.aziende[cercaAzienda(newAzienda.denominazione)] = newAzienda; //CRASHA!!!!

                try
                {
                    DbAziende.delete(newAzienda.id);
                    System.Threading.Thread.Sleep(500);
                    DbAziende.append(newAzienda);
                }
                catch { MessageBox.Show("salvataggio non riuscito"); }
                //ricarica aziende
                this.ricaricaAziende();
            }
            else { MessageBox.Show("oggetto azienda null"); }
        }

        public void salvaNuovaAzienda()
        {
            Azienda newAzienda = due.getAzienda();
            bool risultato = false;
            if (newAzienda!=null)
            {
                System.Threading.Thread.Sleep(500);
                risultato = DbAziende.append(newAzienda);
                if (!risultato) { MessageBox.Show("problema nel salvataggio"); }
            }
            else { MessageBox.Show("oggetto azienda null"); }
            this.ricaricaAziende();
            this.ricaricaAree();
        }

        public void salvaNuovaArea()
        {
            this.salvaNelFileAree(this.formArea.getArea());
            this.ricaricaAree();
        }

        public void sostituisciArea(Area a) { 
            //sostituisci nel file
            if (a != null && a.id != 0)
            {
                Utils.salvaAreaModificata(a);
            }
            else {
                MessageBox.Show("ERRORE: ho perso l'id");
            }
            this.ricaricaAree();
        }

        public void salvaLavoratore() { 
            //salva nuovo lavoratore
            Lavoratore nuovo = this.formLav.getLavoratore();
            
            //aggiungi alla parte grafica
            this.salvaNelFileLavoratori(nuovo);
            this.ricaricaAree();

        }

        public void eliminaLavoratore(int idLavoratoreEliminare) {
            DbLavoratori.elimina(idLavoratoreEliminare);
            this.ricaricaAree();
        }

        public void salvaNelFileLavoratori(Lavoratore l) {
            DbLavoratori.save(l);
        }

        private void salvaNelFileAziende(Azienda newAzienda)
        {
            // salva nel file
            using (FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Append))
                using (StreamWriter w = new StreamWriter(streamAziende))
                {
                    w.WriteLine(newAzienda.id);
                    w.WriteLine(newAzienda.denominazione);
                    w.WriteLine(newAzienda.indirizzo);
                    w.WriteLine(newAzienda.cap);
                    w.WriteLine(newAzienda.comune);
                    w.WriteLine(newAzienda.provincia);
                    w.WriteLine(newAzienda.contatto);
                    w.WriteLine(newAzienda.telefono);
                    w.WriteLine(newAzienda.email);
                    w.WriteLine(newAzienda.piva);
                    w.WriteLine("###############");
                } 
        }

        private void salvaNelFileAree(Area newArea)
        {
            // salva nel file
            Utils.salvaNelFileAree(newArea);
        }

        public void chiusoEditAzienda() 
        {
            this.RiprendiPossesso1();
        }

        public void chiusoFormVal()
        {
            this.RiprendiPossesso();
        }

        public void RiprendiPossesso() 
        {
            this.Enabled = true;
            this.panel1.Visible = false;
            this.panel1.Enabled = true;
            this.panel2.Enabled = true;
            this.panel2.Visible = true;
            //this.Focus();
            this.aggiornaValutazioni();
        }

        public void RiprendiPossesso1()
        {
            this.Enabled = true;
            this.panel1.Visible = false;
            this.panel1.Enabled = true;
            this.panel2.Enabled = true;
            this.panel2.Visible = true;
            this.Focus();
            this.aggiornaValutazioni();
        }

        public void chiusoEditArea()
        {
            this.RiprendiPossesso();
        }

        //_____SelectedIndexChanged_____

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Visible = false;
            button7.Visible = true;
            button8.Visible = false;
            button10.Enabled = true;
            //seleziono una azienda
            //refresh treeview

            //cancella tutto
            treeView1.Nodes.Clear();
            listBox2.Items.Clear();
            
            //ricarica la lista
            this.ricaricaAree();
        }




        //_____LINK____
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Abbandona inizio procedura guidata
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.Enabled = true;
        }








        //_____MENU  AZIENDA_____

        private void modificaDatiAziendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Modifica azienda
            if (listBox1.SelectedItem != null)
            {
                Azienda daModificare = (Azienda)listBox1.SelectedItem;
                int indice = daModificare.id;
                if (indice <= 0) MessageBox.Show("oggetto azienda null");
                else
                {
                    //MessageBox.Show(" " + indice.ToString());
                    this.due = new Form2(this, daModificare);
                    due.Show();
                    this.Enabled = false;
                }
            }
            else { MessageBox.Show("Non hai selezionato nessuna azienda"); }

        }

        private void eliminaTuttiIDatiRelativiAQuestaAziendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //elimina una zienda
            if (listBox1.SelectedItem != null)
            {
                DialogResult r1 = MessageBox.Show("Vuoi veramente eliminare", "Conferma di eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r1 == DialogResult.Yes) { 
                    //bisogna eliminare l'azienda;
                    Azienda daEliminare = (Azienda) listBox1.SelectedItem;
                    DbAziende.delete(daEliminare.id);
                   
                }
                else
                {
                    //non succede nulla e si può continuare con l'inserimento dati
                }
            }
            else { MessageBox.Show("Non hai selezionato nessuna azienda"); }
            System.Threading.Thread.Sleep(700);
            this.ricaricaAziende();
        }

      




        //menu area lavoratore
        private void modificaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            if (!(tn == null))
            {
                //modifica area
                if (treeView1.SelectedNode.Level == 0)
                {

                    //se è selezionata una area
                    //String nomeareadamodificare = this.selectedArea;
                    //int idArea = ((Area)treeView1.SelectedNode).id;
                    int idArea = int.Parse(treeView1.SelectedNode.Name);
                    int idAzienda = ((Azienda)listBox1.SelectedItem).id;
                    this.formArea = new FormAreaLavoratore(this, idAzienda, idArea, "modifica");
                    formArea.Show();
                    this.Enabled = false;

                }
                else if (treeView1.SelectedNode.Level == 1)
                {
                    //si è selezionato un lavoratore 
                    int idLavoratore = int.Parse(treeView1.SelectedNode.Tag.ToString());
                    Lavoratore l = Utils.findLavoratore(idLavoratore);
                    int idArea = l.area;
                    int idAzienda = l.idazienda;
                    this.formLav = new FormLavoratore(this, idAzienda, idArea, idLavoratore);
                    this.formLav.Show();
                    this.Enabled = false;
                }
                
            }
            else
            {
                MessageBox.Show("Non hai selezionato nulla");
            }
        }




        private void button10_Click_1(object sender, EventArgs e)
        {
            //MODIFICA DATI AZIENDA
            if (listBox1.SelectedItem != null)
            {
                Azienda daModificare = (Azienda) listBox1.SelectedItem;
                int indice = daModificare.id;

                if (indice <= 0) MessageBox.Show("oggetto azienda null");
                else
                {
                    this.due = new Form2(this, daModificare);
                    due.Show();
                    this.Enabled = false;
                }
            }
            else MessageBox.Show("Non hai selezionato nessuna azienda"); 
        }

        private void eliminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ELIMINA AREA O LAVORATORE
            DialogResult r1 = MessageBox.Show("Vuoi veramente eliminare", "Conferma di eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r1 == DialogResult.Yes)
            {



                //selezione nel tree view
                if(treeView1.SelectedNode.Level==0)
                {
                   //se è selezionata una area !!!!!!
                    DbAree.elimina(int.Parse(treeView1.SelectedNode.Name));//ID
               }
               else if (treeView1.SelectedNode.Level == 1)
                {
                    //se è selezionato un lavoratore
                    DbLavoratori.elimina(int.Parse(treeView1.SelectedNode.Tag.ToString()));//ID
                }


                //ridisegna il tree view
                this.ricaricaAree();
 



            }
            else
            {
                //non succede nulla e si può continuare con l'inserimento dati
            }

            


        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
        }


       //_________________________________________________________________________________
       //_________________________________________________________________________________
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) { }
        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private void panel2_Paint(object sender, PaintEventArgs e)        {

            
        
        
        
        }
        private void modificaToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void textBox1_Validating(object sender, CancelEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void button18_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void button11_Click(object sender, EventArgs e) { }
        private void button14_Click(object sender, EventArgs e) { }
        private void button15_Click(object sender, EventArgs e) { }
        private void button13_Click(object sender, EventArgs e) { }
        private void button10_Click(object sender, EventArgs e) { }
        private void button19_Click(object sender, EventArgs e) { }
        private void aggiungiModificaAreeELavoratoriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
       

        private void modificaAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void modificaLavoratoreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eliminaValutazioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //elimina valutazione selezionata

            if (listBox1.SelectedItem != null)
            {
                DialogResult r1 = MessageBox.Show("Vuoi veramente eliminare? L'operazione non è reversibile.", "Conferma di eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r1 == DialogResult.Yes)
                {
                    Valutazione daEliminare = (Valutazione)listBox2.SelectedItem;
                    if (daEliminare != null)
                    {
                        DbValutazione.delete(daEliminare.id);
                    }
                    else {
                        MessageBox.Show("Non hai selezionato la valutazione da eliminare");
                    }
                    
                    this.aggiornaValutazioni();
                }
                else
                {
                    //non succede nulla e si può continuare con l'inserimento dati
                }
            }
            else { MessageBox.Show("Non hai selezionato nulla"); }

          
        }

        private void agentiChimiciToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void sostanzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaricaSostanza carica = new CaricaSostanza("sostanza");
            carica.Show();
        }

        private void pericoloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaricaSostanza carica = new CaricaSostanza("miscelaP");
            carica.Show();
        }

        private void nonPericoloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaricaSostanza carica = new CaricaSostanza("miscelaNP");
            carica.Show();
        }

        private void processiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaricaSostanza carica = new CaricaSostanza("processo");
            carica.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //APRI
            Valutazione v = (Valutazione) listBox2.SelectedItem;
            FormValutazione fv = new FormValutazione(v);
            fv.Show();


            //DA TOGLIERE
            //GraficoStatoFisico a = new GraficoStatoFisico();
            //a.Show();
            //SpiegazioneTipoUso b = new SpiegazioneTipoUso();
            //b.Show();
            //SpiegazioneTipoControllo c = new SpiegazioneTipoControllo();
            //c.Show();
        }

        private void aiutoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

          
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Versione Novembre 2024");
        }

        private void corsiFormazioneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void modelloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modello m = new Modello();
            m.Show();
        }

        private void copyrightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copyright c = new Copyright();
            c.Show();
        }

       
       

        

        


        
    }
}
