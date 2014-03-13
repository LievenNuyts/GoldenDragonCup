using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public static class idManager
    {
        
        private static int clubIdCounter;
        private static int fighterIdCounter;
        private static int weightclassIdCounter;
        private static int fightViewIdCounter; 

        static idManager()
        {
            clubIdCounter = 0;
            fighterIdCounter = 0;
            weightclassIdCounter = 0;
            fightViewIdCounter = 0;
        }

        public static int getNewClubId() 
        {
            clubIdCounter++;
            return clubIdCounter;
        }

        public static int getNewFighterId()
        {
            fighterIdCounter++;
            return fighterIdCounter;
        }

        public static int getNewWeightclassId()
        {
            weightclassIdCounter++;
            return weightclassIdCounter;
        }

        public static int getNewFightViewId()
        {
            fightViewIdCounter++;
            return fightViewIdCounter;
        }
    }
}
