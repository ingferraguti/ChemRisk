using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Movarisch1
{
    public static class IncrementalIndex
    {
        public static int getNewIndex(String info){
            int i = IncrementalIndex.getLastIndex() + 1;
            DateTime now = DateTime.Now;
            String d = now.ToString(" dd/MM/yyyy HH:mm:ss:fff");
            IncrementalIndex.writeLine(i.ToString() + "#" + info + "#" + d);
            return i;
        }

        private static int getLastIndex() {
            String line;

            if (File.Exists(Utils.pathDatabase() + "Incremental.mvrc"))
            {
                using (FileStream streamIndexes = File.Open(Utils.pathDatabase() + "Incremental.mvrc", FileMode.Open))
                using (StreamReader r = new StreamReader(streamIndexes))
                {
                    String[] parts= new String[3];
                    while ((line = r.ReadLine()) != null) {
                        parts = line.Split('#');
                    }
                    Regex reg = new Regex("[^0-9]");
                    Match match = reg.Match(parts[0]);
                    if(parts[0].Length>9){
                        MessageBox.Show("Attenzione index maggiore di 9 caratteri , contattare l'assistenza,il file incremental.mvrc potrebbe essere stato modificato manualmente in maniera non corretta oppure il file è danneggiato");
                        return 1;
                    }
                    if (match.Success)
                    {
                        MessageBox.Show("Attenzione index contenente lettere, contattare l'assistenza,il file incremental.mvrc potrebbe essere stato modificato manualmente in maniera non corretta oppure il file è danneggiato");
                        return 1000000;
                    }
                    int i = int.Parse(parts[0]);
                    if (i > 932352357) { MessageBox.Show("Attenzione: integer in overflow contattare l'assistenza, a breve il programma smetterà di funzionare"); }
                    if (i > 0) { return i; }
                    else {
                        MessageBox.Show("Errore grave.Contattare l'assistenza. Impossibile leggere l'indice incrementale");
                        return -1; 
                    }
                }
            }
            else 
            {
                return 0;
            }
        }
        private static void writeLine(String line) {

            using (FileStream streamIndexes = File.Open(Utils.pathDatabase() + "Incremental.mvrc", FileMode.Append))
            using (StreamWriter w = new StreamWriter(streamIndexes))
            {
                w.WriteLine(line);
            }
            /* if (File.Exists(@"incremental.mvrc")) {}  else  {  return -1;  }*/
        }
    }
}
