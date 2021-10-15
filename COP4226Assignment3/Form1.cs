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

            foreach (String myfile in openFileDialog1.FileNames)
            {
                importedGraphList.Items.Add(openFileDialog1.FileName);
            }
        }

        private void multipleGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "all supported (*.csv,*.txt)|*.csv;*.txt|csv files (*.csv)|*.csv|txt files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();

            foreach (String myfile in openFileDialog1.FileNames)
            {
                importedGraphList.Items.Add(openFileDialog1.FileName);
            }
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
            if(importedGraphList.SelectedItem.Equals(null))
            {
                MessageBox.Show("Error: An imported graph file isn't selected.");
            }
            else
            {
                g.GetMST(importedGraphList.SelectedItem.ToString());
                calculatedResults.Items.Add("MST: " + importedGraphList.SelectedItem.ToString());
            }

            /*try
            {
                g.GetMST(importedGraphList.SelectedItem.ToString());
                calculatedResults.Items.Add("MST: " + importedGraphList.SelectedItem.ToString());
            }
            catch(System.NullReferenceException ex)
            {
                throw new NullReferenceException("Error: An imported graph file isn't selected.");
            }*/
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            g.Dijkstra(importedGraphList.SelectedIndex.ToString());
            calculatedResults.Items.Add("Shortest Paths: " + importedGraphList.SelectedItem.ToString());
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            String SelectedGraphFile;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            if (calculatedResults.SelectedItem.ToString().Contains("MST"))
            {
                SelectedGraphFile = calculatedResults.SelectedItem.ToString();
                saveFileDialog1.ShowDialog();
                SelectedGraphFile = SelectedGraphFile.Replace("MST: ", string.Empty);
                /*Console.WriteLine("Calculated Results Item: " + calculatedResults.SelectedItem.ToString());
                Console.WriteLine("SelectedGraphFileName: " + SelectedGraphFile*/

                g.WriteMSTSolutionTo(saveFileDialog1.FileName, SelectedGraphFile);
            }
            else if(calculatedResults.SelectedItem.ToString().Contains("Shortest Paths"))
            {
                SelectedGraphFile = calculatedResults.SelectedItem.ToString();
                saveFileDialog1.ShowDialog();
                SelectedGraphFile = SelectedGraphFile.Replace("Shortest Paths: ", string.Empty);
                g.WriteSSSPSolutionTo(saveFileDialog1.FileName, SelectedGraphFile);
            }
            else
            {
                MessageBox.Show("Error: Graph Result not Selected");
            }
        }
    }
}
