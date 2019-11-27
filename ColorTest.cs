using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace colors_recognition
{
    class ColorTest
    {
        // zmienne prywatne mogą być modyfikowane tylko w klasie

        // ilość pytań w teście
        public int testCases = 5;

        // słownik w którym przechowujemy sobie nazwy kolorów i odnośniki do obrazków, postaci klucz -> wartość
        public Dictionary<string, System.Drawing.Bitmap> colorsDict = new Dictionary<string, System.Drawing.Bitmap>();
        private int actualProgress;
        private List<string> actualTestQueue;
        // tutaj będę przechowywał wyniki aktualnego testu
        private int goodAnswersCnt;
        private int badAnswersCnt;

        // konstruktor. W uproszczeniu ten kod wykona się za każdym razem kiedy stworzę obiekt ColorTest. Inicjalizuję sobie tutaj zmienne. 
        public ColorTest()
        {
            // Aby odwołać się do mojego słownika colorsDict, który jest w mojej klasie korzystam ze wskazania że zmienna jest elementem mojej klasy czyli this właśnie.
            // dodaję kolory do mojego słownika
            this.colorsDict.Add("niebieski", Properties.Resources.blue);
            this.colorsDict.Add("czerwony", Properties.Resources.red);
            this.colorsDict.Add("zielony", Properties.Resources.green);
            this.colorsDict.Add("biały", Properties.Resources.white);
            this.colorsDict.Add("żółty", Properties.Resources.yellow1);
            this.colorsDict.Add("czarny", Properties.Resources.black);
            this.colorsDict.Add("brązowy", Properties.Resources.brown);
            this.colorsDict.Add("pomarańczowy", Properties.Resources.orange);
            // ustawiam zmienną która będzie mi mówić o aktualnym progresie testu
            this.actualProgress = 0;
            // W tej liście będę przechowywał sobie kolejość w jakiej mają się pojawiać obrazki.
            this.actualTestQueue = new List<string>();
            this.goodAnswersCnt = 0;
            this.badAnswersCnt = 0;

        }
        // przygotowanie kolejki
        private void setActualTestQueue()
        {
            // opróżniam aktualną kolejkę
            this.actualTestQueue.Clear();
            // pętla w której losuję kolory ze słownika
            // dopóki moja kolejka nie będzie miała odpowiedniej ilości testów to powtarzaj
            while(this.actualTestQueue.Count<this.testCases)
            {
                // losuję liczbę od 0 do ilości kolorów w słowniku
                // sprawdzam czy kolor nie został wcześniej wylosowany
                Random randNum = new Random();
                string actualColor = this.colorsDict.ElementAt(randNum.Next(0,this.colorsDict.Count)).Key;

                if (!this.actualTestQueue.Contains(actualColor))
                {
                    // nie znaleziono w kolejce wylosowanego koloru. Dodaję go do tablicy
                    this.actualTestQueue.Add(actualColor);
                } else
                {
                    // wykonuj nadal pętlę
                    continue;
                }

                
            }

        }
        // Wywołanie kolejnego przykładu
        public string getNextCase()
        {
            // Aktualizuję progres testu
            if(this.actualProgress<this.testCases)
            {
                this.actualProgress = this.actualProgress + 1;
                return actualTestQueue[this.actualProgress-1];
            } else
            {
                return actualTestQueue[this.actualProgress];
            }
            
        }
        // przygotowanie nowego testu czyli wypełnienie kolejki ustawienie progresu, wyzerowanie odpowiedzi
        public void startTest()
        {

            this.actualProgress = 0;
            this.setActualTestQueue();
            this.goodAnswersCnt = 0;
            this.badAnswersCnt = 0;
        }

        // sprawdzenie odpowiedzi
        // true w przypadku poprawnej odpowiedzi, false w przypadku złej
        public bool checkAnswer(string answer)
        {
            if(answer.ToLower() == actualTestQueue[this.actualProgress-1])
            {
                this.goodAnswersCnt = this.goodAnswersCnt + 1;
                return true;
            } else
            {
                this.badAnswersCnt = this.badAnswersCnt + 1;
                return false;
            }
        }

        public int getActualProgress()
        {
            return this.actualProgress;
        }

        public string getGoodAnswers()
        {
            return this.goodAnswersCnt.ToString();
        }

        public string getBadAnswers()
        {
            return this.badAnswersCnt.ToString();
        }

    }
}
