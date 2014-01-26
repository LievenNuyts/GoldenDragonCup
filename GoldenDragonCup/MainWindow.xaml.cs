using System;
using System.Collections.Generic;
using System.Collections;
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
using System.IO;
using System.Data;
using System.Data.OleDb;
using GoldenDragonCup.View;

namespace GoldenDragonCup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Tournament tournament;
        public ArrayList allFighters;
        //static Random random;
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                //random = new Random();

                allFighters = new ArrayList();
                //readFightersFromFile();

                //tournament = new Tournament("Golden Dragon Cup 2014", allFighters);
                //createTabsforWeightClasses(tournament.weightClassCodes);
                //createTabsforWeightClasses(createTestArray());

                tournament = new Tournament("Golden Dragon Cup 2014", createTestFighters());

                createTabsforWeightClasses();

                createTrees();

                //testFightViews();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }          
        }

        private void testFightViews()
        {

            foreach (WeightClass weightClass in tournament.weightClasses)
            {

                Fighter one = weightClass.weightClassFighters[0];
                Fighter two = weightClass.weightClassFighters[1];

                FightView fightView = new FightView(one, two, weightClass);

                foreach (TabItem tab in tabControl.Items)
                {
                    if(tab.Header.ToString() == weightClass.category)
                    {               
                        Grid toAddFightViewsTo = (Grid)tab.Content;

                        toAddFightViewsTo.Children.Add(fightView);
                        Grid.SetColumn(fightView, 1);
                        Grid.SetRow(fightView, 1);
                    }
                }
            }
        }

        private ArrayList createTestArray()
        {
            ArrayList testList = new ArrayList();
            testList.Add("FC M -65");
            testList.Add("FC M -75");
            testList.Add("FC M -85");
            testList.Add("FC M +85");
            testList.Add("FC V -65");
            testList.Add("FC M +65");

            return testList;
        }

        private ArrayList createTestFighters()
        {
            ArrayList fighters = new ArrayList();

            Fighter fighter1 = new Fighter("Jan", "Jansens", "Long Hu Men", "Leuven, Belgium", "FC M +65");
            fighters.Add(fighter1);

            Fighter fighter2 = new Fighter("Pol", "Vereycken", "Golden Phoenix", "Brussel, Belgium", "FC M +65");
            fighters.Add(fighter2);

            Fighter fighter3 = new Fighter("Jeroen", "Vanhoeck", "Long Hu Men", "Vilvoorde, Belgium", "FC M +65");
            fighters.Add(fighter3);

            Fighter fighter4 = new Fighter("Jaak", "Clymans", "Golden Phoenix", "Brussel, Belgium", "FC M +65");
            fighters.Add(fighter4);
  
            Fighter fighter5 = new Fighter("Raf", "Evens", "Long Hu Men", "Leuven, Belgium", "FC M +65");
            fighters.Add(fighter5);

            Fighter fighter6 = new Fighter("Pieter", "Vermeulen", "Golden Phoenix", "Brussel, Belgium", "FC M +65");
            fighters.Add(fighter6);

            Fighter fighter7 = new Fighter("Joris", "Peters", "Long Hu Men", "Leuven, Belgium", "FC M +65");
            fighters.Add(fighter7);

            return fighters;
        }

        private void createTabsforWeightClasses()
        {
            foreach (WeightClass weightClass in tournament.weightClasses)
            {
                TabItem tab = new TabItem();
                tab.Header = weightClass.category;

                Grid grid = new Grid();
                grid.Background = Brushes.Crimson;
                grid.Height = 1600;

                //add columns to grid              
                for (int i = 0; i < 6; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20.0) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(220.0) });
                }            
            
                //add rows to grid
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20.0) });

                for (int i = 0; i < weightClass.weightClassFighters.Count(); i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(140.0) });
                }

                //create scrollbar
                ScrollViewer scrollView = new ScrollViewer();
                         
                scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                
                //add grid to scrollviewer
                scrollView.Content = grid;  
        
                //add scrollviewer to tab
                tab.Content = scrollView;
       
                //add tab to tabControl
                tabControl.Items.Add(tab);
            }
        }


        private void createTrees()
        {
            try
            {
                foreach (WeightClass weightClass in tournament.weightClasses)
                {
                    foreach (TabItem tab in tabControl.Items)
                    {
                        if (weightClass.category == tab.Header.ToString())
                        {
                            ScrollViewer viewer = (ScrollViewer)tab.Content;
                            Grid grid = (Grid)viewer.Content;

                            //set first round in column 1
                            fightViewPerRoundPositioner(grid, weightClass.rounds[0], 1, 1, 2);

                            //set other columns
                            positionFightViews(grid, weightClass);
                        }
                    }
                }
            }
            catch (Exception exc)
            {      
                throw new GDCException("Error in method createTrees(): " + exc.Message);
            }
        }

        private void positionFightViews(Grid grid, WeightClass weightClass)
        {  
            try
            {

                switch (weightClass.weightClassFighters.Count)
                {
                    case 2:
                        //nothing extra needed
                        break;

                    case 3:     
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 1);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 1);
                        break;

                    case 4:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 2);
                        break;

                    case 5:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 3);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 3);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 3);
                        break;

                    case 6:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 3);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 3);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 3);
                        break;

                    case 7:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 4);
                        break;

                    case 8:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 4);
                        break;

                    case 9:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 8);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 5);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 5);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 5);
                        break;

                    case 10:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 8);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 5);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 5);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 5);
                        break;

                    case 11:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 6);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 6);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 6);
                        break;

                    case 12:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 6);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 6);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 6);
                        break;

                    case 13:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 12);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 8);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 8);
                        break;

                    case 14:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 12);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 8);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 8);
                        break;

                    case 15:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[1][3], 3, 14);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 12);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 8);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 8);                      
                        break;

                    case 16:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[1][3], 3, 14);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 12);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 8);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 8);                                     
                        break;

                    case 17:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[1][3], 3, 14);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 12);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 8);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 8);
                        fightViewPositioner(grid, weightClass.rounds[5][0], 11, 8);
                        break;

                    case 18:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[1][3], 3, 14);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 4);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 12);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 8);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 8);
                        fightViewPositioner(grid, weightClass.rounds[5][0], 11, 8);
                        break;

                    case 19:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[1][3], 3, 14);
                        fightViewPositioner(grid, weightClass.rounds[1][4], 3, 18);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 5);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 15);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 10);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 10);
                        fightViewPositioner(grid, weightClass.rounds[5][0], 11, 10);                  
                        break;

                    case 20:
                        fightViewPositioner(grid, weightClass.rounds[1][0], 3, 2);
                        fightViewPositioner(grid, weightClass.rounds[1][1], 3, 6);
                        fightViewPositioner(grid, weightClass.rounds[1][2], 3, 10);
                        fightViewPositioner(grid, weightClass.rounds[1][3], 3, 14);
                        fightViewPositioner(grid, weightClass.rounds[1][4], 3, 18);
                        fightViewPositioner(grid, weightClass.rounds[2][0], 5, 5);
                        fightViewPositioner(grid, weightClass.rounds[2][1], 5, 15);
                        fightViewPositioner(grid, weightClass.rounds[3][0], 7, 10);
                        fightViewPositioner(grid, weightClass.rounds[4][0], 9, 10);
                        fightViewPositioner(grid, weightClass.rounds[5][0], 11, 10);      
                        break;

                    default:
                        throw new GDCException("Error in fighter count of weightCategory " + weightClass.category);
                        break;
                }
                
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method positionFightViews(parameters): " + exc.Message);
            }
        }

        private void fightViewPositioner(Grid grid, FightView fightView, int x, int y)
        {
            try
            {
                grid.Children.Add(fightView);
                Grid.SetColumn(fightView, x);
                Grid.SetRow(fightView, y);
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method fightViewPositioner(parameters): " + exc.Message);
            }
        }

        //method adds FightView to Grid and sets position on Grid based on two integer values as starting point
        //and an int as interval between fightViews in the column
        private void fightViewPerRoundPositioner(Grid grid, List<FightView> list, int x, int y, int interval)
        {
            try
            {
                if (list != null)
                {
                    foreach (FightView fightView in list)
                    {
                        grid.Children.Add(fightView);
                        Grid.SetColumn(fightView, x);
                        Grid.SetRow(fightView, y);

                        y += interval;
                    }
                }
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method fightViewPerRoundPositioner(parameters): " + exc.Message);
            }
        }

        private void readFightersFromFile()
        {
            try
            {

                //var fileName = string.Format("{0}\\gdc.xlsx", Directory.GetCurrentDirectory());
                string fileName = string.Format("{0}\\gdc.xlsx", Directory.GetCurrentDirectory());
                //var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);
                string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);
                //var adapter = new OleDbDataAdapter("SELECT * FROM [Blad1$]", connectionString);
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Blad1$]", connectionString);
                //var ds = new DataSet();
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                DataTable data = ds.Tables[0];

                foreach (DataRow row in data.Rows)
                {
                    string firstName = row["Naam"].ToString();
                    string lastName = row["Voornaam"].ToString();
                    string club = row["Club"].ToString();
                    string clubLocation = row["Locatie"].ToString();
                    string category = row["Categorie"].ToString();

                    Fighter newFighter = new Fighter(firstName, lastName, club, clubLocation, category);
                    allFighters.Add(newFighter);
                }
            }
            catch (Exception exc)
            {
                throw new GDCException("Error in method readFightersFromFile(): " + exc.Message);
            }
        }
    }
}
