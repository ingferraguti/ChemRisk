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
    public partial class FormAreaLavoratore : Form
    {
        private Form1 father;
        private int idAzienda;
        private int idArea;
        private String stato="nuovo";
        private Area newArea; 

        public Area getArea() {return newArea;}


        public FormAreaLavoratore(Form1 f, int azienda)
        {
            InitializeComponent();
            this.father = f;
            this.idAzienda = azienda;
        }

        public FormAreaLavoratore(Form1 f, int azienda, int idA, String stato ) { 
            //modifica
            InitializeComponent();
            this.father = f;
            this.idAzienda = azienda;
            this.idArea = idA;
            this.stato = stato;
            Area a = Utils.findAreaById(this.idArea);
            this.textBox1.Text = a.nome;
            this.textBox2.Text = a.note.Replace("#@#", System.Environment.NewLine);
        }

        private void FormAreaLavoratore_Load(object sender, EventArgs e)
        {
            label5.Text = Utils.findDenominazioneAzienda(this.idAzienda);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //Si è cliccato SALVA !
            String nome = textBox1.Text;
            String note = textBox2.Text;

            note = note.Replace(System.Environment.NewLine,"#@#");

            if (this.stato=="nuovo"){
                this.newArea = new Area(IncrementalIndex.getNewIndex("nuova area"), this.idAzienda, nome, note);//per ora muore qui
                father.salvaNuovaArea();
            }
            else{
                this.newArea = new Area(this.idArea,this.idAzienda,nome,note);
                father.sostituisciArea(newArea);
            }
            
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void FormAreaLavoratore_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.father.chiusoEditArea();
        }








        //____________VUOTI_______________
        private void FormAreaLavoratore_Leave(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
