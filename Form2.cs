using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ProgramForCreateTest
{
    public partial class Form2 : Form
    {
        //Массивы вопросов.
        string[] arrayQuestions;
        string[] questions;

        //Номер вопроса.
        int currentNumber = 0;

        //Номер варианта ответа.
        int choice = 1;

        //Подсчёт баллов.
        int ans;
        double wer;

        public Form2()
        {
            InitializeComponent();
            StreamReader sr = File.OpenText(".\\" + Form1.nameFile + "\\" + Form1.nameFile + ".txt");
            arrayQuestions = sr.ReadToEnd().Split(new string[] { "№" }, StringSplitOptions.RemoveEmptyEntries);
            questions = arrayQuestions[currentNumber].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            sr.Close();


            this.Text = "question № " + (currentNumber + 1);
            label1.Text = questions[0];

            //Вывод изображений.
            string resultString = Regex.Match(questions[0], @"\d+").Value;

            if (File.Exists(".\\" + Form1.nameFile + "\\" + resultString + ".jpg"))
            {
                pictureBox1.Image = Image.FromFile(".\\" + Form1.nameFile + "\\" + resultString + ".jpg");
            }
            else
            {
                pictureBox1.Image = Image.FromFile("-1.jpg");
            }

            //Вывод вариантов ответа.
            if (questions[questions.Length - 1].Contains('*'))
            {
                choice = 1;
                foreach (RadioButton el in panel1.Controls.OfType<RadioButton>())
                {
                    el.Visible = false;
                }
                foreach (CheckBox el in panel1.Controls.OfType<CheckBox>())
                {
                    if (choice <= questions.Length - 2)
                    {
                        el.Visible = true;

                        el.Text = questions[choice];
                        choice++;
                    }
                    else
                    {
                        el.Visible = false;
                    }
                }
                currentNumber++;
            }
            else
            {
                choice = 1;
                foreach (CheckBox el in panel1.Controls.OfType<CheckBox>())
                {
                    el.Visible = false;
                }
                foreach (RadioButton el in panel1.Controls.OfType<RadioButton>())
                {
                    if (choice <= questions.Length - 2)
                    {
                        el.Visible = true;

                        el.Text = questions[choice];
                        choice++;
                    }
                    else
                    {
                        el.Visible = false;
                    }
                }
                currentNumber++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка выбранного ответа и подсчёт баллов.
            if (questions[questions.Length - 1].Contains('*'))
            {
                foreach (CheckBox el in panel1.Controls.OfType<CheckBox>())
                {
                    if (el.Checked)
                    {
                        string text = questions[questions.Length - 1];

                        string[] words = text.Split(new char[] { '*' });

                        foreach (string s in words)
                        {
                            if (el.Text == s)
                            {
                                wer = wer + 0.5;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (RadioButton el in panel1.Controls.OfType<RadioButton>())
                {
                    if (el.Checked)
                    {
                        if (el.Text == questions[questions.Length - 1])
                        {
                            ans = ans + 1;
                        }
                    }
                }
            }

            //Выод следующего вопроса или результат теста.
            if (currentNumber < arrayQuestions.Length)
            {
                questions = arrayQuestions[currentNumber].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                this.Text = "question № " + (currentNumber + 1);
                label1.Text = questions[0];

                //Вывод изображений.
                string resultString = Regex.Match(questions[0], @"\d+").Value; // "\d": соответствует любой десятичной цифре. "+": предыдущий символ повторяется 1 и более раз.

                if (File.Exists(resultString + ".jpg"))
                {
                    pictureBox1.Image = Image.FromFile(resultString + ".jpg");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("-1.jpg");
                }

                //Вывод вариантов ответа.
                if (questions[questions.Length - 1].Contains('*'))
                {
                    choice = 1;
                    foreach (RadioButton el in panel1.Controls.OfType<RadioButton>())
                    {
                        el.Visible = false;
                    }
                    foreach (CheckBox el in panel1.Controls.OfType<CheckBox>())
                    {
                        if (choice <= questions.Length - 2)
                        {
                            el.Visible = true;

                            el.Text = questions[choice];
                            choice++;
                        }
                        else
                        {
                            el.Visible = false;
                        }
                    }
                    currentNumber++;
                }
                else
                {
                    choice = 1;
                    foreach (CheckBox el in panel1.Controls.OfType<CheckBox>())
                    {
                        el.Visible = false;
                    }
                    foreach (RadioButton el in panel1.Controls.OfType<RadioButton>())
                    {
                        if (choice <= questions.Length - 2)
                        {
                            el.Visible = true;

                            el.Text = questions[choice];
                            choice++;
                        }
                        else
                        {
                            el.Visible = false;
                        }
                    }
                    currentNumber++;
                }
            }
            else
            {
                button1.BackColor = Color.Gray;
                button1.Text = "the end.";
                button1.Enabled = false;
                MessageBox.Show($"the number of correct {ans + wer} out of {arrayQuestions.Length}.", " results!");
                this.Close();
            }
        }
    }
}