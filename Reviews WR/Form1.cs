using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reviews_WR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }                   

        private void button3_Click(object sender, EventArgs e)
        {

            bool flag = true; float f;
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                flag = false;
                MessageBox.Show("Empty Field(s).");
            }
            else if (textBox1.Text.All(char.IsDigit) == true)
            {
                flag = false;
                MessageBox.Show("Movie name cannot be Numeric.");
            }
            else if (float.TryParse(textBox2.Text, out f) != true)
            {
                flag = false;
                MessageBox.Show("Rating cannot be in characters");
            }
            else if (float.TryParse(textBox2.Text, out f) == true)
            {
                if ((f >= 0.5 && f < 5.0) != true)
                {
                    flag = false;
                    MessageBox.Show("Invalid ratting");
                }
            }
            if (flag == true)
            {
                if (File.Exists("azizalmaro_movies.txt"))
                {
                    File.AppendAllText("azizalmaro_movies.txt", textBox1.Text + "," + textBox2.Text + Environment.NewLine);
                    MessageBox.Show("Record Insert");
                }
                else
                {
                    StreamWriter sw = new StreamWriter("azizalmaro_movies.txt");
                    sw.Write(textBox1.Text + "," + textBox2.Text + Environment.NewLine);
                    sw.Close();
                    MessageBox.Show("Record Insert");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog1.FileName;
                string[] word = FileName.Split('\\');
                int len = word.Length;
                if (word[len - 1] == "azizalmaro_movies.txt")
                {
                    string line;
                    StreamReader file = new StreamReader("azizalmaro_movies.txt");
                    List<string> highlist = new List<string>();
                    List<string> lesslist = new List<string>();
                    List<string> list = new List<string>();
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] words = line.Split(',');
                        if (float.Parse(words[1]) >= 3.0)
                        {
                            highlist.Add("*" + line);
                        }
                        else
                        {
                            lesslist.Add(line);
                        }
                    }
                    file.Close();
                    highlist.AddRange(lesslist);
                    richTextBox1.Lines = highlist.ToArray();
                }
                else
                {
                    string filetext = File.ReadAllText(FileName);
                    richTextBox1.Text = filetext;
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
        }
    }
}
