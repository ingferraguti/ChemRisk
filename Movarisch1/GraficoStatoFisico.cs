using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Movarisch1
{
    public partial class GraficoStatoFisico : Form
    {

        
        


        public GraficoStatoFisico()
        {
            InitializeComponent();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            chart1.ChartAreas.Add("Area");
            chart1.ChartAreas["Area"].AxisX.Minimum = 20;
            chart1.ChartAreas["Area"].AxisX.Maximum = 160;
            chart1.ChartAreas["Area"].AxisX.Interval = 20;
            chart1.ChartAreas["Area"].AxisY.Minimum = 0;
            chart1.ChartAreas["Area"].AxisY.Maximum = 350;
            chart1.ChartAreas["Area"].AxisY.Interval = 50;


            chart1.Series.Add("Linea 1");
            chart1.Series["Linea 1"].Points.Add(new DataPoint(20, 150));
            chart1.Series["Linea 1"].Points.Add(new DataPoint(60, 350));
            chart1.Series["Linea 1"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("Linea 2");
            chart1.Series["Linea 2"].Points.Add(new DataPoint(20, 50));
            chart1.Series["Linea 2"].Points.Add(new DataPoint(160, 330));
            chart1.Series["Linea 2"].ChartType = SeriesChartType.Line;

            chart1.Series["Linea 2"].ChartArea = "Area";
            chart1.Series["Linea 1"].ChartArea = "Area";

            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }






        ToolTip tooltip = new ToolTip();
        Point? prevPosition = null;

        void chart1_MouseMove(object sender, MouseEventArgs e)
        {

            chart1.ChartAreas["Area"].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), true);
            chart1.ChartAreas["Area"].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), true);

            double pX = chart1.ChartAreas["Area"].CursorX.Position; //X Axis Coordinate of your mouse cursor
            double pY = chart1.ChartAreas["Area"].CursorY.Position; //Y Axis Coordinate of your mouse cursor

            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;


            tooltip.Show("To=" + pX.ToString() + ", Te=" + pY.ToString(), this.chart1, pos.X, pos.Y - 15);
           
        }

        private void chart1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GraficoStatoFisico_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = @"Vengono individuati quattro livelli, in ordine crescente relativamente  alla  possibilità  della  sostanza  di  rendersi  disponibile  in  aria,  in funzione della volatilità del liquido e della ipotizzabile o conosciuta granulometria delle polveri:

-  stato solido/nebbie (largo spettro granulometrico),
-  liquidi a bassa volatilità (bassa tensione di vapore),
-  liquidi ad alta e media volatilità (alta tensione di vapore) o polveri fini,
-  stato gassoso.


Per assegnare alle sostanze il corrispondente livello di granulometria delle polveri si può utilizzare il criterio individuato in: S.C: Maidment “Occupational Hygiene Considerations in the Development of a Structured Approach to Select Chemical Control Strategies” Ann. Occup. Hyg. Vol. 42, No 6 pp. 391-400, 1998.


Liquidi
Per quanto riguarda i liquidi invece è necessario rifarsi alla volatilità dell’agente chimico considerando la temperatura di ebollizione (Te) e la temperatura operativa (To) secondo la seguente suddivisione:

liquido a bassa volatilità Te ≥ 5 x To + 50

liquido a media volatilità  2 x To + 10 < Te < 5 x To + 50

liquido ad alta volatilità Te ≤ 2 x To + 10

oppure individuando la fascia di appartenenza nel grafico a fianco:
    -sei valori di To e Te rientrano nell'area sottostante alla linea 2 il liquido è ad alta volatilità.
    -sei valori di To e Te rientrano nell'area compresa tra la linea 1 e la linea 2 il liquido è a media volatilità
    -sei valori di To e Te rientrano nell'area delimitata inferiormente dalla Linea 1 (area soprastante) il liquido è a bassa volatilità
";
        }
     
    }
}
