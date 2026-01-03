using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Movarisch1;

namespace Movarisch1
{
    public partial class Form2 : Form
    {
        //attributi
        private Form1 father;
        private Azienda az;
        private String mode;
        private int idAzienda;

        //costruttori
        public Form2()
        {
            InitializeComponent();
        }
        public Form2( Form1 f )
        {
            InitializeComponent();
            this.father = f;
            this.mode = "nuova";
        }
        public Form2(Form1 f,Azienda a)
        {
            InitializeComponent();
            this.father = f;
            this.az = a;
            this.mode = "modifica";
            this.idAzienda = a.id;

            textBoxDenominazione.Text=a.denominazione;
            textBoxIndirizzo.Text=a.indirizzo;
            textBoxCap.Text=a.cap;
            textBoxComune.Text=a.comune;
            textBoxProvincia.Text=a.provincia;
            textBoxNominativo.Text=a.contatto;
            textBoxTelefono.Text=a.telefono;
            textBoxEmail.Text=a.email;
            textBoxPiva.Text = a.piva;
        }

        //metodi
        public Azienda getAzienda() {
            return this.az;
        }

        //Controlli interfaccia utente
        private void button1_Click(object sender, EventArgs e)
        {
            //Si è cliccato SALVA !
            int id = 0;
            String nome = "";
            String indi = "";
            String capp = "";
            String citt = "";
            String prov = "";
            String pers = "";
            String tell = "";
            String mail = "";
            String ivaa = "";


            if (!string.IsNullOrEmpty(textBoxDenominazione.Text)) { 
                nome = textBoxDenominazione.Text;
                if (!string.IsNullOrEmpty(textBoxIndirizzo.Text)) { indi = textBoxIndirizzo.Text; }
                if (!string.IsNullOrEmpty(textBoxCap.Text)) { capp = textBoxCap.Text; }
                if (!string.IsNullOrEmpty(textBoxComune.Text)) { citt = textBoxComune.Text; }
                if (!string.IsNullOrEmpty(textBoxProvincia.Text)) { prov = textBoxProvincia.Text; }
                if (!string.IsNullOrEmpty(textBoxNominativo.Text)) { pers = textBoxNominativo.Text; }
                if (!string.IsNullOrEmpty(textBoxTelefono.Text)) { tell = textBoxTelefono.Text; }
                if (!string.IsNullOrEmpty(textBoxEmail.Text)) { mail = textBoxEmail.Text; }
                if (!string.IsNullOrEmpty(textBoxPiva.Text)) { ivaa = textBoxPiva.Text; }


                Azienda az1 = new Azienda(id,nome, indi, capp, citt, prov, pers, tell, mail, ivaa);
                this.az = az1;
                if(this.mode=="nuova"){
                    az1.id = IncrementalIndex.getNewIndex("nuova azienda");
                    System.Threading.Thread.Sleep(300);
                    father.salvaNuovaAzienda();
                }
                else if(this.mode=="modifica"){
                    az1.id = this.idAzienda;
                    father.salvaAziendaModificata();
                }
                
                this.Hide(); 
                this.Close();
            }
            else
            {
                DialogResult r1 = MessageBox.Show("Denominazione non indicata! Vuoi abbandonare l'inserimento?", "Errore", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r1 == DialogResult.Yes) { this.Hide();  this.Close(); }
                else {
                   //non succede nulla e si può continuare con l'inserimento dati
                }
            }
            
        }

        private void button1_Leave(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.father.chiusoEditAzienda();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void textBoxDenominazione_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
