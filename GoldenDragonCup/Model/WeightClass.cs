using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using GoldenDragonCup;
using GoldenDragonCup.View;

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
                throw new GDCException("Error in constructor WeightClass(string, Tournament) " + exc.Message);
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
                throw new GDCException("Error in methode selectTournamentFighters() " + exc.Message);
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
                throw new GDCException("Error in method createFightViews() " + exc.Message);
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

                if(i == rounds.Count)
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
            catch (Exception exc)
            {
                throw new GDCException("Error in method addFightViewToRound(List<FightView> list) " + exc.Message);
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
                throw new GDCException("Error in method fightViewCreator(params) " + exc.Message);
            }
        }


        public void assignFightersToFightViews()
        {
            try
            {
                if (roundRobinChecker()) //round robin
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
                            fightView.setFighter1(selectFreeFighter());
                        }

                        if (fightView.getFighter2() == null)
                        {
                            fightView.setFighter2(selectFreeFighter());
                        }
                    }

                    List<Fighter> freeFighters = createFreeFighterList();

                    //assign fighters to second round if needed
                    if (freeFighters.Count > 0)
                    {
                        foreach (FightView fightView in (List<FightView>)rounds[1])
                        {
                            freeFighters = createFreeFighterList();

                            if (freeFighters.Count >= 1 && fightView.getFighter1() == null)
                            {
                                fightView.setFighter1(selectFreeFighter());
                            }

                            if (freeFighters.Count > 1 && fightView.getFighter2() == null)
                            {
                                fightView.setFighter2(selectFreeFighter());
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method testMethodToAssignFightersToFightViews() " + exc.Message);
            }
        }

        //method that returns a free random fighter from not selected fighters in the weightClass
        private Fighter selectFreeFighter()
        {
            try
            {
                List<Fighter> freeFighters = createFreeFighterList();
                Fighter fighter = freeFighters[random.Next(freeFighters.Count)];
                fighter.isSelected = true;
                return fighter;
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method selectFreeFighter(): " + exc.Message);
            }
        }

        //method that returns a list of unselected fighters of the weightclass
        private List<Fighter> createFreeFighterList()
        {
            try
            {
                List<Fighter> freeFighters = (from fighterX in weightClassFighters
                                              where fighterX.isActive == true && fighterX.isSelected == false
                                              select fighterX).ToList<Fighter>();

                return freeFighters;
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method createFreeFighterList(): " + exc.Message);
            }
        }

        public bool roundRobinChecker()
        {
            int i = 0;
            
            foreach(List<FightView> list in rounds)
            {
                i += list.Count;
            }

            if (rounds.Count == 3 && i == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
