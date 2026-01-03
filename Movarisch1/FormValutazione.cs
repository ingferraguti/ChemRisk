using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movarisch1
{
    public partial class FormValutazione : Form
    {
        private Valutazione v;
        private FormSostanza father;
        private Boolean modifica=false;

        public FormValutazione(Valutazione val, FormSostanza f)
        {
            InitializeComponent();
            this.v = val;
            this.father = f;
        }
        public FormValutazione(Valutazione val)
        {
            InitializeComponent();
            this.v = val;
            button2.Visible = false;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Documento di testo .doc|*.doc";
            saveDialog.Title = "salva valutazione con nome";
            saveDialog.DefaultExt = "doc";
            DialogResult result = saveDialog.ShowDialog();

            if(result == DialogResult.OK & saveDialog.FileName != "" & saveDialog.FileName != null){
                
                ValutazioneDoc.salvaFile(this.v, saveDialog.FileName);
            }
        }

        private void FormValutazione_Load(object sender, EventArgs e)
        {
            Lavoratore l = DbLavoratori.findLavoratore(v.idLavoratore);
            Azienda a = DbAziende.find(l.idazienda);
            Area ar = DbAree.find(l.area);

            label14.Text=a.denominazione;//azienda
            label15.Text=ar.nome;//area 
            label16.Text=l.cognome+" "+l.nome;//nome cognome
            label17.Text=v.ac.nome+" "+v.ac.identificativo;//agentechimico
            label18.Text=v.data.ToShortDateString();//data

            label10.Text = v.rInal.ToString();
            label12.Text = v.rCute.ToString();
            label11.Text = v.getRisch().ToString();

            labelEinal.Text = v.einal.ToString();
            labelRisk.Text = v.ac.getScore().ToString();

            labelEcute.Text = v.ecute.ToString();
            labelRisk2.Text = labelRisk.Text;

            label26.Text = v.getTitoloValutazione();
            label27.Text = v.getDescrizioneValutazione();

            //DbValutazione.append(v);
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            if(!(this.father==null)){
                this.father.closeFormSostanza();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.modifica = true;
            this.father.Visible = true;
            this.Close();
        }

        private void FormValutazione_Leave(object sender, EventArgs e)
        {
          
        }

        private void FormValutazione_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(this.father == null) && !this.modifica)
            {
                this.father.closeFormSostanza();
            }
        }
    }
}
