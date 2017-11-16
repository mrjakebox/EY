using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    static class CountingData
    {
        static private long _sumA = 0, _countB = 0;
        static private int _increment = 0;
        static private List<int> _list = new List<int>(50);

        static public long SumA
        {
            get { return _sumA; }
            private set { _sumA = value; }
        }

        static public long CountB
        {
            get { return _countB; }
            private set { _countB = value; }
        }

        static public List<int> List
        {
            get { return _list; }
            private set { _list = value; }
        }

        //Counting data using line
        static public void Counting(string line)
        {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //Counting of the amount A
            try
            {
                //good luck P.S. long max value is 9,223,372,036,854,775,807 but you never know:)
                checked { SumA += Convert.ToInt64(words[3]); };
            }
            catch (OverflowException)
            {
                throw new CustomException("Overflow occurred during counting of the amount A");
            }
            //Counting of the quantity B in the range [3.0; 5.0]
            if (Convert.ToDouble(words[4]) >= 3.0 && Convert.ToDouble(words[4]) <= 5.0)
            {
                try
                {
                    checked { CountB++; };
                }
                catch (OverflowException)
                {
                    throw new CustomException("Overflow occurred during counting of the quantity B");
                }
            }
            //Generate top list A
            if (_increment < 50)
            {
                _increment++;
                List.Add(Convert.ToInt32(words[3]));
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    if (Convert.ToInt32(words[3]) > List[i])
                    { List[i] = Convert.ToInt32(words[3]); break; }
                }
            }
            List.Sort();
            List.Reverse();
        }
    }
}
