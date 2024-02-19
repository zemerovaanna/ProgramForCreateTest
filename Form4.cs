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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int good = 0;

            DirectoryInfo dirInfo = new DirectoryInfo(textBox8.Text);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            StreamWriter sw = new StreamWriter(".\\" + textBox8.Text + "\\" + textBox8.Text + ".txt", true);

            if (textBox1.Text == "")
            {
                MessageBox.Show("the question field is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                good = 0;
            }
            else
            {
                good++;
            }


            if (textBox2.Text == "")
            {
                MessageBox.Show("answer field № 1 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                good = 0;
            }
            else
            {
                good++;
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("answer field № 2 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                good = 0;
            }
            else
            {
                good++;
            }

            //Правильный ответ.
            string correctAnswer = "";
            int zero = 0;
            int one = 0;

            if(checkBox1.Checked == true)
            {
                one++;
            }
            else
            {
                zero++;
            }
            if (checkBox2.Checked == true)
            {
                one++;
            }
            else
            {
                zero++;
            }
            if (checkBox3.Checked == true)
            {
                one++;
            }
            else
            {
                zero++;
            }
            if (checkBox4.Checked == true)
            {
                one++;
            }
            else
            {
                zero++;
            }
            if (checkBox5.Checked == true)
            {
                one++;
            }
            else
            {
                zero++;
            }

            if(zero == 5)
            {
                MessageBox.Show("check the correct answer / answers.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                good = 0;
            }
            else if(one > 1)
            {
                if (checkBox1.Checked == true) correctAnswer += textBox2.Text + "*";
                if (checkBox2.Checked == true) correctAnswer += textBox3.Text + "*";
                if (checkBox3.Checked == true) correctAnswer += textBox4.Text + "*";
                if (checkBox4.Checked == true) correctAnswer += textBox5.Text + "*";
                if (checkBox5.Checked == true) correctAnswer += textBox6.Text;

                if (correctAnswer[correctAnswer.Length - 1] == '*')
                {
                    correctAnswer = correctAnswer.TrimEnd('*'); ;
                }
            }
            else if(one == 1)
            {
                if (checkBox1.Checked == true) correctAnswer = textBox2.Text;
                if (checkBox2.Checked == true) correctAnswer = textBox3.Text;
                if (checkBox3.Checked == true) correctAnswer = textBox4.Text;
                if (checkBox4.Checked == true) correctAnswer = textBox5.Text;
                if (checkBox5.Checked == true) correctAnswer = textBox6.Text;
            }

            if (good != 0)
            {
                sw.WriteLine("№" + textBox1.Text);
                sw.WriteLine(textBox2.Text);
                sw.WriteLine(textBox3.Text);
                if (textBox4.Text != "")
                {
                    sw.WriteLine(textBox4.Text);
                }
                else
                {
                    sw.WriteLine("-");
                }

                if (textBox5.Text != "")
                {
                    sw.WriteLine(textBox5.Text);
                }
                else
                {
                    sw.WriteLine("-");
                }

                if (textBox6.Text != "")
                {
                    sw.WriteLine(textBox6.Text);
                }
                else
                {
                    sw.WriteLine("-");
                }

                sw.WriteLine(correctAnswer);

                sw.Close();

                bool ok = false;

                foreach (string line in File.ReadLines("listOfTests.txt"))
                {
                    if (!line.Contains(textBox8.Text))
                    {
                        ok = true;
                    }
                }

                if(ok == true)
                {
                    StreamWriter s = new StreamWriter("listOfTests.txt", true);
                    s.WriteLine(textBox8.Text);
                    s.Close();
                }

                MessageBox.Show("added.", "notification!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
            else if (good == 0)
            {
                MessageBox.Show("not added.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
