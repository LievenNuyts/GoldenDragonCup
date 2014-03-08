using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup.Tools
{
    public static class NameHelper
    {
        public static string adjust(string name)
        {
            try
            {
                string adjustedName = null;

                if (name.Length > 12) //name is longer than 12 chars
                {
                    if (name.Contains(" "))
                    {
                        string trimmed = name.Replace(@" ", ""); //remove spaces in the last name + abbreviate to 12 chars

                        if (trimmed.Length > 12) //if length is still over 12 after trimming
                        {
                            adjustedName = trimmed.Substring(0, 11);
                        }
                        else
                        {
                            return trimmed;
                        }
                    }
                    else
                    {
                        adjustedName = name.Substring(0, 11); //abbreviate to 12 chars
                    }

                    return adjustedName + ".";

                }
                else
                {
                    return name;
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method nameHelper(string name): " + exc.Message);
            }
        }
    }
}
