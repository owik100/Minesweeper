using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Minesweeper.BazaDanych;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for DataBaseWindow.xaml
    /// </summary>
    public partial class DataBaseWindow : Window
    {
        Database database = new Database();

        public DataBaseWindow()
        {
            InitializeComponent();
            database.Connect();
            dataGridView.ItemsSource = database.PrintData();
          
        }

        private void dataGridView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
           if(e.Column.Header.ToString()=="ID")
            {
                e.Cancel = true;
            }
           
        }

        private void DeleteResultClick(object sender, RoutedEventArgs e)
        {
            Wyniki wynik = (Wyniki)dataGridView.SelectedItem;

            database.DeleteData(wynik.ID);
            dataGridView.ItemsSource = database.PrintData();
        }

        private void DeleteAllResultsClick(object sender, RoutedEventArgs e)
        {
            database.DeleteAll();
            dataGridView.ItemsSource = database.PrintData();
        }

        private void dataGridView_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(dataGridView.SelectedIndex >= 0)
            {
                btnDeleteSelected.IsEnabled = true;
            }
            else
            {
                btnDeleteSelected.IsEnabled = false;
            }

        }

       
    }
}
