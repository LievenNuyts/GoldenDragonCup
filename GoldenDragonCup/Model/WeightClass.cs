using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using GoldenDragonCup;
using GoldenDragonCup.View;
using System.Windows;









namespace GoldenDragonCup
{
    public class WeightClass
    {
        public static Random random;
        
        public string category;

        public List<Fighter> weightClassFighters; //list of all fighters in this weightclass
        public List<List<FightView>> rounds; //list of lists of fightViews (the rounds)

        public Tournament tournament;

        public WeightClass(string category, Tournament tournament)
        {
            try
            {
                random = new Random();
                
                this.category = category;
                this.tournament = tournament;
                this.weightClassFighters = new List<Fighter>();
                this.rounds = new List<List<FightView>>();
            }
            catch (Exception exc)
            {
                throw new Exception("Error in constructor WeightClass(string, Tournament) " + exc.Message);
            }
        }


        //methode that select all qualified fighters from the tournament fighter list for this weightclass
        public void selectTournamentFighters()
        {
            try
            {
                foreach (Fighter fighter in this.tournament.allFighters)
                {
                    if (fighter.category == this.category)
                    {
                        this.weightClassFighters.Add(fighter);
                    }
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Error in methode selectTournamentFighters() " + exc.Message);
            }
        }

        //methode that generates Fightview lists and adds fightviews to each list depending on number of fighters
        public void createFightViews()
        {
            try
            {
                //initialise fightview list per round that will be used based on count of fighters in weightclass     
                
                createRounds(1);
                if (weightClassFighters.Count() > 2)
                    {createRounds(2);}
                if (weightClassFighters.Count() > 4)
                    {createRounds(1);}
                if (weightClassFighters.Count() > 8)
                    {createRounds(1);}
                if (weightClassFighters.Count() > 16)
                    {createRounds(1);}
                

                //create fightviews and assign to proper round lists based on amount of fighters in weightclass
                switch (weightClassFighters.Count())
                {
                    case 2:
                        fightViewCreator(1, 0, 0, 0, 0, 0);
                        break;
                    case 3: //round robin
                        fightViewCreator(1, 1, 1, 0, 0, 0);
                        break;
                    case 4:
                        fightViewCreator(2, 1, 1, 0, 0, 0);
                        break;
                    case 5:
                        fightViewCreator(1, 2, 1, 1, 0, 0);
                        break;
                    case 6:
                        fightViewCreator(2, 2, 1, 1, 0, 0);
                        break;
                    case 7:
                        fightViewCreator(3, 2, 1, 1, 0, 0);
                        break;
                    case 8:
                        fightViewCreator(4, 2, 1, 1, 0, 0);
                        break;
                    case 9:
                        fightViewCreator(1, 4, 2, 1, 1, 0);
                        break;
                    case 10:
                        fightViewCreator(2, 4, 2, 1, 1, 0);
                        break;
                    case 11:
                        fightViewCreator(3, 4, 2, 1, 1, 0);
                        break;
                    case 12:
                        fightViewCreator(4, 4, 2, 1, 1, 0);
                        break;
                    case 13:
                        fightViewCreator(5, 4, 2, 1, 1, 0);
                        break;
                    case 14:
                        fightViewCreator(6, 4, 2, 1, 1, 0);
                        break;
                    case 15:
                        fightViewCreator(7, 4, 2, 1, 1, 0);
                        break;
                    case 16:
                        fightViewCreator(8, 4, 2, 1, 1, 0);
                        break;
                    case 17:
                        fightViewCreator(1, 8, 4, 2, 1, 1);
                        break;
                    case 18:
                        fightViewCreator(2, 8, 4, 2, 1, 1);
                        break;
                    case 19:
                        fightViewCreator(3, 8, 4, 2, 1, 1);
                        break;
                    case 20:
                        fightViewCreator(4, 8, 4, 2, 1, 1);
                        break;
                    default:
                        throw new GDCException("Invalid number of fighters in weightclass " + this.category + ": " +
                                                    weightClassFighters.Count().ToString());                     
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method createFightViews() " + exc.Message);
            }
        }

        //method to generate List<FightView> rounds and add them to ArrayList 'rounds'
        private void createRounds(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                List<FightView> round = new List<FightView>();
                rounds.Add(round);
            }
        }

        //method creates fightView and adds it to list provided as parameter. 
        //method also sets the title of round number, final, ...
        public void addFightViewToRound(List<FightView> list)
        {
            try
            {
                int i = rounds.IndexOf(list) + 1;
                
                FightView fightView = new FightView(this);             
                list.Add(fightView);

                //check if the round only has three fighters and thus is a round robin
                if (weightClassFighters.Count == 3)
                {
                    if (i == rounds.Count)
                    {
                        fightView.setHeader("ROBIN3");
                    }
                    else if (i == rounds.Count - 1)
                    {
                        fightView.setHeader("ROBIN2");
                    }
                    else
                    {
                        fightView.setHeader("ROBIN1");
                    }
                }
                else //if it is not round robin
                {
                    if (i == rounds.Count)
                    {
                        fightView.setHeader("FINAL");
                    }
                    else if (i == rounds.Count - 1)
                    {
                        fightView.setHeader("final");
                    }
                    else
                    {
                        fightView.setHeader("ROUND" + i.ToString());
                    }
                }

            }
            catch (Exception exc)
            {
                throw new Exception("Error in method addFightViewToRound(List<FightView> list) " + exc.Message);
            }
        }

        //method creates x fightViews per round based on int value parameters
        public void fightViewCreator(int a, int b, int c, int d, int e, int f)
        {
            try
            {
                for (int i = 0; i < a; i++)
                { addFightViewToRound((List<FightView>)rounds[0]); }

                for (int i = 0; i < b; i++)
                { addFightViewToRound((List<FightView>)rounds[1]); }

                for (int i = 0; i < c; i++)
                { addFightViewToRound((List<FightView>)rounds[2]); }

                for (int i = 0; i < d; i++)
                { addFightViewToRound((List<FightView>)rounds[3]); }

                for (int i = 0; i < e; i++)
                { addFightViewToRound((List<FightView>)rounds[4]); }

                for (int i = 0; i < f; i++)
                { addFightViewToRound((List<FightView>)rounds[5]); }
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method fightViewCreator(params) " + exc.Message);
            }
        }

        /*
        public void assignFightersToFightViews()
        {
            try
            {
                if (weightClassFighters.Count == 3) //round robin
                {
                    FightView fvRound1 = rounds[0][0];
                    fvRound1.setFighter1(weightClassFighters[0]);
                    fvRound1.setFighter2(weightClassFighters[1]);

                    FightView fvRound2 = rounds[1][0];
                    fvRound2.setFighter1(weightClassFighters[0]);
                    fvRound2.setFighter2(weightClassFighters[2]);

                    FightView fvRound3 = rounds[2][0];
                    fvRound3.setFighter1(weightClassFighters[1]);
                    fvRound3.setFighter2(weightClassFighters[2]);
                }
                else //not round robin
                {
                    //assign fighters to the first round
                    foreach (FightView fightView in (List<FightView>)rounds[0])
                    {
                        if (fightView.getFighter1() == null)
                        {
                            fightView.setFighter1(selectFreeFighter(freeFighterList()));
                        }

                        if (fightView.getFighter2() == null)
                        {
                            fightView.setFighter2(selectFreeFighter(freeFighterList()));
                        }
                    }

                    List<Fighter> freeFighters = freeFighterList();

                    //assign fighters to second round if needed
                    if (freeFighters.Count > 0)
                    {
                        foreach (FightView fightView in (List<FightView>)rounds[1])
                        {
                            freeFighters = freeFighterList();

                            if (freeFighters.Count >= 1 && fightView.getFighter1() == null)
                            {
                                fightView.setFighter1(selectFreeFighter(freeFighterList()));
                            }

                            if (freeFighters.Count > 1 && fightView.getFighter2() == null)
                            {
                                fightView.setFighter2(selectFreeFighter(freeFighterList()));
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method testMethodToAssignFightersToFightViews() " + exc.Message);
            }
        }*/

        
        public void assignFightersToFightViews()
        {
            try
            {
                if (weightClassFighters.Count == 3) //round robin
                {
                    FightView fvRound1 = rounds[0][0];
                    fvRound1.setFighter1(weightClassFighters[0]);
                    fvRound1.setFighter2(weightClassFighters[1]);

                    FightView fvRound2 = rounds[1][0];
                    fvRound2.setFighter1(weightClassFighters[0]);
                    fvRound2.setFighter2(weightClassFighters[2]);

                    FightView fvRound3 = rounds[2][0];
                    fvRound3.setFighter1(weightClassFighters[1]);
                    fvRound3.setFighter2(weightClassFighters[2]);
                }
                else //not round robin
                {
                    List<List<Fighter>> list = createPairedList(); //list of paired fighters is created
         
                    //assign fighters to the first round
                    for (int i = 0; i < rounds[0].Count; i++)
                    {
                        FightView fightView = rounds[0][i]; //select first list of round one
                        fightView.setFighter1(list[0][0]); //select list i in the list and assign the first fighter in that list of two
                        fightView.setFighter2(list[0][1]); //select list i in the list and assign the second fighter in that list of two
                        list.Remove(list[0]); //remove the 'used' pair from list
                    }

                    //assign fighters to second round if needed
                    if (list.Count > 0)
                    {
                        int counter = 0;

                        for (int i = 0; i < list.Count; i++, counter++)
                        {
                            FightView fightView = rounds[1][i]; //selecting FightViews from second round of the weightclass
                            fightView.setFighter1(list[i][0]); //assigning first Fighter to FightView    
                            fightView.setFighter2(list[i][1]); //assigning second Fighter to FightView                      
                        }

                        //there might be one free fighter left, which needs to be assigned
                        List<Fighter> remaining = freeFighterList();

                        if (remaining != null && remaining.Count > 0)
                        {
                            FightView fightView = rounds[1][counter];
                            fightView.setFighter1(selectFreeFighter(remaining));
                        }
                    }
                    else 
                    {                                
                        //check if there is still one free fighter that needs to be added to next round
                        if (freeFighterList() != null && freeFighterList().Count > 0) 
                        {
                            FightView fightViewExtra = rounds[1][0];
                            Fighter lastFighter = selectFreeFighter(freeFighterList());
                            fightViewExtra.setFighter1(lastFighter);
                        }
                    }

                    if (freeFighterList() != null && freeFighterList().Count != 0)
                    {
                        throw new GDCException("Error in method assignFightersToFightViews() for weightclass " + this.category + " : there are still " + freeFighterList().Count.ToString() + " unassigned fighters!!");
                    }
                }      
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method assignFightersToFightViews() " + exc.Message);
            }
        }


        #region METHODS TO PAIR FIGHTERS AND SELECT FREE FIGHTERS

        //method that returns a list of unselected fighters of the weightclass
        private List<Fighter> freeFighterList()
        {
            try
            {
                List<Fighter> freeFighters = (from fighterX in weightClassFighters
                                              where fighterX.isSelected == false
                                              select fighterX).ToList<Fighter>();

                return freeFighters;
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method createFreeFighterList(): " + exc.Message);
            }
        }

        //create list of fighters not belonging to club x
        private List<Fighter> freeFighterNonClubList(string club)
        {
            try
            {
                List<Fighter> list = null;
                
                var freeFighters = (from fighterX in weightClassFighters
                                              where fighterX.isSelected == false && fighterX.club != club
                                              select fighterX).ToList<Fighter>();
                if (freeFighters.Count != 0)
                {
                    list = freeFighters.ToList();
                }
                
                return list;
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method freeFighterNonClubList(string club): " + exc.Message);
            }
        }


        //returns random fighter from biggest free fighter club
        private Fighter selectBigClubFreeFighter()
        {
            try
            {
                List<Fighter> list = freeFighterList();
               
                string maxClub = null;
                int maxClubCount = 0;

                for (int i = 0; i < list.Count; i++)
                {
                    int clubCount = 0;
                    string club = list[i].club;

                    foreach (Fighter fighter in list)
                    {
                        if (fighter.club == club)
                        {
                            clubCount++;

                            if (clubCount > maxClubCount)
                            {
                                maxClubCount = clubCount;
                                maxClub = club;
                            }
                        }
                    }
                }

                List<Fighter> freeFighters = (from fighterX in list
                                              where fighterX.club == maxClub
                                              select fighterX).ToList<Fighter>();

                return selectFreeFighter(freeFighters);
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method selectBigClubFreeFighter(): " + exc.Message);
            }
        }

        //returns random fighter from list NOT of club X
        private Fighter selectNONBigClubFreeFighter(string club)
        {
            try
            {
                List<Fighter> freeFighters = (from fighterX in weightClassFighters
                                              where fighterX.isSelected == false && fighterX.club != club
                                              select fighterX).ToList<Fighter>();

                return selectFreeFighter(freeFighters);          
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method selectNONBigClubFreeFighter(string club): " + exc.Message);
            }
        }

        //method that returns a free random fighter from not selected fighters in list fighters
        //sets the selected fighter to isSelected = true to avoid duplicate fight assignment
        private Fighter selectFreeFighter(List<Fighter> list)
        {
            try
            {
                Fighter fighter = list[random.Next(list.Count)];
                fighter.isSelected = true;
                return fighter;
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method selectFreeFighter(List<Fighter> list): " + exc.Message);
            }
        }


        //method that creates list of list<fighter> (always just 2 = paired fighters)
        private List<List<Fighter>> createPairedList()
        {
            try
            {
                List<List<Fighter>> list = new List<List<Fighter>>();

                do
                {
                    List<Fighter> pair = new List<Fighter>();

                    //adding first fighter (of the biggest club)
                    Fighter fighter = selectBigClubFreeFighter();
                    pair.Add(fighter);

                    //adding second fighter (if possible not from the biggest club)
                    if (freeFighterNonClubList(fighter.club) != null) //if there are still fighters left not belonging to the big club
                    {
                        pair.Add(selectNONBigClubFreeFighter(fighter.club));
                    }
                    else //if only fighters left are from the big club
                    {
                        pair.Add(selectBigClubFreeFighter());
                    }

                    list.Add(pair);
                }
                while (freeFighterList().Count > 1); //keep on pairing till we drop below 2 remaining free fighters

                //if one fighter remains, this is taken care of in the method assignFightersToFightViews()
    
                //randomize the list
                var randomizedList = list.OrderBy(a => Guid.NewGuid());
                List<List<Fighter>> newList = randomizedList.ToList();

                //testMethodPairList(newList);

                return newList;
            }
            catch (Exception exc)
            {
                throw new Exception("Error in method createPairedList(): " + exc.Message);
            }
        }

        private void testMethodPairList(List<List<Fighter>> list)
        {
            string message = category + "\n\n";

            foreach (List<Fighter> subList in list)
            {
                message += subList[0].lastName + " " + subList[0].firstName + " vs. " + 
                            subList[1].lastName + " " + subList[1].firstName + "\n";
            }

            MessageBox.Show(message);
        }

        #endregion
    }
}
