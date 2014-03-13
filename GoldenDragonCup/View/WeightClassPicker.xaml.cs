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
    /// Interaction logic for WeightClassPicker.xaml
    /// </summary>
   
   
    public partial class WeightClassPicker : Window
    {
        private List<WeightClass> weightClassList;


        public WeightClassPicker(List<WeightClass> weightClassList)
        {
            try
            {

                InitializeComponent();

                this.weightClassList = weightClassList;

                foreach (WeightClass weightClass in weightClassList)
                {
                    if (weightClass.inView == true)
                    {
                        lstb_inView.Items.Add(weightClass);
                    }
                    else
                    {
                        lstb_outView.Items.Add(weightClass);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in constructor WeightClassPicker(param): " + exc.Message);
            }
        }

        //add a weightclass to this tournament (deze mat)
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstb_outView.SelectedItem != null)
                {
                    WeightClass weightClass = (WeightClass)lstb_outView.SelectedItem;

                    lstb_inView.Items.Add(weightClass);
                    lstb_outView.Items.Remove(weightClass);
                    weightClass.inView = true;
                }
                else
                {
                    MessageBox.Show("First make a selection");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method btn_add_Click(params): " + exc.Message);
            }
        }

        //remove a weightclass from this tournament (deze mat)
        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstb_inView.SelectedItem != null)
                {
                    WeightClass weightClass = (WeightClass)lstb_inView.SelectedItem;

                    lstb_outView.Items.Add(weightClass);
                    lstb_inView.Items.Remove(weightClass);
                    weightClass.inView = false;
                }
                else
                {
                    MessageBox.Show("First make a selection");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method btn_remove_Click(params): " + exc.Message);
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {


            }
            catch (Exception exc)
            {
                MessageBox.Show("Error in method btn_OK_Click(params): " + exc.Message);
            }
        }
    }
}
