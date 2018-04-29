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

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow(string language)
        {
            InitializeComponent();

            string eng = "The objective of the game is to clear a board containing hidden mines without detonating any of them with help from clues about the number of neighboring mines in each field (from zero to eight). If we mark a field with a flag (PPM), it is protected against being unveiled, so that we will not reveal the mine by accident.";
            string pl = " Gra polega na odkrywaniu na planszy poszczególnych pól w taki sposób, aby nie natrafić na minę. Na każdym z odkrytych pól napisana jest liczba min, które bezpośrednio stykają się z danym polem (od zera do ośmiu). Jeśli oznaczymy dane pole flagą (PPM), jest ono zabezpieczone przed odsłonięciem, dzięki czemu przez przypadek nie odsłonimy miny.";

            if (language=="eng")
            {
                textBlockTitle.Text = "Minesweeper";
                textBlockHelp.Text = eng;
            }
            else
            {
                textBlockTitle.Text = "Saper";
                textBlockHelp.Text = pl;
            }
            
        }
    }
}
