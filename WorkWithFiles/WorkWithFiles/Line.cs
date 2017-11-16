using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    //Class for line generation
    public class Line
    {
        //Pattern singleton for this class
        private static Line instance;

        private Line() { random = new Random(); }

        public static Line Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Line();
                }
                return instance;
            }
        }

        //Static fields
        static private string _engCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz";
        static private string _ruCharacters =
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" +
            "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        static Random random;

        //Private fields
        private string _date;
        private string _lineRU;
        private string _lineEN;
        private long _a;
        private double _b;

        //Properties
        public string Date
        {
            get { return this._date; }
            set { this._date = value; }
        }

        public string LineRU
        {
            get { return this._lineRU; }
            set { this._lineRU = value; }
        }

        public string LineEN
        {
            get { return this._lineEN; }
            set { this._lineEN = value; }
        }

        public long A
        {
            get { return this._a; }
            set { this._a = value; }
        }

        public double B
        {
            get { return this._b; }
            set { this._b = value; }
        }

        //Additional fields
        private int _maximumA = 100000000, _minimumB = 1, _maximumB = 20;


        //Filling fields random values
        public string GenerateLine()
        {
            this.Date = GenRandomDate();
            this.LineEN = new string(GenRandomLine(_engCharacters, 10));
            this.LineRU = new string(GenRandomLine(_ruCharacters, 10));
            this.A = (2 * random.Next(this._maximumA / 2));
            this.B = Math.Round(random.NextDouble() * (this._maximumB - this._minimumB) + this._minimumB, 8);
            return this.Date + " " + this.LineEN + " " + this.LineRU + " " + this.A + " " + this.B;
        }

        //Receiving a random 10 character set
        private char[] GenRandomLine(string characters, int n)
        {
            char[] a = new char[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = characters[random.Next(characters.Length)];
            }
            return a;
        }

        //Receiving a random date in the last 5 years
        private string GenRandomDate()
        {
            DateTime from = DateTime.Now.AddYears(-5);
            DateTime to = DateTime.Now;
            int daysDiff = (to - from).Days;
            return from.AddDays(random.Next(daysDiff)).ToString("d");
        }

    }
}
