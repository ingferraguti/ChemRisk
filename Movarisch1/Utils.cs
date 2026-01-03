using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Movarisch1
{
    static class Utils
    {

        public static float scoreOfHPhrase(String h, string[,] frasih, int frasiHindex)
        { 
            //---
            float score = 0;
            int contatore = 0;
            float f = 0;
            string fras = h.Split('(')[0];
            for (int i = 0; i < frasiHindex; i++)
            {
                if (fras == frasih[i, 0])
                {
                    f = float.Parse(frasih[i, 2]);
                    score = f;
                }
            }
            contatore++;
            return score;
        }

        //_____________
        public static String pathDatabase() {
            return Path.Combine(Application.StartupPath, @"database/");
        }
        public static String pathBackup() {
            return Path.Combine(Application.StartupPath, @"database/backup/");
        }
        //______________
        //___find_____
        public static Azienda findAziendaByLavoratore(int idLavoratore) {
            //MessageBox.Show("id lavoratore " + idLavoratore.ToString());
            Lavoratore l = DbLavoratori.findLavoratore(idLavoratore);
            //MessageBox.Show("lavoratore " + l.nome);

            Azienda a = Utils.findAzienda(l.idazienda);
           // MessageBox.Show("id azienda " + l.idazienda + " " + a.id);
            return a;
        }
        public static Area findAreaByLavoratore(int idLavoratore) {
            Lavoratore l = DbLavoratori.findLavoratore(idLavoratore);
            Area a = Utils.findAreaById(l.area);
            return a;
        }

        //___LAVORATORI________________________________________
        public static Lavoratore findLavoratore(int id) { 
            return DbLavoratori.findLavoratore(id);
        }

        public static Lavoratore[] caricaLavoratori() {
            return DbLavoratori.caricaLavoratori();
        }

        public static int contaLavoratori() {
            return DbLavoratori.contaLavoratori();
        }


        //___AREE_______________________________________________
        public static Area findAreaById(int idArea) {
            List<Area> aree = DbAree.retrieve(); 
            foreach (Area area in aree){
                if(area.id==idArea){
                    return area;
                }
            }
            MessageBox.Show("area non trovata nel database");
            return new Area(0,0,"","");
        }

        public static int findIdArea(String area,int azienda) {
            int index = Utils.contaAree();
            Area[] aree = Utils.caricaAree();
            for (int i = 0; i < index; i++)
            {
                if ((aree[i].nome == area)&(aree[i].azienda == azienda))
                {
                    return aree[i].id;
                }
            }
            return 0;
        }

        public static String findNomeArea(int area) {
            int index = Utils.contaAree();
            Area[] aree = Utils.caricaAree();
            for (int i = 0; i < index;i++ ) {
                if (aree[i].id==area) {
                    return aree[i].nome;
                }
            }
            return "Nome area non trovato";
        }

        public static void salvaAreaModificata(Area a)
        {
            if (!a.Equals(null))
            {
                Area[] newAree = new Area[299];
                int indexNewAree = 0;
                Area[] aree = Utils.caricaAree();
                int numAree = Utils.contaAree();
                for (int j = 0; j < numAree; j++)
                {//Area temp in aree
                    if (aree[j].id == a.id)
                    {
                        //sostituisci
                        newAree[indexNewAree] = a;
                    }
                    else
                    {
                        newAree[indexNewAree] = aree[j];
                    }
                    indexNewAree++;
                };
                // rinomino file attuale e lo tengo per backup
                DateTime now = DateTime.Now;
                String d = now.ToString("_yyyy-MM-dd_HH-mm-ss-fff");
                System.IO.File.Move(Utils.pathDatabase() + "Aree.mvrc", Utils.pathBackup() + "Aree" + d + ".mvrc.bk");
                // salva tutte le aziende nel file nuovo
                for (int i = 0; i < indexNewAree; i++)
                {
                    Utils.salvaNelFileAree(newAree[i]);
                }
            }
            else { MessageBox.Show("oggetto azienda null"); }
        }

        public static void salvaNelFileAree(Area newArea)
        {
            DbAree.append(newArea);
        }

        public static int contaAree()
        {
            return DbAree.retrieve().ToArray().Count();
        }

        public static Area[] caricaAree() {
            return DbAree.retrieve().ToArray();
        }
        


        // CERCARE AZIENDE
        public static Azienda findAzienda(int id)
        {
            List <Azienda> aziende = DbAziende.retrieve();
            
            foreach (Azienda a in aziende)
            {
                //MessageBox.Show(a.id.ToString()+" = "+id.ToString());
                if (a.id == id)
                {
                    return a;
                }
            }
            MessageBox.Show("azienda non trovata");
            return null;
        }
        public static int findIdAzienda(String denominazione){
            Azienda[] aziende = Utils.caricaAziende();
            int numAziende = Utils.numAziende();
            for (int i = 0; i < numAziende; i++)
            {

                if (aziende[i].denominazione == denominazione) {
                    return aziende[i].id;
                }
            }

            return 0;
        }
        
        public static String findDenominazioneAzienda(int idAzienda) {
            Azienda[] aziende = Utils.caricaAziende();
            int numAziende = Utils.numAziende();
            for (int i = 0; i < numAziende; i++)
            {
                if (aziende[i].id == idAzienda)
                {
                    return aziende[i].denominazione;
                }
            }
            return "Errore:Azienda non trovata";
        }

        // CARICARE AZIENDE DA FILE
        public static Azienda[] caricaAziende()
        {
            //carica Aziende
            Azienda[] aziende = new Azienda[199];
            String line;
            String[] azienda = new String[10];
            String separatore = "###############";
            int numline = 0;
            int aziendeIndex = 0;
            if (!File.Exists(Utils.pathDatabase() + "Aziende.mvrc")) { FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Append); }
            using (FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Open))
            using (StreamReader r = new StreamReader(streamAziende))
            {
                while ((line = r.ReadLine()) != null)
                {
                    //separa le aziende per sedici#
                    if (line == separatore)
                    {
                        numline = 0;
                        Azienda nuova = new Azienda(int.Parse(azienda[0]), azienda[1], azienda[2], azienda[3], azienda[4], azienda[5], azienda[6], azienda[7], azienda[8], azienda[9]);
                        aziende[aziendeIndex] = nuova;
                        aziendeIndex++;
                        //listBox1.Items.Add(nuova.denominazione);
                    }
                    else
                    {
                        azienda[numline] = line;
                        numline++;
                    }
                }
            }
            return aziende;
        }

        public static int numAziende()
        {
            //conta Aziende
            Azienda[] aziende = new Azienda[199];
            String line;
            String[] azienda = new String[10];
            String separatore = "###############";
            int numline = 0;
            int aziendeIndex = 0;
            if (!File.Exists(Utils.pathDatabase() + "Aziende.mvrc")) { FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Append); }
            using (FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Open))
            using (StreamReader r = new StreamReader(streamAziende))
            {
                while ((line = r.ReadLine()) != null)
                {
                    //separa le aziende per sedici#
                    if (line == separatore)
                    {
                        numline = 0;
                        Azienda nuova = new Azienda(int.Parse(azienda[0]), azienda[1], azienda[2], azienda[3], azienda[4], azienda[5], azienda[6], azienda[7], azienda[8], azienda[9]);
                        aziende[aziendeIndex] = nuova;
                        aziendeIndex++;
                        //listBox1.Items.Add(nuova.denominazione);
                    }
                    else
                    {
                        azienda[numline] = line;
                        numline++;
                    }
                }
            }
            return aziendeIndex;
        }
    }
}
