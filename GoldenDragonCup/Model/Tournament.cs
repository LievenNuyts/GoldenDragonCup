using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public class Tournament
    {
        public string name;
        public ArrayList weightClassCodes;
        public ArrayList weightClasses; //list of the weightclasses that are available in the competition
        public ArrayList allFighters; //list of all the fighters who have entered the competition

        public Tournament() { }

        public Tournament(string name, ArrayList allFighters)
        {
            try
            {            
                this.name = name;
                this.allFighters = allFighters;
                
                this.weightClassCodes = new ArrayList();
                this.weightClasses = new ArrayList();

                this.extractWeightClassCodes();
                this.addWeightClasses();
                this.divideFightersInWeightClasses();

                foreach (WeightClass weightClass in weightClasses)
                {
                    weightClass.createFightViews();
                    weightClass.assignFightersToFightViews();
                }

            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        
        //method to create a new weightclass based on parameters and add to the list of weightclasses
        private void addWeightClasses()
        {
            foreach(string code in weightClassCodes)
            {
               WeightClass weightClass = new WeightClass(code, this);
               this.weightClasses.Add(weightClass);
            }
        }


        //method to extract unique categories from allFighters arraylist
        private void extractWeightClassCodes()
        {
            foreach (Fighter fighter in allFighters)
            {
                if (!weightClassCodes.Contains(fighter.category))
                {
                    weightClassCodes.Add(fighter.category);
                }
            }
        }


        //method that will divide the list of all fighters into their weightclasses
        private void divideFightersInWeightClasses()
        {
            foreach(WeightClass weightClass in weightClasses)
            {
                weightClass.weightClassFighters.Clear();
                weightClass.selectTournamentFighters();
            }
        }
    }
}
