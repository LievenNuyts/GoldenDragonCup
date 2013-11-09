using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    class Fighter
    {
        public int id;
        public int clubId;
        public int weightClassId;
        public int age;

        public string name;
        public float weight;
        public string gender; // M or V
        public bool fullContact; // true = Sanda, false = Shinda
        public bool adult;

        public int tournamentRanking;
        public int freeMatchCounter;
        public bool isActive = true;
        public bool isSelected;

        public Fighter() { }

        public Fighter(int weightClassId, int age, string name, float weight, string gender, bool fullContact)
        {
            this.initializeFighter(weightClassId, age, name, weight, gender, fullContact);
        }

        public Fighter(int clubId, int weightClassId, int age, string name, float weight, string gender, bool fullContact)
        {
            this.clubId = clubId;
            this.initializeFighter(weightClassId, age, name, weight, gender, fullContact);
        }

        private void initializeFighter(int weightClassId, int age, string name, float weight, string gender, bool fullContact)
        {
            this.weightClassId = weightClassId;
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;
            this.fullContact = fullContact;

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
