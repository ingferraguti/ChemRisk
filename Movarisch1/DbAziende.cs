using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Movarisch1
{
    class DbAziende
    {

        public static void delete(int id)
        {
            List<Azienda> val = DbAziende.retrieve();
            List<Azienda> daSalvare = new List<Azienda>();
            foreach (Azienda v in val)
            {
                if (v.id != id)
                {
                    daSalvare.Add(v);
                }
            }
            DbAziende.save(daSalvare);
            
        }

        public static void save(List<Azienda> daSalvare) 
        {
            //faccio un backup
            DateTime now = DateTime.Now;
            String d = now.ToString("_yyyy-MM-dd_HH-mm-ss-fff");
            if (File.Exists(Path.Combine(Utils.pathDatabase(), "Aziende.mvrc"))) System.IO.File.Move(Utils.pathDatabase() + "Aziende.mvrc", Utils.pathBackup() + "Aziende" + d + ".mvrc.bk");
            System.Threading.Thread.Sleep(200);
            //sostituisco i dati
            foreach (Azienda a in daSalvare)
            {
                DbAziende.append(a);
               
            }
        }

        public static bool append(Azienda newAzienda)
        {
            // salva nel file
            try
            {
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
                    //System.Threading.Thread.Sleep(100);
                    w.Close();
                    //streamAziende.Close();
                }
                return true;
            }
            catch {
                FileStream a = null;
                try
                {
                    a = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Append);
                }
                catch {
                    System.Threading.Thread.Sleep(700);
                }
                finally{
                    a.Close();
                }
                return false;

            }
        }

        public static List<Azienda> retrieve()
        {
            //carica Aziende
            List<Azienda> aziende = new List<Azienda>();
            String line;
            String[] azienda = new String[10];
            String separatore = "###############";
            int numline = 0;
            int aziendeIndex = 0;
            if (!File.Exists(Utils.pathDatabase() + "Aziende.mvrc")) { FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Append); }
            using (FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aziende.mvrc", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader r = new StreamReader(streamAziende))
            {
                while ((line = r.ReadLine()) != null)
                {
                    //separa le aziende per sedici#
                    if (line == separatore)
                    {
                        numline = 0;
                        Azienda nuova = new Azienda(int.Parse(azienda[0]), azienda[1], azienda[2], azienda[3], azienda[4], azienda[5], azienda[6], azienda[7], azienda[8], azienda[9]);
                        aziende.Add(nuova);
                        aziendeIndex++;
                        //listBox1.Items.Add(nuova.denominazione);
                    }
                    else
                    {
                        azienda[numline] = line;
                        numline++;
                    }
                }
                r.Close();
                streamAziende.Close();
            }
            return aziende;
        }


        public static Azienda find(int id)
        {
            List<Azienda> val = DbAziende.retrieve();
           
            foreach (Azienda v in val)
            {
                if (v.id == id)
                {
                    return v;
                }
            }

            return null;

        }





    }
}
