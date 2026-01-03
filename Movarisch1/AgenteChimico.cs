using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
 

namespace Movarisch1
{
    [Serializable]
    public class AgenteChimico
    {
        public int id;
        public string nome;
        public String identificativo;
        public Boolean vlep;
        public List<String[]> frasiH;
        public int[] sostanze;
        //public List<KeyValuePair<int,Boolean>> emissione;
        public Boolean altaemissione;
        public String tipo;

        private float score=0;


        public AgenteChimico() { 
            //-----------

        }

       
       public String[] getArrayFrasiH()
       {
           if (this.tipo == "processo" | this.tipo == "miscelaNP") return this.getAllFrasiHComponenti();
           else
           {
               List<string> frasi = new List<string>();
               foreach (String[] f in this.frasiH)
               {
                   frasi.Add(f[0] + ";" + f[1] + ";" + f[2]);
               }
               return frasi.ToArray();
           }
       }

       public List<AgenteChimico> getComponentiMiscela() 
       {
           List<AgenteChimico> componenti = new List<AgenteChimico>();
           List<AgenteChimico> tutti = DbAgentiChimici.retrieve();

           foreach (int s in this.sostanze) 
           {
               foreach (AgenteChimico agent in tutti)
               {
                    if (agent.id==s)
                    {
                        componenti.Add(agent);
                    }
               }
               //AgenteChimico a = DbAgentiChimici.getById(s);
               
           }
           return componenti;
       }

       public string[] getAllFrasiHComponenti() 
       {
           List<AgenteChimico> componenti = this.getComponentiMiscela();
           List<String> frasi = new List<String>();
           foreach (AgenteChimico ac in componenti) 
           {
               frasi.AddRange(ac.getArrayFrasiH());
           }
           return frasi.ToArray();
       }



        public float getScore() {

            this.score=1;//?????????

            if (this.tipo == "sostanza" | this.tipo == "miscelaP")
            {
                //____frasi h + controllo se è vlep______
                float f;
                foreach(String[] fraseh in this.frasiH)
                {
                    try
                    {
                        var culture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
                        culture.NumberFormat.NumberDecimalSeparator = ",";


                        f = float.Parse(fraseh[2], culture);//prendo lo score della frase

                    }
                    catch
                    {
                        f = (float)1;
                        MessageBox.Show("non riesco a interpretare: " + fraseh[2]);
                    }

                    if (f > this.score)//controllo se è più alto 
                    {
                        this.score = f;//se è più alto lo assegno
                    }
                }
                //ora controllo vlep
                if(this.vlep){
                    if (this.score < 3) this.score = 3;
                }
                return this.score;
            }
             
            else if (this.tipo == "miscelaNP") { 
                //elenco frasi h delle sostanze che lo compomgono per controllo casi particolari
                
                float score = CasiParticolari.misceleNonPericolose(this.getAllFrasiHComponenti());
                //se contiene un vlep lo score è 2.25 anche se nessuna sostanza che contiene ha frasi h DA FARE
                bool contieneVlep = false;
                List<AgenteChimico> componenti = this.getComponentiMiscela();
                foreach(AgenteChimico ag in componenti)
                {
                    if (ag.vlep == true) contieneVlep = true;
                }
                if (contieneVlep & (score < 2.25F)) return 2.25F;
                return score;
            }

            else if (this.tipo == "processo") { 
                // controllo se è bassa o alta e seguo i casi particolari
                
                //controlla la storia dei vlep e se imposta bene alta e bassa emissione
                float temp = 0;
                float value = 0;
                foreach (int i in this.sostanze)
                {
                    AgenteChimico ag = DbAgentiChimici.getById(i);
                    temp = 0;
                    if (ag == null) MessageBox.Show("agente chimico "+i+" non trovato");
                    if (this.altaemissione)
                    {
                        temp = CasiParticolari.processiElevataEmissione(ag.getArrayFrasiH());
                    }
                    else
                    {
                        temp = CasiParticolari.processiBassaEmissione(ag.getArrayFrasiH());
                    }
                    if (temp > value) { value = temp; }
                }


                return value;
                
                }

            
            else
            {
                MessageBox.Show("Errore nel tipo di agente chimico");
            }
            return 1;
        }


        public string Nome
        {
            get
            {
                return this.nome;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
        }

    }
}
