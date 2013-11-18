using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public class Fight
    {
        //WILL PROBABLY BE REPLACED BY FIGHTVIEW CLASS
        
        public int fighter1;
        public int fighter2;
        public int winner;
        public bool inProgress;
        public int roundIndex;
        public string fightName;

        public Fight() { }

        public Fight(int fighter1, int fighter2)
        {
            this.fighter1 = fighter1;
            this.fighter2 = fighter2;
        }

        public Fight(int fighter1, int fighter2, int roundIndex)
        {
            this.fighter1 = fighter1;
            this.fighter2 = fighter2;
            this.roundIndex = roundIndex;
        }
    }
}
