using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movarisch1
{
    public static class CasiParticolari
    {
        public static float processiElevataEmissione(String[] frasih){

            //______________________________
            //_PROCESSI______________________
            //____ELEVATA EMISSIONE ________

            String[] elevataUno = new String[19];
            float elevataUnoScore = 5;

            //CAMBIARE!!!!
            String fraseElevataUno = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via inalatoria con score >= a 6,50";

            elevataUno[0] = "H330 cat.2";
            elevataUno[1] = "H330 cat.1";
            elevataUno[2] = "H334 cat.1A";
            elevataUno[3] = "H334 cat.1B";
            elevataUno[4] = "H370";
            elevataUno[5] = "H371";
            elevataUno[6] = "H372";
            elevataUno[7] = "H373";

            elevataUno[8] = "H341";
            elevataUno[9] = "H351";

            elevataUno[10] = "H361";
            elevataUno[11] = "H361f";
            elevataUno[12] = "H361d";
            elevataUno[13] = "H361fd";

            elevataUno[14] = "EUH207";
            elevataUno[15] = "EUH071";
            elevataUno[16] = "EUH204";

            //NUOVE 2024
            elevataUno[17] = "EUH380";
            elevataUno[18] = "EUH381";

            //elevataUno[8] =  "H360"; TOGLIERE
            //elevataUno[9] =  "H360D"; TOGLIERE
            //elevataUno[10] = "H360Df"; TOGLIERE
            //elevataUno[11] = "H360F"; TOGLIERE
            //elevataUno[12] = "H360FD"; TOGLIERE
            //elevataUno[22] = "H360Fd"; //22   TOGLIERE

           /* elevataUno[13] = "H341";
            elevataUno[14] = "H351";

            elevataUno[15] = "H361";
            elevataUno[16] = "H361f";
            elevataUno[17] = "H361d";
            elevataUno[18] = "H361fd";

            elevataUno[19] = "EUH207";
            elevataUno[20] = "EUH071";
            elevataUno[21] = "EUH204";
            */
           

            foreach (String h in frasih)
            {
                foreach (String a in elevataUno)
                {

                    if (h.Split(';')[0] == a) return elevataUnoScore;

                }
               
            }



            //____________________________

            String[] elevataDue = new String[12];
            float elevataDueScore = 3;

            //CAMBIARE??
            String fraseElevataDue = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via inalatoria con score < a 6,50 e >= a 4,50";

            elevataDue[0] = "H332";
            elevataDue[1] = "H331";
            elevataDue[2] = "H317 cat.1A";
            elevataDue[3] = "H317 cat.1B";
            elevataDue[4] = "H362";
            elevataDue[5] = "EUH201";
            elevataDue[6] = "EUH201A";
            elevataDue[7] = "EUH203";
            elevataDue[8] = "EUH205";
            //NUOVO 2024
            //elevataDue[9] = "EUH208";
            elevataDue[9] = "EUH212";
            elevataDue[10] = "H304";
            elevataDue[11] = "EUH211";
            

            foreach (String h in frasih)
            {
                foreach (String a in elevataDue)
                {

                    if (h.Split(';')[0] == a) return elevataDueScore;

                }
            }

            //____________________________

            String[] elevataQuattro = new String[1];
            float elevataQuattroScore = 3;

            //CAMBIARE??
            String fraseElevatoQuattro = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via cutanea e/o per ingestione con score >= a 6,50";

            elevataQuattro[0] = "H310 cat.1";

            foreach (String h in frasih)
            {
                foreach (String a in elevataQuattro)
                {
                    if (h.Split(';')[0] == a) return elevataQuattroScore;
                }
            }

            //____________________________

            String[] elevataTre = new String[6];
            float elevataTreScore = 2.25F;
            String fraseElevatoTre = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via inalatoria con score < a 4,50 e >= a 3,00";

            elevataTre[0] = "H336";
            elevataTre[1] = "H335";
            //elevataTre[2] = "H304"; TOGLIERE
            elevataTre[3] = "EUH206";
            elevataTre[4] = "EUH029";
            elevataTre[5] = "EUH031";
            elevataTre[2] = "EUH032";
            //elevataTre[6] = "EUH032";

            foreach (String h in frasih)
            {
                foreach (String a in elevataTre)
                {
                    if (h.Split(';')[0] == a) return elevataTreScore;
                }
            }

            

            //____________________________
            String[] elevataCinque = new String[8];
            float elevataCinqueScore = 2.25F;
            String fraseElevatoCinque = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via cutanea e/o per ingestione con score < a 6,50 e ≥ a 4,50";

            elevataCinque[0] = "H311";
            elevataCinque[1] = "H310 cat.2";
            elevataCinque[2] = "H314 cat.1A";
            elevataCinque[3] = "H314 cat.1B";
            elevataCinque[4] = "H314 cat.1C";
            elevataCinque[5] = "H318";
            elevataCinque[6] = "EUH070";
            elevataCinque[7] = "EUH202";

            foreach (String h in frasih)
            {
                foreach (String a in elevataCinque)
                {
                    if (h.Split(';')[0] == a) return elevataCinqueScore;
                }
            }

            //____________________________
            String[] elevataSei = new String[3];
            float elevataSeiScore = 2;
            String fraseElevatoSei = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via cutanea e/o per ingestione con score < a 4,50 e ≥ a 3,00";

            elevataSei[0] = "H312";
            elevataSei[1] = "H300 cat.1";
            elevataSei[2] = "H319";

            foreach (String h in frasih)
            {
                foreach (String a in elevataSei)
                {
                    if (h.Split(';')[0] == a) return elevataSeiScore;
                }
            }

            //____________________________
            String[] elevataSette = new String[5];
            float elevataSetteScore = 1.75F;
            String fraseElevatoSette = "Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta un’elevata emissione di almeno un agente chimico pericoloso per via cutanea e/o per ingestione con score < a 3,00 e ≥ a 2,00";

            elevataSette[0] = "H302";
            elevataSette[1] = "H301";
            elevataSette[2] = "H315";
            elevataSette[3] = "H300 cat.2";
            elevataSette[4] = "EUH066";

            foreach (String h in frasih)
            {
                foreach (String a in elevataSette)
                {
                    if (h.Split(';')[0] == a) return elevataSetteScore;
                }
            }


            //se non è nessuno di questi casi?
            return 1;
        }

        //_______Bassa EMISSIONE _______________________________________________________
        public static float processiBassaEmissione(String[] frasih)
        {

            float scoreUno = 2.50F;
            /*Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta una bassa emissione di almeno un agente chimico pericoloso per via inalatoria con score ≥ a 6,50*/
            //String[] bassaUno = new String[24];
            String[] bassaUno = new String[19];
            bassaUno[0] = "H330 cat.2";
            bassaUno[1] = "H330 cat.1";
            bassaUno[2] = "H334 cat.1A";
            bassaUno[3] = "H334 cat.1B";						
            bassaUno[4] = "H370";
            bassaUno[5] = "H371";
            bassaUno[6] = "H372";
            bassaUno[7] = "H373";

           // bassaUno[8] =  "H360";  TOGLIERE
           // bassaUno[9] =  "H360D";  TOGLIERE
           // bassaUno[10] = "H360Df";  TOGLIERE
           // bassaUno[11] = "H360F";  TOGLIERE
          //  bassaUno[12] = "H360FD";  TOGLIERE
          //  bassaUno[23] = "H360Fd";  TOGLIERE

            bassaUno[8] = "H341";
            bassaUno[9] = "H351";

            bassaUno[10] = "H361";
            bassaUno[11] = "H361f";
            bassaUno[12] = "H361d";
            bassaUno[13] = "H361fd";

            bassaUno[14] = "EUH207"; 
            bassaUno[15] = "EUH071";
            bassaUno[16] = "EUH204";

            //nuovo 2024
            bassaUno[17] = "EUH380";
            bassaUno[18] = "EUH381";

            

            foreach (String h in frasih)
            {
                foreach (String a in bassaUno)
                {
                    if (h.Split(';')[0] == a) return scoreUno;
                }
            }
            //________________________________________________________

            float scoreDue = 2;
            /* Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta una bassa emissione di almeno un agente chimico pericoloso per via inalatoria con score < a 6,50 e ≥ a 4,50 */
            String[] bassaDue = new String[12] { "H332", "H331", "H317 cat.1A", "H317 cat.1B", "H362", "EUH201", "EUH201A", "EUH203", "EUH205", "H304", "EUH211", "EUH212" }; //, "EUH208"
            foreach (String h in frasih)
            {
                foreach (String a in bassaDue)
                {
                    if (h.Split(';')[0] == a) return scoreDue;
                }
            }

            float scoreTre = 1.75F;
            /*Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta una bassa emissione di almeno un agente chimico pericoloso per via inalatoria con score < a 4,50 e ≥ a 3,00*/
           // String[] bassaTre = new String[7] { "H336", "H335", "H304", "EUH206", "EUH029", "EUH031", "EUH032" };//
            String[] bassaTre = new String[6] { "H336", "H335", "EUH206", "EUH029", "EUH031", "EUH032" };//2024
            foreach (String h in frasih)
            {
                foreach (String a in bassaTre)
                {
                    if (h.Split(';')[0] == a) return scoreTre;
                }
            }

            float scoreQuattro = 1.25F;
            /* Sostanze e miscele non classificate pericolose il cui impiego e tecnologia comporta una bassa emissione di almeno un agente chimico pericoloso per via cutanea e/o per ingestione appartenente ad una qualsiasi categoria di pericolo */
            String[] bassaQuattro = new String[17] { "H310 cat.1", "H311", "H310 cat.2", "H314 cat.1A", "H314 cat.1B", "H314 cat.1C", "H318", "EUH070", "EUH202", "H312", "H300 cat.1", "H319", "H302", "H301", "H300 cat.2", "H315", "EUH066"};
            foreach (String h in frasih)
            {
                foreach (String a in bassaQuattro)
                {
                    if (h.Split(';')[0] == a) return scoreQuattro;
                }
            }

             /* Sostanze e miscele non classificate pericolose e non contenenti nessuna sostanza pericolosa 
            1,00																						
            */
            return 1;

        }



        public static float misceleNonPericolose(String[] frasih)
        {
            //_________________________________
            String[] uno = new String[13];
            float unoScore = 5.50F;
            String fraseUno = "Miscele non classificabili come pericolose ma contenenti almeno una sostanza pericolosa appartenente ad una qualsiasi classe di pericolo con score >= 8";

            uno[0] = "EUH207";

            uno[1] = "H330 cat.1";
            uno[2] = "H334 cat.1A";
            uno[3] = "H334 cat.1B";
            uno[4] = "H370";
            uno[5] = "H371";
            uno[6] = "H372";

           /* uno[7] =  "H360";
            uno[8] =  "H360D";
            uno[9] =  "H360Df";
            uno[10] = "H360F";
            uno[11] = "H360FD";
            uno[0] = "H360Fd";*/

            uno[7] = "H341";
            uno[8] = "H351";

            uno[9] = "H361";
            uno[10] = "H361fd";
            uno[11] = "EUH380";
            uno[12] = "EUH381";
            

            foreach (String h in frasih)
            {
                foreach (String a in uno)
                {
                    if (h.Split(';')[0] == a) return unoScore;
                }
            }

            //____________________________
            String[] due = new String[18];
            float dueScore = 4;
            String fraseDue = "Miscele non classificabili come pericolose ma contenenti almeno una sostanza pericolosa per via inalatoria appartenente ad una qualsiasi classe di pericolo diversa dalla tossicità di categoria 4 e dalle categorie relative all’irritazione inalatoria, narcosi e reazione con score < 8; oppure contenenti sensibilizzanti cutanei";

            due[0] = "H331";	
            due[1] = "H330 cat.2";	           	
            due[2] = "H373";	
            due[3] = "H304";	
            due[4] = "H361d";	
            due[5] = "H361f";	
            due[6] = "H362";	
            due[7] = "EUH071";	          
            due[8] = "H317 cat.1A";
            due[9] = "EUH204";
            due[10] = "EUH212";
            //due[10] = "EUH208";	
            due[11] = "EUH203";	
            due[12] = "EUH205";	
            due[13] = "EUH201";	
            due[14] = "EUH201A";	
            due[15] = "EUH202";	
            due[16] = "H317 cat.1B";
            due[17] = "EUH211";
            
          

            foreach (String h in frasih)
            {
                foreach (String a in due) 
                {
                    if (h.Split(';')[0] == a) return dueScore;
                }
                /*
                if (due.Contains(h.Split(';')[0]))
                {
                    
                    return dueScore;
                }
                 * */
            }


            //__________________________
            String[] tre = new String[7];
            float treScore = 2.50F;
            String fraseTre = "Miscele non classificabili come pericolose ma contenenti almeno una sostanza pericolosa per via inalatoria appartenente alla classe di pericolo della tossicità di categoria 4, di reazione, di narcosi edi irritazione inalatoria";

            tre[0] = "H332";
            tre[1] = "H336";
            //tre[2] = "H319";
            tre[3] = "H335";
            tre[4] = "EUH206";
            tre[5] = "EUH029";
            tre[6] = "EUH031";
            tre[2] = "EUH032";
            

            foreach (String h in frasih)
            {
                foreach (String a in tre)
                {
                    if (h.Split(';')[0] == a) return treScore;
                }
            }

            //__________________________
            String[] quattro = new String[11];
            float quattroScore = 2.25F;
            String fraseQuattro = "Miscele non classificabili come pericolose ma contenenti almeno una sostanza pericolosa per via cutanea/mucose e/o per ingestione appartenente ad una qualsiasi classe di pericolo relativa ai soli effetti acuti con score >= 3";

            quattro[0] = "H312";
            quattro[1] = "H311";
            quattro[2] = "H310 cat.2";
            //quattro[3] = "H300 cat.2";
            quattro[3] = "EUH070";
            quattro[4] = "H310 cat.1";
            quattro[5] = "H300 cat.1";
            quattro[6] = "H314 cat.1A";
            quattro[7] = "H314 cat.1B";
            quattro[8] = "H314 cat.1C";
            quattro[9] = "H318";
            quattro[10] = "H319";//2024
            //se vlep

            foreach (String h in frasih)
            {
                foreach (String a in quattro)
                {
                    if (h.Split(';')[0] == a) return quattroScore;
                }
            }



            //__________nuova 2024________________
            String[] cinque = new String[5];
            float cinqueScore = 1.75F;
            String fraseCinque = "Miscele non classificabili come pericolose ma contenenti almeno una sostanza pericolosa per via cutanea/mucose e/o per ingestione appartenente ad una qualsiasi classe di pericolo relativa ai soli effetti acuti con score < 3";

            cinque[0] = "H302";
            cinque[1] = "H301";
            cinque[2] = "H300 cat.2";
            cinque[3] = "H315";
            cinque[4] = "EUH066";
            
            //se vlep

            foreach (String h in frasih)
            {
                foreach (String a in cinque)
                {
                    if (h.Split(';')[0] == a) return cinqueScore;
                }
            }






            //Anche nel caso di un vlep??
            //se contiene un vlep lo score è 2.25 anche se nessuna sostanza che contiene ha frasi h
            //Miscele non classificabili come pericolose ma contenenti almeno una sostanza alla quale è stato assegnato un valore limite d’esposizione professionale


            //Miscele non classificabili come pericolose ma contenenti almeno una sostanza alla quale è stato assegnato un valore limite d’esposizione professionale
            //Sostanza non classificabile come pericolosa, ma alla quale è stato assegnato un valore limite d’esposizione professionale 








            //MessageBox.Show("sono arrivato alla fine dei casi particolari delle miscele non pericolose senza trovare nulla ");
            return 1;

        }
    }
}
