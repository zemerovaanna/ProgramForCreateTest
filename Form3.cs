using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProgramForCreateTest
{
    public partial class Form3 : Form
    {
        int i = 0;
        string[] question;
        List<string[]> questions;

        static int check(string question)
        {
            int number = 0;
            if (question[0] == '№')
            {
                number++;
            }
            return number;
        }

        public Form3()
        {
            InitializeComponent();
            questions = new List<string[]>();
            StreamReader sr = File.OpenText(".\\" + Form1.nameFile + "\\" + Form1.nameFile + ".txt");

            while (!sr.EndOfStream)
            {
                question = new string[7];
                for (int a = 0; a < 7; a++)
                {
                    question[a] = sr.ReadLine();
                }

                questions.Add(question);
            }

            sr.Close();

            textBox1.Text = questions[0][0];
            textBox2.Text = questions[0][1];
            textBox3.Text = questions[0][2];
            textBox4.Text = questions[0][3];
            textBox5.Text = questions[0][4];
            textBox6.Text = questions[0][5];
            string txt = questions[0][6];

            int f = 0;
            foreach (char q in txt)
            {
                if (q == '*') f++;
            }

            if (f >= 1)
            {
                string[] subs = txt.Split('*');
                foreach (string sub in subs)
                {
                    if (sub == textBox2.Text) checkBox1.Checked = true;
                    if (sub == textBox3.Text) checkBox2.Checked = true;
                    if (sub == textBox4.Text) checkBox3.Checked = true;
                    if (sub == textBox5.Text) checkBox4.Checked = true;
                    if (sub == textBox6.Text) checkBox5.Checked = true;
                }
            }
            else
            {
                if (txt == textBox2.Text) checkBox1.Checked = true;
                if (txt == textBox3.Text) checkBox2.Checked = true;
                if (txt == textBox4.Text) checkBox3.Checked = true;
                if (txt == textBox5.Text) checkBox4.Checked = true;
                if (txt == textBox6.Text) checkBox5.Checked = true;
            }

            string s = questions[0][0];
            char c = s[1];
            char r = s[2];
            string cr = "";

            if (char.IsDigit(r))
            {
                cr = $"{c}{r}";
            }
            else
            {
                cr = $"{c}";
            }

            if (File.Exists(".\\" + Form1.nameFile + "\\" + cr + ".jpg"))
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(".\\" + Form1.nameFile + "\\" + cr + ".jpg");
            }
            else
            {
                pictureBox1.Image = System.Drawing.Image.FromFile("-1.jpg");
            }
        }

        //<--
        private void button3_Click(object sender, EventArgs e)
        {
            int good = 0;

            good = check(textBox1.Text);

            if (good == 0)
            {
                MessageBox.Show("the first character in the question line must be '№'.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("the question field is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("answer field № 1 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBox3.Text == "")
                        {
                            MessageBox.Show("answer field № 2 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (textBox1.Text == "") questions[i][0] = "-";
                            else questions[i][0] = textBox1.Text;

                            if (textBox2.Text == "") questions[i][1] = "-";
                            else questions[i][1] = textBox2.Text;

                            if (textBox3.Text == "") questions[i][2] = "-";
                            else questions[i][2] = textBox3.Text;

                            if (textBox4.Text == "") questions[i][3] = "-";
                            else questions[i][3] = textBox4.Text;

                            if (textBox5.Text == "") questions[i][4] = "-";
                            else questions[i][4] = textBox5.Text;

                            if (textBox6.Text == "") questions[i][5] = "-";
                            else questions[i][5] = textBox6.Text;

                            string correctAnswer = "";
                            int zero = 0;
                            int one = 0;

                            if (checkBox1.Checked == true) one++;
                            else zero++;

                            if (checkBox2.Checked == true) one++;
                            else zero++;

                            if (checkBox3.Checked == true) one++;
                            else zero++;

                            if (checkBox4.Checked == true) one++;
                            else zero++;

                            if (checkBox5.Checked == true) one++;
                            else zero++;

                            if (zero == 5)
                            {
                                MessageBox.Show("check the correct answer / answers.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                good = 0;
                            }
                            else if (one > 1)
                            {
                                if (checkBox1.Checked == true) correctAnswer += textBox2.Text + "*";
                                if (checkBox2.Checked == true) correctAnswer += textBox3.Text + "*";
                                if (checkBox3.Checked == true) correctAnswer += textBox4.Text + "*";
                                if (checkBox4.Checked == true) correctAnswer += textBox5.Text + "*";
                                if (checkBox5.Checked == true) correctAnswer += textBox6.Text;

                                if (correctAnswer[correctAnswer.Length - 1] == '*')
                                {
                                    correctAnswer = correctAnswer.TrimEnd('*');
                                }
                                good++;
                            }
                            else if (one == 1)
                            {
                                if (checkBox1.Checked == true) correctAnswer = textBox2.Text;
                                if (checkBox2.Checked == true) correctAnswer = textBox3.Text;
                                if (checkBox3.Checked == true) correctAnswer = textBox4.Text;
                                if (checkBox4.Checked == true) correctAnswer = textBox5.Text;
                                if (checkBox5.Checked == true) correctAnswer = textBox6.Text;
                                good++;
                            }

                            if (good > 0)
                            {
                                questions[i][6] = correctAnswer;

                                checkBox1.Checked = false;
                                checkBox2.Checked = false;
                                checkBox3.Checked = false;
                                checkBox4.Checked = false;
                                checkBox5.Checked = false;

                                if (i > 1)
                                {
                                    i--;
                                    button4.Text = "-->";
                                }
                                else
                                {
                                    i = 0;
                                    button3.Text = "the end.";
                                }

                                textBox1.Text = questions[i][0];
                                textBox2.Text = questions[i][1];
                                textBox3.Text = questions[i][2];
                                textBox4.Text = questions[i][3];
                                textBox5.Text = questions[i][4];
                                textBox6.Text = questions[i][5];
                                string txt = questions[i][6];

                                int f = 0;
                                foreach (char q in txt)
                                {
                                    if (q == '*') f++;
                                }

                                if (f >= 1)
                                {
                                    string[] subs = txt.Split('*');
                                    foreach (string sub in subs)
                                    {
                                        if (sub == textBox2.Text) checkBox1.Checked = true;
                                        if (sub == textBox3.Text) checkBox2.Checked = true;
                                        if (sub == textBox4.Text) checkBox3.Checked = true;
                                        if (sub == textBox5.Text) checkBox4.Checked = true;
                                        if (sub == textBox6.Text) checkBox5.Checked = true;
                                    }
                                }
                                else
                                {
                                    if (txt == textBox2.Text) checkBox1.Checked = true;
                                    if (txt == textBox3.Text) checkBox2.Checked = true;
                                    if (txt == textBox4.Text) checkBox3.Checked = true;
                                    if (txt == textBox5.Text) checkBox4.Checked = true;
                                    if (txt == textBox6.Text) checkBox5.Checked = true;
                                }

                                //Вывод изображений.
                                string s = questions[i][0];
                                char c = s[1];
                                char r = s[2];
                                string cr = "";

                                if (char.IsDigit(r))
                                {
                                    cr = $"{c}{r}";
                                }
                                else
                                {
                                    cr = $"{c}";
                                }

                                if (File.Exists(".\\" + Form1.nameFile + "\\" + cr + ".jpg"))
                                {
                                    pictureBox1.Image = System.Drawing.Image.FromFile(".\\" + Form1.nameFile + "\\" + cr + ".jpg");
                                }
                                else
                                {
                                    pictureBox1.Image = System.Drawing.Image.FromFile("-1.jpg");
                                }
                            }
                        }
                    }
                }
            }
        }

        //-->
        private void button4_Click(object sender, EventArgs e)
        {
            int good = 0;

            good = check(textBox1.Text);

            if (good == 0)
            {
                MessageBox.Show("the first character in the question line must be '№'.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("the question field is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("answer field № 1 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBox3.Text == "")
                        {
                            MessageBox.Show("answer field № 2 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (textBox1.Text == "") questions[i][0] = "-";
                            else questions[i][0] = textBox1.Text;

                            if (textBox2.Text == "") questions[i][1] = "-";
                            else questions[i][1] = textBox2.Text;

                            if (textBox3.Text == "") questions[i][2] = "-";
                            else questions[i][2] = textBox3.Text;

                            if (textBox4.Text == "") questions[i][3] = "-";
                            else questions[i][3] = textBox4.Text;

                            if (textBox5.Text == "") questions[i][4] = "-";
                            else questions[i][4] = textBox5.Text;

                            if (textBox6.Text == "") questions[i][5] = "-";
                            else questions[i][5] = textBox6.Text;

                            string correctAnswer = "";
                            int zero = 0;
                            int one = 0;

                            if (checkBox1.Checked == true) one++;
                            else zero++;

                            if (checkBox2.Checked == true) one++;
                            else zero++;

                            if (checkBox3.Checked == true) one++;
                            else zero++;

                            if (checkBox4.Checked == true) one++;
                            else zero++;

                            if (checkBox5.Checked == true) one++;
                            else zero++;

                            if (zero == 5)
                            {
                                MessageBox.Show("check the correct answer / answers.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                good = 0;
                            }
                            else if (one > 1)
                            {
                                if (checkBox1.Checked == true) correctAnswer += textBox2.Text + "*";
                                if (checkBox2.Checked == true) correctAnswer += textBox3.Text + "*";
                                if (checkBox3.Checked == true) correctAnswer += textBox4.Text + "*";
                                if (checkBox4.Checked == true) correctAnswer += textBox5.Text + "*";
                                if (checkBox5.Checked == true) correctAnswer += textBox6.Text;

                                if (correctAnswer[correctAnswer.Length - 1] == '*')
                                {
                                    correctAnswer = correctAnswer.TrimEnd('*');
                                }
                                good++;
                            }
                            else if (one == 1)
                            {
                                if (checkBox1.Checked == true) correctAnswer = textBox2.Text;
                                if (checkBox2.Checked == true) correctAnswer = textBox3.Text;
                                if (checkBox3.Checked == true) correctAnswer = textBox4.Text;
                                if (checkBox4.Checked == true) correctAnswer = textBox5.Text;
                                if (checkBox5.Checked == true) correctAnswer = textBox6.Text;
                                good++;
                            }

                            if (good > 0)
                            {
                                questions[i][6] = correctAnswer;

                                checkBox1.Checked = false;
                                checkBox2.Checked = false;
                                checkBox3.Checked = false;
                                checkBox4.Checked = false;
                                checkBox5.Checked = false;

                                if (i < questions.Count - 1)
                                {
                                    i++;
                                    button3.Text = "<--";
                                }
                                else
                                {
                                    i = questions.Count - 1;
                                    button4.Text = "the end.";
                                }

                                textBox1.Text = questions[i][0];
                                textBox2.Text = questions[i][1];
                                textBox3.Text = questions[i][2];
                                textBox4.Text = questions[i][3];
                                textBox5.Text = questions[i][4];
                                textBox6.Text = questions[i][5];
                                string txt = questions[i][6];

                                int f = 0;
                                foreach (char q in txt)
                                {
                                    if (q == '*') f++;
                                }

                                if (f >= 1)
                                {
                                    string[] subs = txt.Split('*');
                                    foreach (string sub in subs)
                                    {
                                        if (sub == textBox2.Text) checkBox1.Checked = true;
                                        if (sub == textBox3.Text) checkBox2.Checked = true;
                                        if (sub == textBox4.Text) checkBox3.Checked = true;
                                        if (sub == textBox5.Text) checkBox4.Checked = true;
                                        if (sub == textBox6.Text) checkBox5.Checked = true;
                                    }
                                }
                                else
                                {
                                    if (txt == textBox2.Text) checkBox1.Checked = true;
                                    if (txt == textBox3.Text) checkBox2.Checked = true;
                                    if (txt == textBox4.Text) checkBox3.Checked = true;
                                    if (txt == textBox5.Text) checkBox4.Checked = true;
                                    if (txt == textBox6.Text) checkBox5.Checked = true;
                                }

                                //Вывод изображений.
                                string s = questions[i][0];
                                char c = s[1];
                                char r = s[2];
                                string cr = "";

                                if (char.IsDigit(r))
                                {
                                    cr = $"{c}{r}";
                                }
                                else
                                {
                                    cr = $"{c}";
                                }

                                if (File.Exists(".\\" + Form1.nameFile + "\\" + cr + ".jpg"))
                                {
                                    pictureBox1.Image = System.Drawing.Image.FromFile(".\\" + Form1.nameFile + "\\" + cr + ".jpg");
                                }
                                else
                                {
                                    pictureBox1.Image = System.Drawing.Image.FromFile("-1.jpg");
                                }
                            }
                        }
                    }
                }
            }
        }

        //save.
        private void button1_Click(object sender, EventArgs e)
        {
            int good = 0;

            good = check(textBox1.Text);

            if (good == 0)
            {
                MessageBox.Show("the first character in the question line must be '№'.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("the question field is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("answer field № 1 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBox3.Text == "")
                        {
                            MessageBox.Show("answer field № 2 is not filled in.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (textBox1.Text == "") questions[i][0] = "-";
                            else questions[i][0] = textBox1.Text;

                            if (textBox2.Text == "") questions[i][1] = "-";
                            else questions[i][1] = textBox2.Text;

                            if (textBox3.Text == "") questions[i][2] = "-";
                            else questions[i][2] = textBox3.Text;

                            if (textBox4.Text == "") questions[i][3] = "-";
                            else questions[i][3] = textBox4.Text;

                            if (textBox5.Text == "") questions[i][4] = "-";
                            else questions[i][4] = textBox5.Text;

                            if (textBox6.Text == "") questions[i][5] = "-";
                            else questions[i][5] = textBox6.Text;

                            string correctAnswer = "";
                            int zero = 0;
                            int one = 0;

                            if (checkBox1.Checked == true) one++;
                            else zero++;

                            if (checkBox2.Checked == true) one++;
                            else zero++;

                            if (checkBox3.Checked == true) one++;
                            else zero++;

                            if (checkBox4.Checked == true) one++;
                            else zero++;

                            if (checkBox5.Checked == true) one++;
                            else zero++;

                            if (zero == 5)
                            {
                                MessageBox.Show("check the correct answer / answers.", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                good = 0;
                            }
                            else if (one > 1)
                            {
                                if (checkBox1.Checked == true) correctAnswer += textBox2.Text + "*";
                                if (checkBox2.Checked == true) correctAnswer += textBox3.Text + "*";
                                if (checkBox3.Checked == true) correctAnswer += textBox4.Text + "*";
                                if (checkBox4.Checked == true) correctAnswer += textBox5.Text + "*";
                                if (checkBox5.Checked == true) correctAnswer += textBox6.Text;

                                if (correctAnswer[correctAnswer.Length - 1] == '*')
                                {
                                    correctAnswer = correctAnswer.TrimEnd('*');
                                }
                                good++;
                            }
                            else if (one == 1)
                            {
                                if (checkBox1.Checked == true) correctAnswer = textBox2.Text;
                                if (checkBox2.Checked == true) correctAnswer = textBox3.Text;
                                if (checkBox3.Checked == true) correctAnswer = textBox4.Text;
                                if (checkBox4.Checked == true) correctAnswer = textBox5.Text;
                                if (checkBox5.Checked == true) correctAnswer = textBox6.Text;
                                good++;
                            }

                            if (good > 0)
                            {
                                questions[i][6] = correctAnswer;

                                StreamWriter sw = new StreamWriter(".\\" + Form1.nameFile + "\\" + Form1.nameFile + ".txt");
                                for (int u = 0; u < questions.Count; u++)
                                {
                                    for (int k = 0; k < 7; k++)
                                    {
                                        sw.WriteLine(questions[u][k]);
                                    }
                                }
                                sw.Close();
                                MessageBox.Show("saved.", "information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        //back.
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
