using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COP4226Assignment3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //g = new GraphAlgorithms(pBar, pLabel, statusStrip2);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
        }

        private void graphMatrixtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
        }

        private void graphMatrixcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "all supported (*.csv,*.txt)|*.csv;*.txt|csv files (*.csv)|*.csv|txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
        }

        private void multipleGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "all supported (*.csv,*.txt)|*.csv;*.txt|csv files (*.csv)|*.csv|txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
        }
    }
}
