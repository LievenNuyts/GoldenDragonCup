using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    class Club
    {
        public int id;
        public string name;
        public string trainer;
        public string address;

        public Club(){}

        public Club(string name, string trainer, string address)
        {
            this.name = name;
            this.trainer = trainer;
            this.address = address;
            this.id = idManager.getNewClubId();
        }
    }
}
