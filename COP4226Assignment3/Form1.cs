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

        private void SendTextToTextBox(string str)
        {
            this.textBox1.Text += str;
        }

        private void ClearTextBox()
        {
            this.textBox1.Text = "";
        }

        private void CalculateResult(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            DataTable dt = new DataTable();
            var v = new object();
            try
            {
                Console.WriteLine("text: ", text);
                v = dt.Compute(text, "");
                Console.WriteLine("v: ", v);
            }
            catch (Exception x)
            {
                v = "NaN";
                Console.WriteLine("exception e: ", x);
            }


            this.textBox1.Text = v.ToString();
        }

        private void AddButtonValueToTextBox(object sender, EventArgs e)
        {
            string buttonValue = (sender as Button).Text;
            this.SendTextToTextBox(buttonValue);
        }

        private void ClearButtonHandler(object sender, EventArgs e)
        {
            this.ClearTextBox();
        }
    }
}
}
