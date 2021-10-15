using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PA3Draft;

namespace COP4226Assignment3
{
    public partial class Form1 : Form
    {
        GraphAlgorithms g;
        public Form1()
        {
            InitializeComponent();
            g = new GraphAlgorithms(pBar, pLabel, statusStrip2);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            g.ReadGraphFromTXTFile(openFileDialog1.FileName);
            importedGraphList.Items.Add(openFileDialog1.FileName);
        }

        private void graphMatrixtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            g.ReadGraphFromTXTFile(openFileDialog1.FileName);
            importedGraphList.Items.Add(openFileDialog1.FileName);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
            g.ReadGraphFromCSVFile(openFileDialog1.FileName);
            importedGraphList.Items.Add(openFileDialog1.FileName);
        }

        private void graphMatrixcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
            g.ReadGraphFromCSVFile(openFileDialog1.FileName);
            importedGraphList.Items.Add(openFileDialog1.FileName);
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            importedGraphList.Items.Remove(importedGraphList.SelectedItem);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            importedGraphList.Items.Clear();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            g.GetMST(importedGraphList.SelectedItem.ToString());
            calculatedResults.Items.Add(importedGraphList.SelectedItem.ToString());
        }
    }
}
