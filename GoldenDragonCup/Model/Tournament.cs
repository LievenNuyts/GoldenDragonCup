using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    class Tournament
    {
        public ArrayList weightClasses;
        public ArrayList allFighters;

        public Tournament() { }

        public Tournament(ArrayList allFighters)
        {
            this.allFighters = allFighters;
        }

        public void addWeightClass(WeightClass weightClass)
        {
            this.weightClasses.Add(weightClass);
        }
        
        public void addWeightClass(float lowerLimit, float upperLimit, bool fullContact, string gender, bool adult)
        {
            WeightClass weightClass = new WeightClass(lowerLimit, upperLimit, fullContact, gender, adult, this);
            this.weightClasses.Add(weightClass);
        }

        public void divideFightersInWeightClasses()
        {
            foreach(WeightClass weightClass in weightClasses)
            {
                weightClass.weightClassFighters.Clear();
                weightClass.selectTournamentFighters();
            }
        }
    }
}
