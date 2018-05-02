using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minesweeper.Saper
{
   public static class NumberColors
    {
        public static Brush GetColor(int value)
        {
            switch (value)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.DarkBlue;
                case 5: return Brushes.Brown;
                case 6: return Brushes.Cyan;
                case 7: return Brushes.Black;
                case 8: return Brushes.Gray;
                default: return Brushes.Blue;
            }
        }
    }
}
