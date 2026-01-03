using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movarisch1
{
    static class DbAree
    {
        public static void elimina(int id)
        {
            List<Area> all = DbAree.retrieve();
            List<Area> daSalvare = new List<Area>();
            foreach (Area ac in all)
            {
                if (ac.id == id)
                {
                }
                else
                {
                    daSalvare.Add(ac);
                }
            }
            DbAree.save(daSalvare);
            return;
        }

        static public void save(List<Area> daSalvare) 
        {
            //faccio un backup
            DateTime now = DateTime.Now;
            String d = now.ToString("_yyyy-MM-dd_HH-mm-ss-fff");
            if (File.Exists(Path.Combine(Utils.pathDatabase(), "Aree.mvrc"))) System.IO.File.Move(Utils.pathDatabase() + "Aree.mvrc", Utils.pathBackup() + "Aree" + d + ".mvrc.bk");

            //sostituisco i dati
            foreach (Area a in daSalvare)
            {
                DbAree.append(a);
            }
        }

        static public void append(Area newArea)
        {
            using (FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aree.mvrc", FileMode.Append))
            using (StreamWriter w = new StreamWriter(streamAziende))
            {
                w.WriteLine(newArea.id);
                w.WriteLine(newArea.azienda);
                w.WriteLine(newArea.nome);
                w.WriteLine(newArea.note);
                w.WriteLine("###############");
            }
        }

        static public List<Area> retrieve()
        {
            String line;
            String[] area = new String[4];
            List<Area> aree = new List<Area>();
            String separatore = "###############";
            int numline = 0;
            int areeIndex = 0;
            if (File.Exists(Utils.pathDatabase() + "Aree.mvrc"))
            {
                using (FileStream streamAziende = File.Open(Utils.pathDatabase() + "Aree.mvrc", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader r = new StreamReader(streamAziende))
                {
                    while ((line = r.ReadLine()) != null)
                    {
                        //separa le aree per sedici#
                        if (line == separatore)
                        {
                            numline = 0;
                            Area nuova = new Area(int.Parse(area[0]), int.Parse(area[1]), area[2], area[3]);
                            aree.Add(nuova);
                            areeIndex++;
                        }
                        else
                        {
                            area[numline] = line;
                            numline++;
                        }
                    }
                    streamAziende.Close();
                    r.Close();
                    
                }
            }
            return aree;
        }


        public static Area find(int id)
        {
            List<Area> all = DbAree.retrieve();
           
            foreach (Area ac in all)
            {
                if (ac.id == id)
                {
                    return ac;
                }
               
            }
            
            return null;
        }


    }
}
