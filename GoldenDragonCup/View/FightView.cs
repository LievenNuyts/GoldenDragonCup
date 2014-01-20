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
        //update to test github
        
        public Tournament tournament;
        public WeightClass weightClass;
        
        public int fightCounter; // int to count the fights in a weightclass, also used for header of groupBox.

        public int fighter1ID;
        public int fighter2ID; 
        public int winnerID;
        public bool inProgress; //true if this fight is currently in progress
        public int roundIndex; //not 2 or 3 rounds per match, but rounds in the weightclass (ex: 1st, 2nd, semi final, final)
        public string fightName; // = Weightclass + gender + fullcontact + round
           
        public Button btn_fighter1, btn_fighter2;
        public Label lbl_text, lbl_winner;
        public Grid grid;
   
        public FightView(){}

        //constructor based on two fighter ID's, weightclass' current roundIndex, weightClass, tournament and fightCounter
        public FightView(int fighter1ID, int fighter2ID, int roundIndex, WeightClass weightClass, Tournament tournament, int fightCounter)
        {
            this.fighter1ID = fighter1ID;
            this.fighter2ID = fighter2ID;
            this.roundIndex = roundIndex;
            this.weightClass = weightClass;
            this.tournament = tournament;
            this.fightCounter = fightCounter;
            
            initialise();
        }


        //method to initialise components and define layout
        public void initialise()
        {
            //dimensions of the group box
            this.Width = 150;
            this.Height = 150;

            //initialise labels
            lbl_text = new Label();
            lbl_text.Content = "Winner:";
            lbl_winner = new Label();

            //initialise buttons and grid
            btn_fighter1 = new Button();
            nameToButton(fighter1ID, btn_fighter1);
            btn_fighter1.Foreground = Brushes.Red;
            btn_fighter2 = new Button();
            nameToButton(fighter2ID, btn_fighter2);

            //add click event to buttons linked to click method
            btn_fighter1.Click += this.btn_fighter1_Click; //check if this is correct
            btn_fighter2.Click += this.btn_fighter2_Click;

            //initialise grid
            grid = new Grid();
            grid.Width = 150;
            grid.Height = 150;

            //add buttons to grid
            grid.Children.Add(btn_fighter1);
            grid.Children.Add(btn_fighter2);

            //add grid to groupbox
            this.Content = grid;

            //add name to the GroupBox
            this.Header = "Fight " + fightCounter.ToString();
        }

        //method to lookup the fighter ID in tournament fighter list and add that name to the button
        public void nameToButton(int fighterID, Button button)
        {
            foreach (Fighter x in tournament.allFighters)
            {
                if (x.id == fighterID)
                {
                    button.Content = x.name;
                }
            }
        }

        //external way to set the position of groupbox on mainwindow tabcontrol
        public void setPosition(int x, int y)
        {
            //
        }

        //events for buttons
        private void btn_fighter1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("jeuuuuj1");
            winnerID = fighter1ID;
            lbl_winner.Content = btn_fighter1.Content;
        }

        private void btn_fighter2_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("jeuuuuj2");
            winnerID = fighter2ID;
            lbl_winner.Content = btn_fighter2.Content;
        }

        //method to make a string summary of the fight for the fightoverview pane
        public override string ToString()
        {
            string manWoman;
            string fullContact;
            string winner;

            if(weightClass.gender == true)
            {
                manWoman = "M";
            }
            else
            {
                manWoman = "W";
            }

            if(weightClass.fullContact == true)
            {
                fullContact = "Full";
            }
            else
            {
                fullContact = "Semi";
            }
            if (lbl_winner.Content != "")
            {
                winner = " *** ";
            }
            else
            {
                winner = lbl_winner.Content.ToString();
            }

            return manWoman + " " + fullContact + " " + weightClass.lowerLimit + "+ :" + 
                    btn_fighter1.Content + " - " + btn_fighter2.Content + ": Winner = " + winner;
        }
    }
}
