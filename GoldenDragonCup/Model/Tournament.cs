using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    class Tournament
    {
        public ArrayList weightClasses; //list of the weightclasses that are available in the competition
        public ArrayList allFighters; //list of all the fighters who have entered the competition

        public Tournament() { }

        public Tournament(ArrayList allFighters)
        {
            this.allFighters = allFighters;
        }

        //method to add weightclasses object to the list of weightclasses
        public void addWeightClass(WeightClass weightClass)
        {
            this.weightClasses.Add(weightClass);
        }
        
        //method to create a new weightclass based on parameters and add to the list of weightclasses
        public void addWeightClass(float lowerLimit, float upperLimit, bool fullContact, bool gender, bool adult)
        {
            WeightClass weightClass = new WeightClass(lowerLimit, upperLimit, fullContact, gender, adult, this);
            this.weightClasses.Add(weightClass);
        }

        //methode that will divide the list of all fighters into their weightclasses
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
