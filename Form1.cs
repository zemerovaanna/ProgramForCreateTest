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

namespace ProgramForCreateTest
{
    public partial class Form1 : Form
    {
        public static string nameFile;
        public Form1()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("listOfTests.txt");
            while(!sr.EndOfStream)
            {
                comboBox1.Items.Add(sr.ReadLine());
            }
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nameFile = comboBox1.Text;
            if (nameFile == "")
            {
                MessageBox.Show("the test was not selected for this action.","note!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                Form2 f = new Form2();
                f.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nameFile = comboBox1.Text;
            if (nameFile == "")
            {
                MessageBox.Show("the test was not selected for this action.", "note!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form3 f = new Form3();
                f.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            StreamReader sr = new StreamReader("listOfTests.txt");
            while (!sr.EndOfStream)
            {
                string srtxt = sr.ReadLine();
                DirectoryInfo dirInfo = new DirectoryInfo(srtxt);
                if (dirInfo.Exists)
                {
                    comboBox1.Items.Add(srtxt);
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter("listOfTests.txt");
            foreach (var item in comboBox1.Items)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }
    }
}
