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
        static Random random;
        
        public int id;
        public string category;

        public List<Fighter> weightClassFighters; //list of all fighters in this weightclass
        public List<List<FightView>> rounds;

        /*
        public List<FightView> round1; //list of fights (=matched fighters). Will be expanded after each finished round
        public List<FightView> round2;
        public List<FightView> round3;
        public List<FightView> round4;
        public List<FightView> round5;
        public List<FightView> round6;*/

        public Tournament tournament;

        public WeightClass(string category, Tournament tournament)
        {
            try
            {
                random = new Random();
                
                this.category = category;
                this.tournament = tournament;
                this.id = idManager.getNewWeightclassId();

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

        public void createFightViews()
        {
            try
            {
                //initialise fightview list per round that will be used based on count of fighters in weightclass

                //round1 = new List<FightView>();
                createRounds(1);

                if (weightClassFighters.Count() > 2)
                {
                    createRounds(2);
                }
                if (weightClassFighters.Count() > 4)
                {
                    createRounds(1);
                }
                if (weightClassFighters.Count() > 8)
                {
                    createRounds(1);
                }
                if (weightClassFighters.Count() > 16)
                {
                    createRounds(1);
                }

                //create fightviews and assign to proper round lists based on amount of fighters in weightclass
                switch (weightClassFighters.Count())
                {
                    case 2:
                        fightViewCreator(1, 0, 0, 0, 0, 0);
                        break;

                    case 3:
                        fightViewCreator(1, 1, 1, 0, 0, 0);
                        break;

                    case 4:
                        fightViewCreator(2, 1, 1, 0, 0, 0);
                        break;

                    case 5:
                        fightViewCreator(2, 1, 1, 1, 0, 0);
                        break;

                    case 6:
                        fightViewCreator(3, 1, 1, 1, 0, 0);
                        break;

                    case 7:
                        fightViewCreator(3, 2, 1, 1, 0, 0);
                        break;

                    case 8:
                        fightViewCreator(4, 2, 1, 1, 0, 0);
                        break;

                    case 9:
                        fightViewCreator(4, 2, 1, 1, 1, 0);
                        break;

                    case 10:
                        fightViewCreator(5, 2, 1, 1, 1, 0);
                        break;

                    case 11:
                        fightViewCreator(5, 3, 1, 1, 1, 0);
                        break;

                    case 12:
                        fightViewCreator(6, 3, 1, 1, 1, 0);
                        break;

                    case 13:
                        fightViewCreator(6, 3, 2, 1, 1, 0);
                        break;

                    case 14:
                        fightViewCreator(7, 3, 2, 1, 1, 0);
                        break;

                    case 15:
                        fightViewCreator(7, 4, 2, 1, 1, 0);
                        break;

                    case 16:
                        fightViewCreator(8, 4, 2, 1, 1, 0);
                        break;

                    case 17:
                        fightViewCreator(8, 4, 2, 1, 1, 1);
                        break;

                    case 18:
                        fightViewCreator(9, 4, 2, 1, 1, 1);
                        break;

                    case 19:
                        fightViewCreator(9, 5, 2, 1, 1, 1);
                        break;

                    case 20:
                        fightViewCreator(10, 5, 2, 1, 1, 1);
                        break;

                    default:
                        throw new GDCException("Invalid number of fighters in weightclass " + this.category + ": " +
                                                    weightClassFighters.Count().ToString());
                        break;
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

        //method creates fightView and ads it to list provided as parameter
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


        public void testMethodToAssignFightersToFightViews()
        {
            try
            {
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

                if (freeFighters.Count == 1)
                {
                    List<FightView> list = (List<FightView>)rounds[1];
                    FightView fightView = list[0];
                    fightView.setFighter1(freeFighters[0]);
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
            List<Fighter> freeFighters = createFreeFighterList();
            Fighter fighter = freeFighters[random.Next(freeFighters.Count)];
            fighter.isSelected = true;
            return fighter;
        }

        //method that returns a list of unselected fighters of the weightclass
        private List<Fighter> createFreeFighterList()
        {
            List<Fighter> freeFighters = (from fighterX in weightClassFighters
                                          where fighterX.isActive == true && fighterX.isSelected == false
                                          select fighterX).ToList<Fighter>();

            return freeFighters;
        }
     
    }
}
