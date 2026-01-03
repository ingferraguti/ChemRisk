using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Movarisch1
{
    class DbAgentiChimici
    {
        public static void append(AgenteChimico a) 
        {
            List<AgenteChimico> daSalvare = DbAgentiChimici.retrieve();
            
            if(daSalvare==null){
                daSalvare = new List<AgenteChimico>();
            }
           
            daSalvare.Add(a);
            DbAgentiChimici.save(daSalvare);
        }
        public static void save(List<AgenteChimico> agenti) 
        {
            //faccio un backup
            DateTime now = DateTime.Now;
            String d = now.ToString("_yyyy-MM-dd_HH-mm-ss-fff");
            if (File.Exists(Path.Combine(Utils.pathDatabase(), "AgentiChimici.mvrc"))) System.IO.File.Move(Utils.pathDatabase() + "AgentiChimici.mvrc", Utils.pathBackup() + "AgentiChimici" + d + ".mvrc.bk");
            
            //sostituisco i dati
            String serializationFile = Path.Combine(Utils.pathDatabase(),"AgentiChimici.mvrc");
            using (Stream s = File.Open(serializationFile, FileMode.Create))
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(s,agenti);
            }
        }

        public static List<AgenteChimico> retrieve() 
        {
            String serializationFile = Path.Combine(Utils.pathDatabase(), "AgentiChimici.mvrc");
            if (File.Exists(serializationFile))
            {
                List<AgenteChimico> agenti = new List<AgenteChimico>();
                using (Stream s = File.Open(serializationFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    try
                    {
                        agenti = (List<AgenteChimico>)bf.Deserialize(s);
                    }
                    catch
                    {
                        agenti = null;
                    }

                    agenti.Sort( (a,b) => a.nome.CompareTo(b.nome) );

                    s.Close();

                    return agenti;
                }
            }
            else
            {
                MessageBox.Show("Non è stato possibile aprire il file Agenti chimici, risulta già aperto");
                return null;
            }
        }

        public static List<AgenteChimico> getOfType(String type) 
        {
            List<AgenteChimico> all = DbAgentiChimici.retrieve();
            List<AgenteChimico> selected = new List<AgenteChimico>();
            if (all == null) return null;
            foreach(AgenteChimico ac in all)
            {
                if(ac.tipo==type)
                {
                    selected.Add(ac);
                }
            }
            return selected;
        }

        public static AgenteChimico getById(int id) 
        {
            List<AgenteChimico> all = DbAgentiChimici.retrieve();
            foreach (AgenteChimico ac in all)
            {
                if (ac.id==id)
                {
                    return ac;
                }
            }
            MessageBox.Show("Agente chimico non trovato "+id);
            return null;
        }

        public static int elimina(int id, bool modifica = false) {

            List<AgenteChimico> all = DbAgentiChimici.retrieve();
            AgenteChimico daEliminare = DbAgentiChimici.getById(id);
            AgenteChimico colpevole = null;
            //controllo se si può eliminare
            bool siPuoEliminare = true;
            foreach (AgenteChimico ajkl in all)
            {
                if (ajkl.tipo == "miscelaNP" || ajkl.tipo == "processo")
                {
                    if (ajkl.sostanze!=null)
                    {
                        foreach (int i in ajkl.sostanze)
                        {
                            if (i == id)
                            {
                                siPuoEliminare = false;
                                colpevole = DbAgentiChimici.getById(ajkl.id);
                            }
                        }
                    }
                }
            }

            if (siPuoEliminare) 
            {
                List<AgenteChimico> daSalvare = new List<AgenteChimico>();
                foreach (AgenteChimico ac in all)
                {
                    if (ac.id == id)
                    {
                    }
                    else
                    {
                        daSalvare.Add(ac);
                    }
                }
                DbAgentiChimici.save(daSalvare);
            }
            else{
                if(modifica){
                    MessageBox.Show("Non è possibile modificare " + daEliminare.nome + " perchè è contenuto in " + colpevole.nome + " " + colpevole.identificativo + " in " + colpevole.tipo + " è stata creata una copia");
                }else{
                    MessageBox.Show("Non è possibile eliminare " + daEliminare.nome + " perchè è contenuto in " + colpevole.nome + " " + colpevole.identificativo + " in " + colpevole.tipo);
                }
                return (2);
            }
            
           
            return (0);

        }





    }
}
