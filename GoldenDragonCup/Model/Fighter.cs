using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public class Fighter
    {
        public int id;
        public string firstName;
        public string lastName;
        public string club;
        public string clubLocation;
        public string category;

        public int tournamentRanking; //defines the ranking in the weightclass at the end of the tournament (1st, 2nd, 3rd, ...)
        public int freeMatchCounter; //value of how many free matches the fighter has had this tournament
        public bool isActive = true; //true if the fighter is still in competition. Fighters who have lost will be put on 'false'
        public bool isSelected = false; //true if the fighter is selected for a fight in the actual round of the competition. 
                                //This is only used when the entire fight round is calculated

        public Fighter() { }

        public Fighter(string firstName, string lastName, string club, string clubLocation, string category)
        {
            this.id = idManager.getNewFighterId();
            this.firstName = firstName;
            this.lastName = lastName;
            this.club = club;
            this.clubLocation = clubLocation;
            this.category = category;
        }
    }
}
