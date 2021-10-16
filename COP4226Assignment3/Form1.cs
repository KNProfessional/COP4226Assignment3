using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
                if(myfile.Contains(".csv"))
                {
                    g.ReadGraphFromCSVFile(openFileDialog1.FileName);
                }
                else if(myfile.Contains(".txt"))
                {
                    g.ReadGraphFromTXTFile(openFileDialog1.FileName);
                }
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
                if (myfile.Contains(".csv"))
                {
                    g.ReadGraphFromCSVFile(openFileDialog1.FileName);
                }
                else if (myfile.Contains(".txt"))
                {
                    g.ReadGraphFromTXTFile(openFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show("Invalid File Type");
                    break;
                }
                importedGraphList.Items.Add(openFileDialog1.FileName);
            }
        }

        private void SendTextToTextBox(string str)
        {
            List<string> numbersAndUnaryOperators = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "1/x", "²", "√", "%" };
            if (!numbersAndUnaryOperators.Contains(str))
            {
                str = " " + str + " ";
            }
            this.UpdateLastLine(this.GetCurrentLine() + str);
        }

        private void ClearTextBox()
        {
            this.listBox2.Items.Clear();
        }

        private void ClearLine()
        {
            this.UpdateLastLine("");
        }

        private string ProcessTextInTextBoxForCompute()
        {
            string text = this.GetCurrentLine();
            // go through and apply all unary operators in-place
            List<string> unaryOperators = new List<string> { "1/x", "²", "√", "%" };
            string[] separated = text.Split(' ');
            List<string> output = new List<string>();
            foreach (string value in separated)
            {
                char lastChar = value[value.Length - 1];
                if (lastChar == ')')
                {
                    string op = value.Substring(0, 3);
                    double num;
                    int end = value.IndexOf(')') - 1;
                    string numString = value.Substring(4, end - 4);
                    Double.TryParse(numString, out num);
                    switch (op)
                    {
                        case "Cos":
                            num = Math.Cos(num);
                            break;
                        case "Tan":
                            num = Math.Tan(num);
                            break;
                        case "Sin":
                            num = Math.Sin(num);
                            break;
                    }

                    output.Add(num.ToString());

                }
                else if (unaryOperators.Contains(lastChar.ToString()))
                {
                    string op = lastChar.ToString();
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
                Console.WriteLine("data in: ", output);
            }
            Console.WriteLine("data: ", output);
            return string.Join(" ", output.ToArray());
        }

        private void CalculateResult(object sender, EventArgs e)
        {
            string originalText = this.GetCurrentLine();
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

            this.UpdateLastLine($"{originalText} = {v.ToString()}");
            this.listBox2.Items.Add("");
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
        private string GetCurrentLine()
        {
            if (this.listBox2.Items.Count == 0)
            {
                return "";
            }
            return this.listBox2.Items[this.listBox2.Items.Count - 1] as string;
        }

        private void UpdateLastLine(string newTerm)
        {
            if (this.listBox2.Items.Count == 0)
            {
                this.listBox2.Items.Add(newTerm);
            }
            int index = this.listBox2.Items.Count - 1;
            this.listBox2.Items.RemoveAt(index);
            this.listBox2.Items.Add(newTerm);
        }

        private int GetCurrentLineSize()
        {
            if (this.listBox2.Items.Count == 0)
            {
                return 0;
            }
            else
            {
                return this.GetCurrentLine().Length;
            }
        }

        private void BackspaceButtonClick(object sender, EventArgs e)
        {
            string lastLine = this.GetCurrentLine();
            int endIndex = Math.Max(0, this.GetCurrentLineSize() - 1);
            this.UpdateLastLine(this.GetCurrentLine().Substring(0, endIndex));
        }

        private void SignChangeClick(object sender, EventArgs e)
        {
            string text = this.GetCurrentLine();
            if (text.Length == 0)
            {
                return;
            }
            char leadingSign = text[0];
            switch (leadingSign)
            {
                case '-':
                    this.UpdateLastLine(text.Substring(1, text.Length));
                    break;
                default:
                    this.UpdateLastLine("-" + text);
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

        private void CosX(object sender, EventArgs e)
        {
            double num = this.GetLastNumber();
            this.ReplaceLastTerm($"Cos({num})");
        }

        private void SinX(object sender, EventArgs e)
        {
            double num = this.GetLastNumber();
            this.ReplaceLastTerm($"Sin({num})");

        }

        private void TanX(object sender, EventArgs e)
        {
            double num = this.GetLastNumber();
            this.ReplaceLastTerm($"Tan({num})");
        }

        private void ReplaceLastTerm(string newTerm)
        {
            string[] input = this.GetCurrentLine().Split(' ');
            Array.Reverse(input);
            if (input.Length == 0)
            {
                return;
            }
            List<string> newTerms = new List<string>();
            List<string> inputList = new List<string>(input);
            inputList.RemoveAt(0);
            newTerms.Add(newTerm);
            foreach(var term in inputList)
            {
                newTerms.Add(term);
            }
            string[] terms = newTerms.ToArray();
            Array.Reverse(terms);
            this.UpdateLastLine(string.Join(" ", terms));
        }

        private double GetLastNumber()
        {
            string[] input = this.GetCurrentLine().Split(' ');
            Array.Reverse(input);
            if (input.Length == 0)
            {
                return 0.1;
            }
            string lastNum = input[0];
            string lastChar = lastNum[Math.Max(lastNum.Length - 1, 0)].ToString();

            if (new List<string> { "1/x", "²", "√", "%" }.Contains(lastChar))
            {
                lastNum = lastNum.Substring(0, Math.Max(lastNum.Length - 1, 0));
            }

            double num;
            if (!Double.TryParse(lastNum, out num))
            {
                return 0.1;
            }
            return num;
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
            calculatedResults.Items.Add("MST: " + importedGraphList.SelectedItem.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dijkstrasAlgorithm_Click(object sender, EventArgs e)
        {
            g.Dijkstra(importedGraphList.SelectedItem.ToString());
            calculatedResults.Items.Add("Shortest Paths: " + importedGraphList.SelectedItem.ToString());
        }

        private void saveResult_Click(object sender, EventArgs e)
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
                calculatedResults.Items.Remove(calculatedResults.SelectedItem);
            }
            else if (calculatedResults.SelectedItem.ToString().Contains("Shortest Paths"))
            {
                SelectedGraphFile = calculatedResults.SelectedItem.ToString();
                saveFileDialog1.ShowDialog();
                SelectedGraphFile = SelectedGraphFile.Replace("Shortest Paths: ", string.Empty);
                g.WriteSSSPSolutionTo(saveFileDialog1.FileName, SelectedGraphFile);
                calculatedResults.Items.Remove(calculatedResults.SelectedItem);
            }
            else
            {
                MessageBox.Show("Error: Graph Result not Selected");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (this.numericUpDown1.ActiveControl != null)
            {
                if (numericUpDown1.Value >= 0)
                    this.dateTimePicker2.Value = this.dateTimePicker1.Value.AddDays((double)numericUpDown1.Value);
                else
                    this.dateTimePicker1.Value = this.dateTimePicker2.Value.AddDays(-1 * (double)numericUpDown1.Value);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.numericUpDown1.ActiveControl != null)
                return;
            int c = this.dateTimePicker1.Value.CompareTo(this.dateTimePicker2.Value);
            int days = 0;
            DateTime temp;
            if (c > 0)
            {
                temp = this.dateTimePicker2.Value;
                while (!temp.Date.Equals(dateTimePicker1.Value.Date))
                {
                    days--;
                    temp = temp.AddDays(1);
                }
            }
            else
            {
                temp = this.dateTimePicker1.Value;
                while (!temp.Date.Equals(dateTimePicker2.Value.Date))
                {
                    days++;
                    temp = temp.AddDays(1);
                }
            }
            this.numericUpDown1.Value = days;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (this.numericUpDown1.ActiveControl != null)
                return;
            int c = this.dateTimePicker1.Value.CompareTo(this.dateTimePicker2.Value);
            int days = 0;
            DateTime temp;
            if (c > 0)
            {
                temp = this.dateTimePicker2.Value;
                while (!temp.Date.Equals(dateTimePicker1.Value.Date))
                {
                    days--;
                    temp = temp.AddDays(1);
                }
            }
            else
            {
                temp = this.dateTimePicker1.Value;
                while (!temp.Date.Equals(dateTimePicker2.Value.Date))
                {
                    days++;
                    temp = temp.AddDays(1);
                }
            }
            this.numericUpDown1.Value = days;
        }

        private void saveMinimumSpanningTreeAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String SelectedGraphFile = calculatedResults.SelectedItem.ToString();
            saveFileDialog1.ShowDialog();
            SelectedGraphFile = SelectedGraphFile.Replace("MST: ", string.Empty);
            /*Console.WriteLine("Calculated Results Item: " + calculatedResults.SelectedItem.ToString());
            Console.WriteLine("SelectedGraphFileName: " + SelectedGraphFile*/

            g.WriteMSTSolutionTo(saveFileDialog1.FileName, SelectedGraphFile);
            calculatedResults.Items.Remove(calculatedResults.SelectedItem);
        }

        private void saveShortestPathsAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String SelectedGraphFile = calculatedResults.SelectedItem.ToString();
            saveFileDialog1.ShowDialog();
            SelectedGraphFile = SelectedGraphFile.Replace("Shortest Paths: ", string.Empty);
            g.WriteSSSPSolutionTo(saveFileDialog1.FileName, SelectedGraphFile);
            calculatedResults.Items.Remove(calculatedResults.SelectedItem);
        }

        private void graphSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            splitContainer1.Panel2.BackColor = colorDialog1.Color;
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            splitContainer2.Panel1.BackColor = colorDialog1.Color;
        }

        private void dayCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            splitContainer2.Panel2.BackColor = colorDialog1.Color;
        }

        private void modifyBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void appearanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }
    }
}

