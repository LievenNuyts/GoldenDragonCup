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
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace GoldenDragonCup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var fileName = string.Format("{0}\\gdc.xls", Directory.GetCurrentDirectory());
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);

            var adapter = new OleDbDataAdapter("SELECT * FROM [Blad1$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds);

            DataTable data = ds.Tables[0];
            foreach (DataRow row in data.Rows)
            {
                string name = row["Naam"].ToString();
                string lastName = row["Voornaam"].ToString();
                string dob = row["Geboorte-datum"].ToString();
                string discipline = row["Discipline"].ToString();
                string weight = row["Gewicht"].ToString();
                string club = row["Club"].ToString();
                string location = row["Locatie"].ToString();
                string type = row["Type"].ToString();
                string category = row["Categorie"].ToString();
            }
        }

    }
}
