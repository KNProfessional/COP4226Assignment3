using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            List<string> numbersAndUnaryOperators = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "1/x", "²", "√", "%" };
            if (!numbersAndUnaryOperators.Contains(str))
            {
                str = " " + str + " ";
            }
            this.textBox1.Text += str;
        }

        private void ClearTextBox()
        {
            this.textBox1.Text = "";
        }

        private string ProcessTextInTextBoxForCompute()
        {
            string text = this.textBox1.Text;
            // go through and apply all unary operators in-place
            List<string> unaryOperators = new List<string> { "1/x", "²", "√", "%" };
            string[] separated = text.Split(' ');
            List<string> output = new List<string>();
            foreach (string value in separated)
            {
                if (unaryOperators.Contains(value[value.Length - 1].ToString()))
                {
                    string op = value[value.Length- 1].ToString();
                    double number;
                    try
                    {
                        number = Double.Parse(value.Substring(0, value.IndexOf(op)));
                        switch(op)
                        {
                            case "1/x":
                                number = 1 / number;
                                break;
                            case "²":
                                number = number * number;
                                break;
                            case "√":
                                number = Math.Sqrt(number);
                                break;
                            case "%":
                                number = number / 100;
                                break;
                        }
                        output.Add(number.ToString());
                    }
                    catch (Exception)
                    {
                        output.Add(value);
                    }
                }
                else
                {
                    output.Add(value);
                }
            }
            return string.Join(" ", output.ToArray());
        }

        private void CalculateResult(object sender, EventArgs e)
        {
            string text = this.ProcessTextInTextBoxForCompute();
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
            this.listBox2.Items.Add($"{text} = {v.ToString()}");
        }

        private void AddButtonValueToTextBox(object sender, EventArgs e)
        {
            string buttonValue = (sender as Button).Text;
            // local translations of symbol -> button
            switch (buttonValue)
            {
                case "÷":
                    buttonValue = "/";
                    break;
                case "x":
                    buttonValue = "*";
                    break;
                case "x^2":
                    buttonValue = "²";
                    break;
                default:
                    break;
            }
            this.SendTextToTextBox(buttonValue);
        }

        private void ClearButtonHandler(object sender, EventArgs e)
        {
            this.ClearTextBox();
        }

        private void BackspaceButtonClick(object sender, EventArgs e)
        {
            int endIndex = Math.Max(0, this.textBox1.Text.Length - 1);
            this.textBox1.Text = this.textBox1.Text.Substring(0, endIndex);
        }

        private void SignChangeClick(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            if (text.Length == 0)
            {
                return;
            }
            char leadingSign = text[0];
            switch (leadingSign)
            {
                case '-':
                    this.textBox1.Text = text.Substring(1, text.Length);
                    break;
                default:
                    this.textBox1.Text = "-" + text;
                    break;
            }
        }

        private void LoadCalculatorHistory(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            foreach (var item in fileContent.Split('\n'))
            {
                this.listBox2.Items.Add(item);
            }
        }

        private void ClearCalculatorHistory(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
        }

        private void SaveCalculatorHistory(object sender, EventArgs e)
        {
            List<string> history = new List<string>();
            foreach (var line in this.listBox2.Items)
            {
                history.Add(line.ToString());
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.txt";
            saveFileDialog1.Title = "Save a Calculation History File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                using (StreamWriter outputFile = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (string line in history)
                        outputFile.WriteLine(line);
                }
            }

        }
    }
}

