using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using GoldenDragonCup.Tools;
using GoldenDragonCup.View;

namespace GoldenDragonCup
{
    public class Tournament
    {
        public string name;
        public List<string> weightClassCodes;
        public List<WeightClass> weightClasses; //list of the weightclasses that are available in the competition
        public List<Fighter> allFighters; //list of all the fighters who have entered the competition

        public Tournament() { }

        //constructor with weightClass list
        //only used for data restore and tournament split afterwards
        public Tournament(string name, List<WeightClass> weightClasses)
        {
            this.name = name;
            this.weightClasses = weightClasses;
        }
        

        //constructor with fighter list
        //used for startup of a new tournament
        public Tournament(string name, List<Fighter> allFighters)
        {
            try
            {
                this.name = name;
                this.allFighters = allFighters;
                
                this.weightClassCodes = new List<string>();
                this.weightClasses = new List<WeightClass>();

                this.extractWeightClassCodes();
                this.addWeightClasses();
                this.divideFightersInWeightClasses();

                foreach (WeightClass weightClass in weightClasses)
                {
                    weightClass.createFightViews();
                    weightClass.assignFightersToFightViews();
                }

                //insert tests to check if no weightclass has less than two or more than 20 fighters
                //or no fightViews created
                validateWeightClasses();
                validateFightViews();

                //sort the weightclasses in the tournament based on the WCComparator
                WCComparator comparator = new WCComparator();
                weightClasses.Sort(comparator);  
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public void activateFightViewEvents(MainWindow window)
        {
            foreach (WeightClass weightClass in weightClasses)
            {
                foreach (List<FightView> list in weightClass.rounds)
                {
                    foreach (FightView fightView in list)
                    {
                        //link method to fightview event
                        fightView.okButtonClicked += window.onFightViewEventMethod;
                    }
                }
            }
        }


        #region METHODS FOR VALIDATION

        //method to check if a weightclass has less than 2 or more than 20 fighters
        private void validateWeightClasses()
        {
            foreach (WeightClass weightClass in weightClasses)
            {
                if (weightClass.weightClassFighters.Count < 2)
                {
                    throw new GDCException("WeightClass " + weightClass.category + " only contains " + weightClass.weightClassFighters.Count.ToString() + " fighters. Check the input Excel file.");
                }
                if (weightClass.weightClassFighters.Count > 20)
                {
                    throw new GDCException("WeightClass " + weightClass.category + " contains " + weightClass.weightClassFighters.Count.ToString() + " fighters (max = 20). Check the input Excel file.");
                }
            }
        }

        //method to check if a weightClass doesn't have any fightviews created
        private void validateFightViews()
        {
            foreach (WeightClass weightClass in weightClasses)
            {
                if (weightClass.rounds == null || weightClass.rounds[0].Count == 0)
                {
                    throw new GDCException("WeightClass " + weightClass.category + " doesn't have any fights. Check the input Excel file.");
                }         
            }
        }

        #endregion


        #region METHODS FOR WEIGHTCLASSES

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

        #endregion
    }
}
