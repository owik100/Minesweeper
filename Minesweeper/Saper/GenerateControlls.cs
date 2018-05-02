using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper.Saper
{
    public static class GenerateControlls
    {
        public static void GenerateGrid(Grid grid)
        {

            RowDefinition[] row = new RowDefinition[10];
            ColumnDefinition[] col = new ColumnDefinition[10];

            for (int i = 0; i < 10; i++)
            {
                row[i] = new RowDefinition();
                row[i].Height = new GridLength(5, GridUnitType.Star);
                grid.RowDefinitions.Add(row[i]);


                col[i] = new ColumnDefinition();
                col[i].Width = new GridLength(5, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col[i]);
            }
        }


        public static void GeneateBoardControlls(Grid grid, RoutedEventHandler Pole_Click, MouseButtonEventHandler poleRightClick)
        {
            Button[,] btn = new Button[9, 9];

            Image[,] img = new Image[9, 9];

            for (int i = 0; i < btn.GetLength(0); i++)
            {
                for (int j = 0; j < btn.GetLength(1); j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].Name = "b_" + i + j;
                    btn[i, j].FontWeight = FontWeights.ExtraBold;
                    btn[i, j].FontSize = 15;

                    btn[i, j].Click += new RoutedEventHandler(Pole_Click);
                    btn[i, j].MouseRightButtonDown += new MouseButtonEventHandler(poleRightClick);


                    img[i, j] = new Image();
                    img[i, j].Name = "i_" + i + j;
                    img[i, j].Stretch = Stretch.Uniform;
                    img[i, j].Visibility = Visibility.Collapsed;
                    img[i, j].Source = new BitmapImage(new Uri("/Minesweeper;component/img/mine.png", UriKind.Relative));


                    btn[i, j].Content = img[i, j];

                    btn[i, j].SetValue(Grid.RowProperty, i + 1);
                    btn[i, j].SetValue(Grid.ColumnProperty, j + 1);
                    grid.Children.Add(btn[i, j]);
                }
            }
        }

        public static void RemoveOldControlls(Grid grid)
        {
            Button[,] btn = new Button[9, 9];

            Image[,] img = new Image[9, 9];

            for (int i = 0; i < btn.GetLength(0); i++)
            {
                for (int j = 0; j < btn.GetLength(1); j++)
                {
                    btn[i, j] = (LogicalTreeHelper.FindLogicalNode(grid, "b_" + i + j)) as Button;
                    grid.Children.Remove(btn[i, j]);
                }
            }


        }

    }
}
