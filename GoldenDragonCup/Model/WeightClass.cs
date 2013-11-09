using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using GoldenDragonCup;

namespace GoldenDragonCup
{
    class WeightClass
    {
        public int id;
        public float lowerLimit;
        public float upperLimit;
        public bool gender;
        public bool fullContact;
        public bool adult;

        public List<Fighter> weightClassFighters;
        public List<Fight> fights;

        public int roundIndex = 1;

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

            weightClassFighters = new List<Fighter>();
            fights = new List<Fight>();
        }

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
