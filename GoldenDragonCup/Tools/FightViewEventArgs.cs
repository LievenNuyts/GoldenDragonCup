using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public class FightViewEventArgs: EventArgs
    {
        public Boolean trueOrFalse;

        public FightViewEventArgs(Boolean trueOrFalse)
        {
            this.trueOrFalse = trueOrFalse;
        }
    }
}
