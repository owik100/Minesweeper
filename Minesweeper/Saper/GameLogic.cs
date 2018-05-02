using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using Minesweeper.Views;

namespace Minesweeper.Saper
{
    public static class GameLogic
    {

       public static bool firstClick = true;
       public static bool gameOver = false;
       public static bool gameWon = false;
       public static int numbersOfVisible = 81;
       public static int numberOfMinesByPlayer = 10;
       public static int time;



        public static void Pole_Click(object sender, EventArgs e, Board board, DispatcherTimer dispatcherTimer, Grid grid)
        {
            Button btn = sender as Button;
            string name = btn.Name;
            int x = int.Parse(name[2].ToString());
            int y = int.Parse((name[3]).ToString());

            if (firstClick)
            {
                firstClick = false;
                board.GenerateMines(x, y);
            }

            if (!board.pola[x, y].flagged && !gameWon && !gameOver)
            {
                dispatcherTimer.Start();

                if (board.pola[x, y].value == 0)
                {
                    ShowEmpty(x, y,board,grid);
                }
                else if (board.pola[x, y].value == 9)
                {
                    Image image = btn.Content as Image;
                    image.Visibility = Visibility.Visible;
                    image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/mine.png", UriKind.Relative));
                    btn.IsEnabled = false;
                    Fail(board,dispatcherTimer,grid);
                }
                else
                {
                    btn.Content = board.pola[x, y].value;
                    btn.Foreground = NumberColors.GetColor(board.pola[x, y].value);
                    if (!board.pola[x, y].visible)
                    {
                        board.pola[x, y].visible = true;
                        numbersOfVisible--;
                    }

                }
                CheckWin(board,dispatcherTimer,grid);
            }

        }

        public static void poleRightClick(object sender, MouseButtonEventArgs e, Board board, Grid grid)
        {
            Button btn = sender as Button;
            string name = btn.Name;
            int x = int.Parse(name[2].ToString());
            int y = int.Parse((name[3]).ToString());
            Label minesCounter = grid.FindName("minesCounter") as Label;

            if (!board.pola[x, y].flagged && numberOfMinesByPlayer > 0 && !board.pola[x, y].visible && !gameWon)
            {
                numberOfMinesByPlayer--;
                minesCounter.Content = numberOfMinesByPlayer;
                board.pola[x, y].flagged = true;

                Image image = (LogicalTreeHelper.FindLogicalNode(grid, "i_" + x + y)) as Image;
                image.Visibility = Visibility.Visible;
                image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/flag.png", UriKind.Relative));
            }
            else if (board.pola[x, y].flagged && !gameWon)
            {
                numberOfMinesByPlayer++;
                minesCounter.Content = numberOfMinesByPlayer;
                board.pola[x, y].flagged = false;
                Image image = btn.Content as Image;
                image.Visibility = Visibility.Collapsed;
            }

        }


            private static void ShowEmpty(int x, int y, Board board, Grid grid)
        {

            if (x < 0 || x > 8) return;
            if (y < 0 || y > 8) return;

            if (board.pola[x, y].visible) return;

            if (board.pola[x, y].value != 9 && board.pola[x, y].visible == false)
            {
                board.pola[x, y].visible = true;
                numbersOfVisible--;
                Button button = (LogicalTreeHelper.FindLogicalNode(grid, "b_" + x + y)) as Button;

                if (board.pola[x, y].value != 0)
                {
                    button.Content = board.pola[x, y].value;
                    button.Foreground = NumberColors.GetColor(board.pola[x, y].value);
                }
                else
                {
                    button.IsEnabled = false;
                }

            }

            if (board.pola[x, y].flagged)
            {
                board.pola[x, y].flagged = false;
                numberOfMinesByPlayer++;
                Label minesCounter = grid.FindName("minesCounter") as Label;
                minesCounter.Content = numberOfMinesByPlayer;

                Button btn = (LogicalTreeHelper.FindLogicalNode(grid, "b_" + x + y)) as Button;
                btn.Content = null;
            }


            if (board.pola[x, y].value != 0) return;

            ShowEmpty(x, y,board,grid);
            ShowEmpty(x - 1, y - 1, board, grid);
            ShowEmpty(x - 1, y, board, grid);
            ShowEmpty(x - 1, y + 1, board, grid);
            ShowEmpty(x + 1, y - 1, board, grid);
            ShowEmpty(x + 1, y, board, grid);
            ShowEmpty(x + 1, y + 1, board, grid);
            ShowEmpty(x, y - 1, board, grid);
            ShowEmpty(x, y + 1, board, grid);
        }




        private static void CheckWin(Board board,DispatcherTimer dispatcherTimer, Grid grid)
        {
            if (numbersOfVisible == board.numbersOfMines)
            {
                gameWon = true;
                Image faceImage = grid.FindName("faceImage") as Image;
                faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceWin.png", UriKind.Relative));
                dispatcherTimer.Stop();
                AddRecord addRecord = new AddRecord(time);
                addRecord.ShowDialog();
                DataBaseWindow dataBaseWindow = new DataBaseWindow();
                dataBaseWindow.ShowDialog();
            }
        }





        private static void Fail(Board board, DispatcherTimer dispatcherTimer, Grid grid)
        {
            gameOver = true;
            Image faceImage = grid.FindName("faceImage") as Image;
            faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceDead.png", UriKind.Relative));
            dispatcherTimer.Stop();

            for (int i = 0; i < board.pola.GetLength(0); i++)
            {
                for (int j = 0; j < board.pola.GetLength(1); j++)
                {
                    if (board.pola[i, j].value == 9)
                    {
                        Image image = (LogicalTreeHelper.FindLogicalNode(grid, "i_" + i + j)) as Image;
                        image.Visibility = Visibility.Visible;
                        image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/mine.png", UriKind.Relative));
                    }

                    if (!board.pola[i, j].visible)
                    {
                        Button button = (LogicalTreeHelper.FindLogicalNode(grid, "b_" + i + j)) as Button;
                        button.IsEnabled = false;
                    }
                }
            }
        }


        public static void RestartGame(Grid grid, Board board, DispatcherTimer dispatcherTimer)
        {

            board.GenerateBoard();
            firstClick = true;
            gameOver = false;
            gameWon = false;
            numbersOfVisible = 81;
            numberOfMinesByPlayer = 10;
            time = 0;

            Label minesCounter = grid.FindName("minesCounter") as Label;
            Label timeCounter = grid.FindName("timeCounter") as Label;

            timeCounter.Content = 0;
            minesCounter.Content = 10;
            dispatcherTimer.Stop();
        }




    }
}
