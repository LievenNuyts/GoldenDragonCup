using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public class Club
    {
        public int id;
        public string name; //name of the club
        public string trainer; //name of the trainer or responsible
        public string address; //address of the club

        public Club(){}


        //constructor based on clubname, trainer/responsible and address
        public Club(string name, string trainer, string address)
        {
            this.name = name;
            this.trainer = trainer;
            this.address = address;
            this.id = idManager.getNewClubId();
        }
    }
}
