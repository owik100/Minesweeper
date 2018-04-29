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
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        Database database = new Database();
        int timeGame;
        public AddRecord(int time)
        {
            InitializeComponent();
            textBlockTime.Text += " " + time;
            timeGame = time;
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            database.Connect();
            database.AddData(name, timeGame);
            Close();
        }
    }
}
