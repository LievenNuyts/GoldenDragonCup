﻿using System;
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
        public string excelFile;
        //static Random random;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                allFighters = new ArrayList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        #region METHODS TO CREATE TABS AND TREES

        private void createTabsforWeightClasses()
        {
            try
            {
                foreach (WeightClass weightClass in tournament.weightClasses)
                {
                    TabItem tab = new TabItem();
                    tab.Header = weightClass.category;

                    Grid grid = new Grid();
                    grid.Background = Brushes.Crimson;
                    grid.Height = 950;

                    //add columns to grid              
                    for (int i = 0; i < 6; i++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(15.0) });
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(145.0) });
                    }

                    //add rows to grid
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15.0) });

                    for (int i = 0; i < 9; i++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80.0) });
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
            catch (Exception exc)
            {
                MessageBox.Show("Error in method createTabsforWeightClasses(): " + exc.Message);
                throw new GDCException("Error in method createTabsforWeightClasses(): " + exc.Message);
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

                            setRows(grid, weightClass);                        
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method createTrees(): " + exc.Message);
                throw new GDCException("Error in method createTrees(): " + exc.Message);
            }
        }

        #endregion

      
        #region METHODS FOR FIGHTVIEW POSITIONING

        //method to set the last column
        public void setRow6(Grid grid, WeightClass weightClass)
        {
            try
            {
                List<FightView> roundFINAL = weightClass.rounds[weightClass.rounds.Count - 1];
                grid.Children.Add(roundFINAL[0]);
                Grid.SetColumn(roundFINAL[0], 11);
                Grid.SetRow(roundFINAL[0], 5);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method setRow6(parameters): " + exc.Message);
                throw new GDCException("Error in method setRow6(parameters): " + exc.Message);
            }
        }

        //method to set column 4 and 5
        public void setRow45(Grid grid, WeightClass weightClass)
        {
            try
            {
                if (weightClass.rounds.Count >= 3)
                {
                    int i = 0;

                    foreach (List<FightView> list in weightClass.rounds)
                    {
                        i += list.Count;
                    }

                    if (i == 3) //it's a round robin
                    {

                        List<FightView> robinThree = weightClass.rounds[weightClass.rounds.Count - 1];
                        grid.Children.Add(robinThree[0]);
                        Grid.SetColumn(robinThree[0], 11);
                        Grid.SetRow(robinThree[0], 5);

                        List<FightView> robinTwo = weightClass.rounds[weightClass.rounds.Count - 2];
                        grid.Children.Add(robinTwo[0]);
                        Grid.SetColumn(robinTwo[0], 9);
                        Grid.SetRow(robinTwo[0], 5);

                        List<FightView> robinOne = weightClass.rounds[weightClass.rounds.Count - 3];
                        grid.Children.Add(robinOne[0]);
                        Grid.SetColumn(robinOne[0], 7);
                        Grid.SetRow(robinOne[0], 5);
                    }
                    else
                    {
                        setRow6(grid, weightClass);

                        List<FightView> roundFinal = weightClass.rounds[weightClass.rounds.Count - 2];
                        grid.Children.Add(roundFinal[0]);
                        Grid.SetColumn(roundFinal[0], 9);
                        Grid.SetRow(roundFinal[0], 5);

                        List<FightView> semiFinal = weightClass.rounds[weightClass.rounds.Count - 3];
                        grid.Children.Add(semiFinal[0]);
                        Grid.SetColumn(semiFinal[0], 7);
                        Grid.SetRow(semiFinal[0], 4);

                        grid.Children.Add(semiFinal[1]);
                        Grid.SetColumn(semiFinal[1], 7);
                        Grid.SetRow(semiFinal[1], 6);
                    }
                }
                else
                {
                    setRow6(grid, weightClass);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method setRow456(parameters): " + exc.Message);
                throw new GDCException("Error in method setRow456(parameters): " + exc.Message);
            }
        }

        //method to set the third row
        public void setRow3(Grid grid, WeightClass weightClass)
        {
            try
            {
                setRow45(grid, weightClass);

                if (weightClass.rounds.Count >= 4)
                {
                    List<FightView> quatreFinal = weightClass.rounds[weightClass.rounds.Count - 4];

                    for (int i = 0, y = 2; i < quatreFinal.Count; i++, y += 2)
                    {
                        grid.Children.Add(quatreFinal[i]);
                        Grid.SetColumn(quatreFinal[i], 5);
                        Grid.SetRow(quatreFinal[i], y);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method setRow3(parameters): " + exc.Message);
                throw new GDCException("Error in method setRow3(parameters): " + exc.Message);
            }
        }

        //method to set the second row
        public void setRow2(Grid grid, WeightClass weightClass)
        {
            try
            {
                setRow3(grid, weightClass);

                if (weightClass.rounds.Count >= 5)
                {
                    List<FightView> eighthFinal = weightClass.rounds[weightClass.rounds.Count - 5];

                    for (int i = 0, y = 1; i < eighthFinal.Count; i++, y++)
                    {
                        if (y == 5)
                        {
                            y++;
                        }

                        grid.Children.Add(eighthFinal[i]);
                        Grid.SetColumn(eighthFinal[i], 3);
                        Grid.SetRow(eighthFinal[i], y);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method setRow2(parameters): " + exc.Message);
                throw new GDCException("Error in method setRow2(parameters): " + exc.Message);
            }
        }

        //method to set the first row
        public void setRows(Grid grid, WeightClass weightClass)
        {
            try
            {
                setRow2(grid, weightClass);

                if (weightClass.rounds.Count == 6)
                {
                    List<FightView> selectionRound = weightClass.rounds[weightClass.rounds.Count - 6];

                    for (int i = 0, y = 2; i < selectionRound.Count; i++, y += 2)
                    {
                        grid.Children.Add(selectionRound[i]);
                        Grid.SetColumn(selectionRound[i], 1);
                        Grid.SetRow(selectionRound[i], y);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method setRow1(parameters): " + exc.Message);
                throw new GDCException("Error in method setRow1(parameters): " + exc.Message);
            }
        }

        #endregion


        #region METHODS FOR LISTBOX

        //method to populate listbox
        private void fightViewsToListBox()
        {
            try
            {
                int counter = 0;
                
                for (int i = 0; i < 6; i++)
                {
                    foreach (WeightClass weightClass in tournament.weightClasses)
                    {
                        if (i < weightClass.rounds.Count)
                        {
                            foreach (FightView fightView in weightClass.rounds[i])
                            {
                                lstb_fights.Items.Add(fightView.ToString());

                                if (counter == 0)
                                {
                                    makeItGlow(fightView, null);
                                }

                                counter++;
                            }
                        }
                    }
                }

                lstb_fights.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method fightViewsToListBox(): " + exc.Message);
                throw new GDCException("Error in method fightViewsToListBox(): " + exc.Message);
            }
        }

        //overload method to populate listbox
        //index added to be able to use the makeItGlow() method
        private void fightViewsToListBox(int index)
        {
            FightView fightViewNew = null;
            FightView fightViewOld = null;
            
            try
            {
                int counter = 0;
                
                for (int i = 0; i < 6; i++)
                {
                    foreach (WeightClass weightClass in tournament.weightClasses)
                    {
                        if (i < weightClass.rounds.Count)
                        {
                            foreach (FightView fightView in weightClass.rounds[i])
                            {
                                lstb_fights.Items.Add(fightView.ToString());

                                if (counter == index - 1)
                                {
                                    fightViewOld = fightView;
                                }                         
                                
                                if (counter == index)
                                {
                                    fightViewNew = fightView;
                                }

                                counter++;
                            }
                        }
                    }
                }

                makeItGlow(fightViewNew, fightViewOld);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method fightViewsToListBox(int index): " + exc.Message);
                throw new GDCException("Error in method fightViewsToListBox(int index): " + exc.Message);
            }
        }

        //method to update listbox after each finished fight (or undo)
        public void oneUp(Boolean up)
        {
            int index = lstb_fights.SelectedIndex;

            if (up) //if true goes one up
            {
                index++;
            }
            else //if false goes one down
            {
                index--;
            }

            //clear and reload the listbox
            lstb_fights.Items.Clear();
            fightViewsToListBox(index);
            //set selection according to new index
            lstb_fights.SelectedIndex = index;
        }


        //method that 'colors' the tab and fightview of the current active fight
        public void makeItGlow(FightView fightViewNew, FightView fightViewOld)
        {
            //remove color from previous active tab
            foreach (TabItem item in tabControl.Items)
            {
                item.ClearValue(TabItem.BackgroundProperty);           
            }

            //color the tabtext of the active fight
            foreach (TabItem item in tabControl.Items)
            {
                if (item.Header.ToString() == fightViewNew.weightClass.category)
                {
                    item.Background = Brushes.Gold;
                }                    
            }

            //color the active fight
            if (fightViewNew != null)
            {
                fightViewNew.INPROGRESS = true;
            }

            //remove color from previous active fight
            if (fightViewOld != null)
            {         
                fightViewOld.INPROGRESS = false;
            }
        }

        #endregion


        //STARTING A NEW TOURNAMENT
        //Starting a new tournament
        private void btn_newTournament_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                excelFile = createOpenFileDialog();

                if (excelFile != null)
                {
                    readFightersFromFile(excelFile);
                    //tournament = new Tournament("Golden Dragon Cup 2014", allFighters, this);
                    tournament = new Tournament("Golden Dragon Cup 2014", createTestFighters(), this);
                    createTabsforWeightClasses();
                    createTrees();
                    fightViewsToListBox();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method btn_newTournament_Click(params): " + exc.Message);
                throw new GDCException("Error in method btn_newTournament_Click(params): " + exc.Message);
            }
        }


        #region METHODS FOR EXCEL IMPORT

        //method to open a filedialog to import an excel file
        private string createOpenFileDialog()
        {
            try
            {
                // Create OpenFileDialog 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                String filename = null;

                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".txt";
                dlg.Filter = "EXCEL Files (*.xls)|*.xlsx";

                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    filename = dlg.FileName;
                }

                return filename;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method createOpenFileDialog(): " + exc.Message);
                throw new GDCException("Error in method createOpenFileDialog(): " + exc.Message);
            }
        }

        //method to import data from excel, create fighters from data and add fighters to allFighters arraylist
        private void readFightersFromFile(string file)
        {
            try
            {
                string conn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties='Excel 12.0;HDR=Yes';", file);
                
                OleDbConnection objConn = new OleDbConnection(conn);            
                objConn.Open();
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Blad1$]", objConn);
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(); 
                objAdapter.SelectCommand = objCmdSelect;   
                DataSet objDataset = new DataSet();  
                objAdapter.Fill(objDataset); 
                objConn.Close();

                //objTable contains excel data
                DataTable objTable = objDataset.Tables[0];

                foreach (DataRow row in objTable.Rows)
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
                MessageBox.Show("Error in method readFightersFromFile(): " + exc.Message);
                throw new GDCException("Error in method readFightersFromFile(): " + exc.Message);
            }
        }

        #endregion


        #region TEST METHODS

        //method to create test fighters (not from excel file)
        private ArrayList createTestFighters()
        {
            /*
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

            Fighter fighter8 = new Fighter("Pieter", "Vermeulen", "Golden Phoenix", "Brussel, Belgium", "SFC M +65");
            fighters.Add(fighter8);

            Fighter fighter9 = new Fighter("Joris", "Peters", "Long Hu Men", "Leuven, Belgium", "SFC M +65");
            fighters.Add(fighter9);


            for (int i = 1; i < 21; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +55");
                fighters.Add(fighter);
            }

            return fighters;*/

            //return createFightersUpTo10();
            return createFightersUpTo20();

        }

        //Test method to create testfighters groups of 2 to 10
        public ArrayList createFightersUpTo10()
        {
            ArrayList fighters = new ArrayList();

            //create fighters 2 - 10
            
            for (int i = 1; i < 3; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +55");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 4; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +60");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 5; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +65");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 6; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +70");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 7; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +75");
                fighters.Add(fighter);
            }

            
            for (int i = 1; i < 8; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +80");
                fighters.Add(fighter);
            }

            
            for (int i = 1; i < 9; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +85");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 10; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +90");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 11; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +95");
                fighters.Add(fighter);
            }

            return fighters;
        }

        //Test method to create testfighters groups of 10 to 20
        public ArrayList createFightersUpTo20()
        {
            ArrayList fighters = new ArrayList();

            //create fighters 11-20
            
            for (int i = 1; i < 12; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +55");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 13; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +60");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 14; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +65");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 15; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +70");
                fighters.Add(fighter);
            }

            
            for (int i = 1; i < 16; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +75");
                fighters.Add(fighter);
            }

            
            for (int i = 1; i < 17; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +80");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 18; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +85");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 19; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +90");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 20; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +95");
                fighters.Add(fighter);
            }

            for (int i = 1; i < 21; i++)
            {
                Fighter fighter = new Fighter("Fighter", i.ToString(), "Long Hu Men", "Leuven, Belgium", "FC M +100");
                fighters.Add(fighter);
            }

            return fighters;
        }


        #endregion
    }
}
