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
    public partial class FormLavoratore : Form
    {
        //
        private Form1 father;
        private String stato = "nuovo";
        private Lavoratore newLavoratore;
        private int idAzienda;
        private int idArea;
        private int idLavoratoreModifica;

        //COSTRUTTORE
        public FormLavoratore(Form1 f,int idAzienda, int idArea)
        {
            //NUOVA AREA
            InitializeComponent();
            this.father = f;
            this.idAzienda = idAzienda;
            this.idArea = idArea;
            this.label7.Text = Utils.findDenominazioneAzienda(idAzienda);
            this.label8.Text = Utils.findNomeArea(idArea);
        }

        
        public FormLavoratore(Form1 f, int idAzienda, int idArea, int idLavoratore)
        {
            //NUOVA AREA
            InitializeComponent();
            this.stato = "modifica";
            this.father = f;
            this.idAzienda = idAzienda;
            this.idArea = idArea;
            this.idLavoratoreModifica = idLavoratore;
            this.label7.Text = Utils.findDenominazioneAzienda(idAzienda);
            this.label8.Text = Utils.findNomeArea(idArea);

            Lavoratore l = Utils.findLavoratore(idLavoratore);
            if (l != null)
            {
                this.textBox1.Text = l.nome;
                this.textBox2.Text = l.cognome;
                this.textBox3.Text = l.note.Replace("#@#", System.Environment.NewLine); ;
            }
            else {
                MessageBox.Show("Non è stato possibile trovare il lavoratore specificato");
            }
        }

        public Lavoratore getLavoratore(){
            return this.newLavoratore;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SALVA

            String note = textBox3.Text;
            note = note.Replace(System.Environment.NewLine, "#@#");

            String nome = textBox1.Text;
            String cognome = textBox2.Text;

            if (this.stato == "nuovo")
            {
                this.newLavoratore = new Lavoratore(
                    IncrementalIndex.getNewIndex("nuovo lavoratore"), // ID lavoratore
                    this.idAzienda,
                    this.idArea,
                    nome,
                    cognome,
                    note);
                this.father.salvaLavoratore();
            }
            else if (this.stato == "modifica") {
                //creato oggetto lavoratore modificato
                this.newLavoratore = new Lavoratore(this.idLavoratoreModifica,this.idAzienda,this.idArea,nome,cognome,note);

                //eliminare versione lavoratore obsoleta
                this.father.eliminaLavoratore(this.idLavoratoreModifica);

                //salvare il lavoratore modificato
                this.father.salvaLavoratore();
            }
            else
            {
                MessageBox.Show("modalità non implementato");
            }

            this.Close();
        }

        private void FormLavoratore_FormClosing(object sender, FormClosingEventArgs e)
        {
            father.RiprendiPossesso();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //ABBANDONA

            this.Close();
        }










        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //valida?
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormLavoratore_Load(object sender, EventArgs e)
        {

        }
    }
}
