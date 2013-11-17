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
    public class FightView : GroupBox
    {
        public int fighter1; //fighterID
        public int fighter2; //fighterID
        public int winner; //fighterID
        public bool inProgress; //Is the match currently in progress?
        public int roundIndex; //not 2 or 3 rounds per match, but rounds in the weightclass (ex: 1st, 2nd, semi final, final)
        public string fightName; // = Weightclass + gender + fullcontact + round
           
        public Button btn_fighter1, btn_fighter2;
        public Grid grid;
   
        public FightView(){}

        //constructor based on two fighter ID's and the weightclass' current roundIndex
        public FightView(int fighter1, int fighter2, int roundIndex)
        {
            this.fighter1 = fighter1;
            this.fighter2 = fighter2;
            this.roundIndex = roundIndex;

            initialise();
        }


        //method to initialise components and define layout
        public void initialise()
        {
            //dimensions of the group box
            this.Width = 150;
            this.Height = 150;

            //initialise buttons and grid
            btn_fighter1 = new Button();
            btn_fighter1.Foreground = Brushes.Red;
            btn_fighter2 = new Button();

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
        }


        //om extern de positie van de groupbox in te stellen
        public void setPosition(int x, int y)
        {
            //
        }


        //events voor de buttons
        private void btn_fighter1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("jeuuuuj1");
            winner = fighter1;
        }

        private void btn_fighter2_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("jeuuuuj2");
            winner = fighter2;
        }
    }
}
