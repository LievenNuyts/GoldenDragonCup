using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using GoldenDragonCup;

namespace GoldenDragonCup
{
    public class WeightClass
    {
        public int id;
        public float lowerLimit; //lower border weight of the weightclass
        public float upperLimit; //higher border weight of the weightclass
        public bool gender;
        public bool fullContact; //true if this is a fullcontact (sanda) weightclass
        public bool adult; //true if this is a category for 18+

        public List<Fighter> weightClassFighters; //list of all fighters in this weightclass
        public List<Fight> fights; //list of fights (=matched fighters). Will be expanded after each finished round

        public int roundIndex = 1; // first round = X matches. When these x matches are finished the index will become 2 and so on

        public Tournament tournament;


        public WeightClass() { }

        public WeightClass(float lowerLimit, float upperLimit, bool fullContact, bool gender, bool adult, Tournament tournament)
        {
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
            this.fullContact = fullContact;
            this.gender = gender;
            this.adult = adult;
            this.tournament = tournament;
            this.id = idManager.getNewWeightclassId();

            weightClassFighters = new List<Fighter>();
            fights = new List<Fight>();
        }

        //methode that select all qualified fighters from the tournament fighter list for this weightclass
        //based on weight, fullcontact, gender and adult
        public void selectTournamentFighters()
        {
            foreach (Fighter fighter in this.tournament.allFighters)
            {
                if(fighter.weight > this.lowerLimit && fighter.weight < this.upperLimit && 
                    fighter.fullContact == this.fullContact && fighter.gender == this.gender && fighter.adult == this.adult)
                {
                    this.weightClassFighters.Add(fighter);
                }
            }
        }

        //method that will calculate all the fights for the current round - UNDER CONSTRUCTION
        public void calculateRound()
        {
            if (weightClassFighters != null)
            {
                //random fighters matchen in fights die nog niet geselecteerd zijn
                //aantal passieve matchen, en actieve vechter
                if (roundIndex == 1)
                {
                    //rekening houden met club in eerste ronde

                    var fighterList = (from fighter in weightClassFighters
                               where fighter.isActive == true && fighter.isSelected == false                   
                               select fighter);

                    if (fighterList.Count()%2 != 0) //if list has uneven count 1 fighter is excluded from this round
                    {
                        int count = fighterList.Count();
                        int index = new Random().Next(count);

                        Fighter skipToNextRound = fighterList.Skip(index).FirstOrDefault();
                        skipToNextRound.freeMatchCounter++;
                        skipToNextRound.isSelected = true;


                    }



                }
                else
                { 
                }
            }
            else
            {
                throw new GDCException("No fighters assigned to weight class.");
            }
        }

        public List<Fighter> queryForFighterList()
        {
            var fighterList = (from fighter in weightClassFighters
                               where fighter.isActive == true && fighter.isSelected == false
                               select fighter);

            return (List<Fighter>)fighterList;
        }
    }
}
