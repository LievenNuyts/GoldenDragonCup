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
using GoldenDragonCup.Tools;


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
            try
            {
                if (nextFight == null && getReady == null)
                {
                    this.Close();
                }
                
                this.nextFight = nextFight;
                this.getReady = getReady;

                InitializeComponent();

                //set custom color to labels
                defineColor();

                //set text to labels
                lbl_nextFightInfo.Content = "Fight nr. " + nextFight.ID + ": " + nextFight.weightClass.category +
                                                 headerConverter(nextFight.Header.ToString());

                lbl_getReadyFightInfo.Content = "Fight nr. " + getReady.ID + ": " + getReady.weightClass.category +
                                                   headerConverter(getReady.Header.ToString());

                displayInfo();
            }
            catch(Exception exc)
            {
                throw new Exception("Error in constructor NextFightPopup: " + exc.Message);
            }
        }


        private void defineColor()
        {
            Color color = Color.FromRgb(228, 34, 23);
            SolidColorBrush lavaRed = new SolidColorBrush(color);

            //set color lava red to labels 
            this.lbl_nextFight.Foreground = lavaRed;
            this.lbl_nextFightInfo.Foreground = lavaRed;
            this.lbl_getReadyFightInfo.Foreground = lavaRed;
            this.lbl_getReady.Foreground = lavaRed;
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
            string fighterString = NameHelper.adjust(fighter.lastName) + "\n  " + NameHelper.adjust(fighter.firstName);

            return fighterString;
        }

        //method to convert final or FINAL to different text
        private string headerConverter(string text)
        {
            string convertedText = "";

            if(text == "final")
            {
                convertedText = " * BATTLE FOR BRONZE";
            }
            else if(text == "FINAL")
            {
                convertedText = " * BATTLE FOR GOLD";
            }
            else
            {
                //nothing happens (only special info for small and big final)
            }

            return convertedText;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
