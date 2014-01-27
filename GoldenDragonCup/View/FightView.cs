using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoldenDragonCup.View
{
    // !! ALLE ELEMENTEN NOG PLAATSEN !!
    
    public class FightView : GroupBox //is a type of GroupBox
    {      
        public WeightClass weightClass;
        
        //public int fightCounter; // int to count the fights in a weightclass, also used for header of groupBox.

        private Fighter fighter1;
        private Fighter fighter2; 
        public Fighter winner;

        public bool inProgress; //true if this fight is currently in progress
         
        public Button btn_fighter1, btn_fighter2, btn_confirm;
        public Label lbl_winner;
        public TextBox txtb_winner;
        public Grid grid;
   
        //constructor based on weightclass
        public FightView(WeightClass weightClass)
        {
            try
            {
                this.weightClass = weightClass;
                initialise();
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in constructor FightView(WeightClass weightClass) " + exc.Message);
            }
        }

        //constructor based on two fighter ID's, weightclass' current roundIndex, weightClass, tournament and fightCounter
        public FightView(Fighter fighter1, Fighter fighter2, WeightClass weightClass)
        {
            try
            {
                this.fighter1 = fighter1;
                this.fighter2 = fighter2;            
                this.weightClass = weightClass;                 
                initialise();
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in constructor FightView(Fighter, Fighter, WeightClass) " + exc.Message);
            }
        }


        //method to initialise components and define layout
        public void initialise()
        {

            try
            {
                //dimensions of the group box
                this.Foreground = Brushes.Gold;
                this.Width = 200;
                this.Height = 135;

                //initialise label
                lbl_winner = new Label();
                lbl_winner.Content = "          Winner:";
                lbl_winner.FontWeight = FontWeights.Bold;
                lbl_winner.Width = 90;
                lbl_winner.Height = 25;

                //initialise textbox
                txtb_winner = new TextBox();
                txtb_winner.Width = 90;
                txtb_winner.Height = 30;
                txtb_winner.FontWeight = FontWeights.Bold;

                //initialise buttons
                btn_fighter1 = new Button();
                btn_fighter1.Width = 90;
                btn_fighter1.Height = 35;
                btn_fighter1.Foreground = Brushes.Red;
                btn_fighter1.FontWeight = FontWeights.Bold;
                //btn_fighter1.Content = fighter1.lastName + " " + fighter1.firstName[0];     
                btn_fighter1.Content = "  ***  ";

                btn_fighter2 = new Button();
                btn_fighter2.Width = 90;
                btn_fighter2.Height = 35;
                btn_fighter2.FontWeight = FontWeights.Bold;
                //btn_fighter2.Content = fighter2.lastName + " " + fighter2.firstName[0];
                btn_fighter2.Content = "  ***  ";

                btn_confirm = new Button();
                btn_confirm.Width = 25;
                btn_confirm.Height = 25;
                btn_confirm.Content = "OK";


                //add click event to buttons linked to click method
                btn_fighter1.Click += this.btn_fighter1_Click; //check if this is correct
                btn_fighter2.Click += this.btn_fighter2_Click;
                btn_confirm.Click += this.btn_confirm_Click;


                //initialise grid
                grid = new Grid();
                grid.Width = 190;
                grid.Height = 115;

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(95.0) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(95.0) });


                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(5.0) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35.0) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35.0) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35.0) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(5.0) });

                //add buttons and labels to grid
                grid.Children.Add(btn_fighter1);
                Grid.SetColumn(btn_fighter1, 0);
                Grid.SetRow(btn_fighter1, 1);

                grid.Children.Add(btn_fighter2);
                Grid.SetColumn(btn_fighter2, 0);
                Grid.SetRow(btn_fighter2, 3);

                grid.Children.Add(lbl_winner);
                Grid.SetColumn(lbl_winner, 0);
                Grid.SetRow(lbl_winner, 2);

                grid.Children.Add(txtb_winner);
                Grid.SetColumn(txtb_winner, 1);
                Grid.SetRow(txtb_winner, 2);

                grid.Children.Add(btn_confirm);
                Grid.SetColumn(btn_confirm, 1);
                Grid.SetRow(btn_confirm, 3);

                //add grid to groupbox
                this.AddChild(grid);
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method initialise() " + exc.Message);
            }
        }

        
        //events for buttons
        private void btn_fighter1_Click(object sender, RoutedEventArgs e)
        {
            winner = fighter1;
            txtb_winner.Text = btn_fighter1.Content.ToString();
            txtb_winner.Foreground = btn_fighter1.Foreground;
        }

        private void btn_fighter2_Click(object sender, RoutedEventArgs e)
        {
            winner = fighter2;
            txtb_winner.Text = btn_fighter2.Content.ToString();
            txtb_winner.Foreground = btn_fighter2.Foreground;
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            if (winner != null)
            {
                FighterToNextRound(winner);
            }
        }

        //method that transfers winner to next round
        private void FighterToNextRound(Fighter fighter)
        {
            try
            {

                int currentRoundIndex = 0;

                foreach (List<FightView> list in weightClass.rounds)
                {
                    foreach (FightView fightView in list)
                    {
                        if (fightView.Equals(this))
                        {
                            currentRoundIndex = weightClass.rounds.IndexOf(list);
                        }
                    }
                }

                //check if next rounds are small final and final
                if (weightClass.rounds.Count - (currentRoundIndex + 1) < 3)
                {
                    //code for final fights
                    if (fighter1 == winner)
                    {
                        finalFightsHelper(currentRoundIndex, fighter2);
                    }
                    else //fighter2 == winner
                    {
                        finalFightsHelper(currentRoundIndex, fighter1);
                    }
                }
                else //when not 'small final' or final
                {
                    List<FightView> round = weightClass.rounds[currentRoundIndex + 1];

                    foreach (FightView fightView in round)
                    {
                        if (fightView.getFighter1() == null)
                        {
                            fightView.setFighter1(fighter);
                            break;
                        }
                        else if (fightView.getFighter2() == null)
                        {
                            fightView.setFighter2(fighter);
                            break;
                        }
                        else
                        {
                            //nothing happens, goes to next fightView in the list
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method FighterToNextRound(Fighter fighter): " + exc.Message);
            }
        }

        private void finalFightsHelper(int currentRoundIndex, Fighter loser)
        {
            try
            {

                //set winner to 'final'
                if (weightClass.rounds[currentRoundIndex + 2][0].getFighter1() == null)
                {
                    weightClass.rounds[currentRoundIndex + 2][0].setFighter1(winner);
                }
                else
                {
                    weightClass.rounds[currentRoundIndex + 2][0].setFighter2(winner);
                }

                //set loser to 'small final'
                if (weightClass.rounds[currentRoundIndex + 1][0].getFighter1() == null)
                {
                    weightClass.rounds[currentRoundIndex + 1][0].setFighter1(loser);
                }
                else
                {
                    weightClass.rounds[currentRoundIndex + 1][0].setFighter2(loser);
                }
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method finalFightsHelper(param): " + exc.Message);
            }
        }


        //method to make a string summary of the fight for the fightoverview pane
        public override string ToString()
        {
            try
            {
                string f1;
                string f2;
            
                if (fighter1 == null)
                {
                    f1 = "   *****   ";
                }
                else
                {
                    f1 = fighter1.lastName + " " + fighter1.firstName[0];
                }

                if (fighter2 == null)
                {
                    f2 = "   *****   ";
                }
                else
                {
                    f2 = fighter2.lastName + " " + fighter2.firstName[0];
                }
             
                if (this.Header == null)
                {
                    this.Header = "";
                }

                return weightClass.category + "  [" + this.Header.ToString() + "] :  " +
                        f1.ToUpper() + "  vs. " + f2.ToUpper();
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method override string ToString(): " + exc.Message);
            }
        }

        //setter methodes
        public void setFighter1(Fighter fighter)
        {
            try
            {
                this.fighter1 = fighter;
                btn_fighter1.Content = fighter1.lastName + " " + fighter1.firstName[0];
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void setFighter2(Fighter fighter)
        {
            try
            {
                this.fighter2 = fighter;
                btn_fighter2.Content = fighter2.lastName + " " + fighter2.firstName[0];
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public Fighter getFighter1()
        {
            return this.fighter1;
        }

        public Fighter getFighter2()
        {
            return this.fighter2;
        }

        public void setHeader(string header)
        {
            this.Header = header;
        }
    }
}
