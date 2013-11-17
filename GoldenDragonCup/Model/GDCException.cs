using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
        //this exception will be used to throw exceptions based on not living up to certain business rules (not real exception)
        //will always be the first catch block
        public class GDCException : ApplicationException
        {
            public GDCException(string message): base(message) {}
        }  
}
