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
using System.Windows.Shapes;


namespace GoldenDragonCup.View
{
    /// <summary>
    /// Interaction logic for NextFightPopup.xaml
    /// </summary>
    public partial class NextFightPopup : Window
    {
        private FightView nextFight;
        private FightView getReady;

        public NextFightPopup(FightView nextFight, FightView getReady)
        {

            if (nextFight == null && getReady == null)
            {
                this.Close();
            }
            
            this.nextFight = nextFight;
            this.getReady = getReady;
                   
            InitializeComponent();

            lbl_nextFightInfo.Content = nextFight.weightClass.category + 
                                        "  *  " + headerConverter(nextFight.Header.ToString());

            lbl_getReadyFightInfo.Content = getReady.weightClass.category +
                                        "  *  " + headerConverter(getReady.Header.ToString());

            displayInfo();
        }

        private void displayInfo()
        {
            nextFightViewToFighterInfo(nextFight);
            prepFightViewToFighterInfo(getReady); 
        }

        private void nextFightViewToFighterInfo(FightView fightView)
        {
            if (fightView != null)
            {
                Fighter fighter1 = fightView.getFighter1();
                Fighter fighter2 = fightView.getFighter2();

                lbl_nextFighter1.Content = fighterToString(fighter1);
                lbl_nextFighter2.Content = fighterToString(fighter2);
            }        
        }

        private void prepFightViewToFighterInfo(FightView fightView)
        {
            if (fightView != null)
            {
                Fighter fighter1 = fightView.getFighter1();
                Fighter fighter2 = fightView.getFighter2();

                lbl_prepFighter1.Content = fighterToString(fighter1);
                lbl_prepFighter2.Content = fighterToString(fighter2);
            }
            else
            {
                lbl_getReady.Visibility = Visibility.Hidden;
                lbl_getReadyFightInfo.Visibility = Visibility.Hidden;
                lbl_prepFighter1.Visibility = Visibility.Hidden;
                lbl_prepFighter2.Visibility = Visibility.Hidden;
                lbl_vs2.Visibility = Visibility.Hidden;
                img_red2.Visibility = Visibility.Hidden;
                img_black2.Visibility = Visibility.Hidden;
            }
        }

        private string fighterToString(Fighter fighter)
        {
            string fighterString = fighter.lastName + "\n  " + fighter.firstName;

            return fighterString;
        }

        //method to convert final or FINAL to different text
        private string headerConverter(string text)
        {
            string convertedText = "";

            if(text == "final")
            {
                convertedText = "BATTLE FOR THE BRONZE";
            }
            else if(text == "FINAL")
            {
                convertedText = "BATTLE FOR THE GOLD";
            }
            else
            {
                convertedText = text;
            }

            return convertedText;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
