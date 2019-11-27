using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace colors_recognition
{
    public partial class Form1 : Form
    {

        private ColorTest colorTest;
        public bool restartFlag = false;
        public int timeForAnswer = 30;
        public int actualAnswerTime = 0;

        public Form1()
        {
            this.colorTest = new ColorTest();
            
            InitializeComponent();
            // konfiguruję progress bar
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = colorTest.testCases;
            label3.Text = this.timeForAnswer.ToString();
            // zmieniam labela
            label1.Text = "0/"+ colorTest.testCases.ToString();
            // tworzę nowy test
            colorTest.startTest();
        }
        // ładowanie zdjęć do boxa
        private void loadResourceToImageBox(string resourceColorName)
        {
            // load image from resources
            pictureBox1.Image = colorTest.colorsDict[resourceColorName];
            //pictureBox1.Load();
        }

        private void resetTimer()
        {
            label3.Text = this.timeForAnswer.ToString();
            actualAnswerTime = 0;
        }

        private void switchState()
        {


            // w zależności od aktualnego progresu testu robię różne czynności
            //resetuję timer
            this.resetTimer();

            string temporaryResourceName = "";
            if (this.colorTest.getActualProgress() == 0)
            {
               
                
                // ustawiam flagę restartu na fałsz
                this.restartFlag = false;
                // rozpoczynam test nie sprawdzam żadnych odpowiedzi i idę dalej
                temporaryResourceName = this.colorTest.getNextCase();
                button1.Text = "Następny";
                // wyświetlam fotografię
                this.loadResourceToImageBox(temporaryResourceName);

                // aktualizuję progres
                this.progressBar1.Value = colorTest.getActualProgress();
                // zmieniam labela
                label1.Text = colorTest.getActualProgress().ToString() + "/" + colorTest.testCases.ToString();
                // uruchamiam timer
                timer1.Enabled = true;

                // kończę wywołanie funkcji
                return;
            }

            if (this.colorTest.getActualProgress() == this.colorTest.testCases)
            {
                // jestem przy ostatnim pytaniu. Czyszczę ImageBoxa, i wyświetlam rezultaty
                if(!this.restartFlag)
                {
                    //zatrzymuję timer
                    timer1.Enabled = false;
                    this.colorTest.checkAnswer(textBox1.Text);
                    MessageBox.Show("Wyniki \n Dobrze:"+colorTest.getGoodAnswers()+" \n Źle: "+colorTest.getBadAnswers());
                    this.restartFlag = true;
                    button1.Text = "Start !";
                    pictureBox1.Image = null;

                }
                else
                {
                    colorTest.startTest();
                    this.switchState();
                }
                return;
            }

            // w przypadku zwykłego kliknięcia sprawdzam wpisaną odpowiedź i idę dalej
            this.colorTest.checkAnswer(textBox1.Text);
            temporaryResourceName = this.colorTest.getNextCase();
            this.loadResourceToImageBox(temporaryResourceName);
            // aktualizuję progres
            this.progressBar1.Value = colorTest.getActualProgress();
            // zmieniam labela
            label1.Text = colorTest.getActualProgress().ToString() + "/" + colorTest.testCases.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.switchState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //aktualizuję timer
            this.actualAnswerTime = this.actualAnswerTime + 1;
            int timeToShow = this.timeForAnswer - this.actualAnswerTime;
            label3.Text = timeToShow.ToString();
            // gdy czas się skończy dla danego pytania
            if (this.actualAnswerTime == this.timeForAnswer)
            {
                //resetuj timer (a w zasadzie zmienne timera)
                this.resetTimer();
                // przejdź do następnego
                this.switchState();
            }
        }
    }
}
