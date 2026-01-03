using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Movarisch1
{
    static class DbLavoratori
    {
        //_________DELETE__________________________________________
        public static void elimina(int id) { 
            /**
             * li leggo tutti 
             * rinomino file attuale come backup
             * tolgo il lavoratore da cancellare e salvo tutti i lavoratori rimasti nel file nuovo 
             */
            Lavoratore[] lavoratori = DbLavoratori.caricaLavoratori();
            Lavoratore[] lavoratoriAggiornati = new Lavoratore[2999];
            int contatore = 0;
            foreach(Lavoratore l in lavoratori)
            {
                if (l == null) { }
                else if (l.id==id){
                    //non lo aggiungo perchè è da eliminare
                }
                else{
                    lavoratoriAggiornati[contatore] = l;
                    contatore++;
                }
            }

            //tolgo i vecchi dati -- rinomino file attuale e lo tengo per backup
            DateTime now = DateTime.Now;
            String d = now.ToString("_yyyy-MM-dd_HH-mm-ss-fff");
            System.IO.File.Move(Utils.pathDatabase() + "Lavoratori.mvrc", Utils.pathBackup() + "Lavoratori" + d + ".mvrc.bk");

            //salva tutti i lavoratori nel file nuovo
            for (int i = 0; i < contatore; i++)
            {
                DbLavoratori.save(lavoratoriAggiornati[i]);
            }
        }

        //___SAVE__________________________________________________
        public static void save(Lavoratore l) {
            using (FileStream streamLav = File.Open(Utils.pathDatabase() + "Lavoratori.mvrc", FileMode.Append))
            using (StreamWriter w = new StreamWriter(streamLav))
            {
                w.WriteLine(l.id);
                w.WriteLine(l.idazienda);
                w.WriteLine(l.area);
                w.WriteLine(l.nome);
                w.WriteLine(l.cognome);
                w.WriteLine(l.note);
                w.WriteLine("###############");
            }
        }


        //___FIND__________________________________________________
        public static Lavoratore findLavoratore(int id)
        {
            //MessageBox.Show("sto cercando" + id.ToString());
            Lavoratore[] lavoratori = DbLavoratori.caricaLavoratori();
            if(lavoratori != null){
                //MessageBox.Show("num lavoratori: "+lavoratori.Length.ToString());
            }
            //int indexLavoratori = Utils.contaLavoratori();
            //MessageBox.Show("index lavoratori: " + indexLavoratori);
            
            for (int i = 0; i < 2999; i++)
            {
                if (lavoratori[i] != null)
                {
                    if (lavoratori[i].id == id)
                    {
                        return lavoratori[i];
                    }
                }
                //else { MessageBox.Show("lavoratori[i] è null"); }
                //msg += lavoratori[i].ToString();
                
            }
            MessageBox.Show("C'è un errore. Controlla il lavoratore selezionato. ");
            return null;
        }

        //___READ_________________________________________
        public static Lavoratore[] caricaLavoratori()
        {
            String line;
            String[] lavoratore = new String[6];
            Lavoratore[] lavoratori = new Lavoratore[2999];
            String separatore = "###############";
            int numline = 0;
            int index = 0;
            if (File.Exists(Utils.pathDatabase() + "Lavoratori.mvrc"))
            {
                using (FileStream streamLav = File.Open(Utils.pathDatabase() + "Lavoratori.mvrc", FileMode.Open, FileAccess.Read, FileShare.Read))// FileMode.Open, FileShare.Read
                using (StreamReader r = new StreamReader(streamLav))
                {
                    while ((line = r.ReadLine()) != null)
                    {
                        //separa le aree per sedici#
                        if (line == separatore)
                        {
                            numline = 0;
                            Lavoratore nuovo = new Lavoratore(int.Parse(lavoratore[0]), int.Parse(lavoratore[1]), int.Parse(lavoratore[2]), lavoratore[3], lavoratore[4], lavoratore[5]);//per ora muore qui
                            lavoratori[index] = nuovo;
                            index++;
                        }
                        else
                        {
                            lavoratore[numline] = line;
                            numline++;
                        }
                    }
                   
                    streamLav.Close();
                    r.Close();
                    //MessageBox.Show("trovati "+index.ToString()+" lavoratore");
                }
               
            }
            
            return lavoratori;
        }

        public static int contaLavoratori()
        {
            String line;
            String separatore = "###############";
            int numline = 0;
            int index = 0;
            if (File.Exists(Utils.pathDatabase() + "Lavoratori.mvrc"))
            {
                using (FileStream streamLav = File.Open(Utils.pathDatabase() + "Lavoratori.mvrc", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader r = new StreamReader(streamLav))
                {
                    while ((line = r.ReadLine()) != null)
                    {
                        //separa le aree per sedici#
                        //MessageBox.Show(line);
                        if (line == separatore)
                        {
                            numline = 0;
                            index++;
                            //MessageBox.Show("separatore...");
                        }
                        else
                        {
                            numline++;
                        }
                    }
                    //MessageBox.Show("trovate "+index.ToString()+" lavoratore");
                    r.Close();
                    streamLav.Close();
                }

            }
            else { MessageBox.Show("Errore 2001"); }
            return index;
        }





    }
}
