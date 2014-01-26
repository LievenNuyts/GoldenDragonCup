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
                this.Height = 120;

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


                //add name to the GroupBox
                //this.Header = "Fight " + fightCounter.ToString();
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
            // write code
        }

        //method to make a string summary of the fight for the fightoverview pane
        public override string ToString()
        {
            return "";
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
    }
}
