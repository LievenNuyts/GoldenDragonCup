using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup.Tools
{
    class WCComparator : IComparer<WeightClass>
    {
        public int Compare(WeightClass x, WeightClass y)
        {
            try
            {
                int result = 0;

                string cat1 = x.category;
                string cat2 = y.category;
                
                if (cat1.Substring(0, 5) != cat2.Substring(0, 5)) //if first five letters are not the same
                {
                    result = String.Compare(cat1, cat2); //check alphabeticaly
                }
                else //if first five letters are the same
                {
                    
                    int intX = int.Parse(cat1.GetLast(2));
                    int intY = int.Parse(cat2.GetLast(2));

                    if (intX == intY) //if last two digits are the same, we do a test on "-" or "+"
                    {
                        string subX = cat1.GetLast(3);
                        string subY = cat2.GetLast(3);

                        string charX = subX.Substring(0, 1);
                        string charY = subY.Substring(0, 1);

                        if (charX == "+")
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                    else //if last two digits are not the same, lowest number goes in list first
                    {
                        if (intX > intY)
                        {
                            result = 1;
                        }
                        else //(intX < intY)
                        {
                            result = -1; //lowest number goes first
                        }
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                throw new Exception("Error in WCComparator: " + exc.Message);
            }
        }
    }


    public static class StringExtension
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
