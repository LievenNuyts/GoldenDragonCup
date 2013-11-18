using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public class Fighter
    {
        public int id;
        public int clubId; 
        public int age;

        public string name;
        public float weight;
        public bool gender; // true = man, false = woman
        public bool fullContact; // true = Sanda, false = Shinda
        public bool adult; //true if 18 or older

        public int tournamentRanking; //defines the ranking in the weightclass at the end of the tournament (1st, 2nd, 3rd, ...)
        public int freeMatchCounter; //value of how many free matches the fighter has had this tournament
        public bool isActive = true; //true if the fighter is still in competition. Fighters who have lost will be put on 'false'
        public bool isSelected; //true if the fighter is selected for a fight in the actual round of the competition. 
                                //This is only used when the entire fight round is calculated

        public Fighter() { }

        public Fighter(int age, string name, float weight, bool gender, bool fullContact)
        {
            this.initializeFighter(age, name, weight, gender, fullContact);
        }

        public Fighter(int clubId, int age, string name, float weight, bool gender, bool fullContact)
        {
            this.clubId = clubId;
            this.initializeFighter(age, name, weight, gender, fullContact);
        }

        //method to load constructor parameters into class variables - used in class constructors
        private void initializeFighter(int age, string name, float weight, bool gender, bool fullContact)
        {
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;
            this.fullContact = fullContact;
            this.id = idManager.getNewFighterId();

            if (age >= 18)
            {
                this.adult = true;
            }
            else
            {
                this.adult = false;
            }
        }
    }
}
