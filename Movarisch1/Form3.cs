using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movarisch1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //avanti-1 miscela pericolosa
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //abbandona miscele
            DialogResult r1 = MessageBox.Show("Sei davvero sicuro?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        }
    }
}
