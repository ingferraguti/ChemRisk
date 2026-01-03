using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Movarisch1
{
    class ValutazioneDoc
    {
        static public void salvaFile(Valutazione v, String path)
        {
            //String contenuto = "";
            //DateTime now = DateTime.Now;
            //String date = now.ToString("_yyyy-MM-dd_HH-mm-ss");
            //System.IO.File.Create(path);

            if (v == null) { MessageBox.Show("Si è verificato un errore nella generazione del report."); }

            
            String testo="";
            try
            {
                testo = ValutazioneDoc.htmlWrapping(ValutazioneDoc.creaTesto(v));
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
               
            }
          

            try
            {

                //using (FileStream streamValutazione = File.Open(path)) //, FileMode.Append
                //using (StreamWriter w = new StreamWriter(streamValutazione))
               
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(path))
                {
                    try
                    {
                        //System.IO.File.WriteAllText(path, ValutazioneDoc.htmlWrapping(ValutazioneDoc.creaTesto(v)));
                        //w.AutoFlush = true;
                        w.WriteLine(testo);
                        w.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        //w.WriteLine(ValutazioneDoc.htmlWrapping(ValutazioneDoc.creaTesto(v)));
                        //w.Close();
                        try
                        {
                            System.IO.File.WriteAllText(path, testo);
                        }
                        catch (Exception xxx)
                        {
                            MessageBox.Show("--" + xxx.ToString());
                        }
                    }

                }
            }
            catch {
                MessageBox.Show("Errore durante il salvataggio del file. Controlla che il percorso inserito non sia pieno o protetto.");
            }
        }
        static private String head() {
            //PROLEMA CON IL CHARSET
            return "<!DOCTYPE html><html><head><title>Valutazione del rischio chimico</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head>";
        }
        static private String htmlWrapping(String content) {
            return ""+ValutazioneDoc.head()+"<body>"+content+"</body></html>";
        }
        static public String creaTesto(Valutazione v)
        {
            //valutazione ogetto invece che:
            //String denominazioneazienda,String nomearea,String nomecognome

            DateTime now = DateTime.Now;
            String d;
            if (now != null)
            {
                d = now.ToString("dd/MM/yyyy");
            }
            else {
                d = "----";
            }

            Lavoratore l = DbLavoratori.findLavoratore(v.idLavoratore);
            Azienda a = null;
            Area area = null;
            if(l != null)  a = DbAziende.find(l.idazienda);
            if(l != null)  area = DbAree.find(l.area);
            
            v.getRisch();
            /*
             
             * 
             
             */
            String testo =  @"<h1>VALUTAZIONE DEL RISCHIO DI UN AGENTE CHIMICO PERICOLOSO</h1>
<br />
Data di compilazione: "+ d +@"<br />
<br />
Azienda: " + a.denominazione + @"<br />
Area: " + area.nome + @"<br />
Lavoratore: " + l.cognome + " " + l.nome + @"<br />
<br />
<br />"
+@"


";
            if (v.ac.tipo == "sostanza")
            { testo += "<p>Valutazione del rischio di una sostanza pericolosa</p>"; }
            if (v.ac.tipo == "miscelaP")
            { testo += "<p>Valutazione del rischio di una miscela pericolosa</p>"; }
            if (v.ac.tipo == "processo")
            { testo += "<p>Valutazione del rischio di una sostanza generata da un processo</p>"; }
            if (v.ac.tipo == "miscelaNP")
            { testo += "<p>Valutazione del rischio di una miscela non pericolosa contenente sostanze pericolose</p>"; }


            testo += "Agente chimico: " + v.ac.nome + @"<br />";

            bool tmp = true;
            if (v.ac.tipo == "sostanza" | v.ac.tipo == "miscelaP") 
            {
                if (v.ac.identificativo.CompareTo("") == 0) tmp = false;
                if (tmp) testo += "Identificativo (CAS,IUPAC,...): " + v.ac.identificativo + "<br />";
                testo +="Frasi di rischio (frasi H) associate: <br /><ul>";
                foreach(String[] h in v.ac.frasiH)
                {
                    testo += "<li>"+h[0]+" - "+h[1]+"</li>";
                }
                testo += @"</ul>";
                if (v.ac.vlep) testo += @"<br /><br />All'agente chimico &egrave; associato un VLEP. <br />";

            }
            else if (v.ac.tipo == "processo" | v.ac.tipo == "miscelaNP")
            {
                testo += "Sostanze che compongono la miscela:<br />";
                List<AgenteChimico> componenti = v.ac.getComponentiMiscela();
                testo += "<ul>";
                foreach(AgenteChimico c in componenti)
                {
                    testo += "<li>" + c.nome + "  ";
                    if (c.identificativo.ToString() != "") testo += " ( " + c.identificativo + " ) "; // Identificativo(CAS,IUPAC,...):
                    if (c.vlep) testo += "  (vlep: Si)";
                        testo += "<ul>";
                        foreach (String[] h in c.frasiH)
                            {
                                //
                                testo += "<li>"+h[0]+" - "+h[1]+"</li>";
                            }
                        testo += "</ul>";
                    testo += "</li>";
                }
                testo += "</ul>";
               
            }


            testo += @"           
<br />
</h2>Parametri inseriti</h2>";
            if (v.ac.tipo != "processo") testo += "Propriet&agrave; chimico fisica: " + v.statoFisicoInalHR + @"<br />";
testo+= " Quantit&agrave; in uso: " + v.quantita.ToString() + @" Kg<br />
Tipologia d'uso: " + v.tipoUsoInalHR + @"<br />
Tipologia di controllo: " + v.tipoControlloHR + @"<br />
Tempo di esposizione: " + v.tempoInal.ToString() + @" minuti<br />
Distanza degli esposti: " + v.distanza.ToString() + @" metri<br />";
 if (v.ac.tipo != "processo") testo+= " Livello di contatto cutaneo: " + v.livelliContattoCutaneoHR + @"<br />";
testo+= @"<br />
<br />
<b>Valutazione del rischio secondo MoVaRisCh 2018</b>:<br />
<br />
Indice di pericolo = " + v.ac.getScore() +@"<br /> 
<br />
[ Einal ] valore dell'indice di esposizione per via inalatoria = " + v.einal +@"<br />
<br />
[ Ecute ] valore dell'indice di esposizione per via cutanea = "+ v.ecute +@"<br />
<br />
<br />
Rischio inalatorio <br />
Rinal = "+ v.rInal +@"<br />";
 if (v.ac.tipo != "processo") testo+= "Rischio cutaneo <br />Rcute = "+ v.rCute +@"<br />";
testo+= @"Rischio cumulativo <br />
Rcum = "+ v.getRisch() +@"<br />
<br />Classificazione del rischio: <br />"+v.getFraseValutazione()+@"<br />";
        return testo;
        }
    }
}
