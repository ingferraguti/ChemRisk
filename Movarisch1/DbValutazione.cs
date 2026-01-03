using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Movarisch1
{
    static class DbValutazione
    {
        //find by id
        public static Valutazione getById(int id) 
        {
            List<Valutazione> val = DbValutazione.retrieve();
            foreach(Valutazione v in val)
            {
                if(v.id==id)
                {
                    return v;
                }
            }
            return null;
        }

        public static void delete(int idToDelete)
        {
            List<Valutazione> val = DbValutazione.retrieve();
            List<Valutazione> daSalvare = new List<Valutazione>();
            foreach(Valutazione v in val)
            {
                if (v.id != idToDelete)
                {
                     daSalvare.Add(v);
                }
            }
            DbValutazione.save(daSalvare);
        }

        public static void append(Valutazione a)
        {
            DbValutazione.delete(a.id);
            System.Threading.Thread.Sleep(500);
            List<Valutazione> daSalvare = DbValutazione.retrieve();

            if (daSalvare == null)
            {
                daSalvare = new List<Valutazione>();
            }

            daSalvare.Add(a);
            DbValutazione.save(daSalvare);
        }

        

        private static void save(List<Valutazione> valutazioni)
        {
            //faccio un backup
            DateTime now = DateTime.Now;
            String d = now.ToString("_yyyy-MM-dd_HH-mm-ss-fff");
            if (File.Exists(Path.Combine(Utils.pathDatabase(), "Valutazioni.mvrc"))) System.IO.File.Move(Utils.pathDatabase() + "Valutazioni.mvrc", Utils.pathBackup() + "Valutazioni" + d + ".mvrc.bk");

            //sostituisco i dati
            String serializationFile = Path.Combine(Utils.pathDatabase(), "Valutazioni.mvrc");
            using (Stream s = File.Open(serializationFile, FileMode.Create))
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(s, valutazioni);
            }
        }

        public static List<Valutazione> retrieve()
        {
            String serializationFile = Path.Combine(Utils.pathDatabase(), "Valutazioni.mvrc");
            if (File.Exists(serializationFile))
            {
                List<Valutazione> agenti = new List<Valutazione>();
                using (Stream s = File.Open(serializationFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    try
                    {
                        agenti = (List<Valutazione>)bf.Deserialize(s);
                    }
                    catch
                    {
                        agenti = null;
                    }

                    s.Close();
                    return agenti;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
