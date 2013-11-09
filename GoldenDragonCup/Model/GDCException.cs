using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
        public class GDCException : ApplicationException
        {
            public GDCException(string message): base(message) {}
        }  
}
