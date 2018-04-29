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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Minesweeper.Saper;

using System.Diagnostics;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();


        Board board = new Board();
        public bool gameOver = false;
        public bool gameWon=false;
        int numbersOfVisible = 81;
        int numberOfMinesByPlayer = 10;
        int time;
        bool firstClick = true;


        public MainWindow()
        {
            InitializeComponent();
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(PoleClickDown), true);
            AddHandler(FrameworkElement.MouseUpEvent, new MouseButtonEventHandler(PoleClickUp), true);

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            timeCounter.Content = 0;

            //board.GenerateMines();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
              time++;
              timeCounter.Content = time;
        }

        //private void RefreshBoard()
        //{
        //    for (int i = 0; i < board.pola.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < board.pola.GetLength(1); j++)
        //        {
        //            Button button = (Button)this.FindName("b_" + i + j);
        //            button.Content = board.pola[i, j].value;
        //        }
        //    }
        //}

        private void Pole_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name;
            int x = int.Parse(name[2].ToString());
            int y = int.Parse((name[3]).ToString());

            if(firstClick)
            {
                firstClick = false;
                board.GenerateMines(x, y);
            }

            if(!board.pola[x,y].flagged && !gameWon && !gameOver)
            {
                dispatcherTimer.Start();

                if (board.pola[x, y].value == 0)
                {
                    ShowEmpty(x, y);
                }
                else if (board.pola[x, y].value == 9)
                {
                    Image image = (Image)this.FindName("i_" + x + y);
                    image.Visibility = Visibility.Visible;
                    image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/mine.png", UriKind.Relative));
                    btn.IsEnabled = false;
                    Fail();
                }
                else
                {
                    btn.Content = board.pola[x, y].value;
                    btn.Foreground = GetColor(board.pola[x, y].value);
                    if (!board.pola[x, y].visible)
                    {
                        board.pola[x, y].visible = true;
                        numbersOfVisible--;
                    }
        
                }
                CheckWin();
            }
           
        }

        private void poleRightClick(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name;
            int x = int.Parse(name[2].ToString());
            int y = int.Parse((name[3]).ToString());

            if (!board.pola[x, y].flagged && numberOfMinesByPlayer>0 && !board.pola[x, y].visible && !gameWon)
            {
                numberOfMinesByPlayer--;
                minesCounter.Content = numberOfMinesByPlayer;
                board.pola[x, y].flagged = true;
                Image image = (Image)this.FindName("i_" + x + y);
                image.Visibility = Visibility.Visible;

                image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/flag.png", UriKind.Relative));
            }
            else if(board.pola[x, y].flagged && !gameWon)
            {
                numberOfMinesByPlayer++;
                minesCounter.Content = numberOfMinesByPlayer;
                board.pola[x, y].flagged = false;
                Image image = (Image)this.FindName("i_" + x + y);
                image.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckWin()
        {
            if(numbersOfVisible==board.numbersOfMines)
            {
                Debug.WriteLine("WIN");
                gameWon = true;
                faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceWin.png", UriKind.Relative));
                dispatcherTimer.Stop();
                AddRecord addRecord = new AddRecord(time);
                addRecord.ShowDialog();
                Wyniki_Click(null, null);
            }
        }

        private void PoleClickDown(object sender, MouseButtonEventArgs e)
        {
            bool mouseIsDown = Mouse.LeftButton == MouseButtonState.Pressed;
            if(mouseIsDown && gameOver==false && !gameWon)
            {
                faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceWarning.png", UriKind.Relative));
            } 
        }

        private void PoleClickUp(object sender, MouseButtonEventArgs e)
        {
            if(!gameOver && !gameWon)
            faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceNormal.png", UriKind.Relative));
        }

        private void ShowEmpty(int x, int y)
        {

            if (x < 0 || x > 8) return;
            if (y < 0 || y > 8) return;

            if (board.pola[x, y].visible) return;

            if (board.pola[x, y].value != 9 && board.pola[x, y].visible == false)
            {
                board.pola[x, y].visible = true;
                numbersOfVisible--;
                Button button = (Button)this.FindName("b_" + x + y);

                if (board.pola[x, y].value != 0)
                {
                    button.Content = board.pola[x, y].value;
                    button.Foreground = GetColor(board.pola[x, y].value);
                }
                else
                {
                    button.IsEnabled = false;
                }

            }

            if(board.pola[x, y].flagged)
            {
                board.pola[x, y].flagged = false;
                numberOfMinesByPlayer++;
                minesCounter.Content =  numberOfMinesByPlayer;
                Image image = (Image)this.FindName("i_" + x + y);
                image.Visibility = Visibility.Collapsed;
            }
           

            if (board.pola[x, y].value != 0) return;

            ShowEmpty(x, y);
            ShowEmpty(x - 1, y - 1);
            ShowEmpty(x - 1, y);
            ShowEmpty(x - 1, y + 1);
            ShowEmpty(x + 1, y - 1);
            ShowEmpty(x + 1, y);
            ShowEmpty(x + 1, y + 1);
            ShowEmpty(x, y - 1);
            ShowEmpty(x, y + 1);
        }

        private Brush GetColor(int value)
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

        private void Fail()
        {
            gameOver = true;
            faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceDead.png", UriKind.Relative));
            dispatcherTimer.Stop();

            for (int i = 0; i < board.pola.GetLength(0); i++)
            {
                for (int j = 0; j < board.pola.GetLength(1); j++)
                {
                    if(board.pola[i,j].value==9)
                    {
                        Image image = (Image)this.FindName("i_" + i + j);
                        image.Visibility = Visibility.Visible;
                        image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/mine.png", UriKind.Relative));
                    }

                    if(!board.pola[i, j].visible)
                    {
                        Button button = (Button)this.FindName("b_" + i + j);
                        button.IsEnabled = false;
                    } 
                }
            }
        }

        private void faceImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("RESTART");
            Restart();
        }

        private void Restart()
        {
            for (int i = 0; i < board.pola.GetLength(0); i++)
            {
                for (int j = 0; j < board.pola.GetLength(1); j++)
                {
                    Button button = (Button)this.FindName("b_" + i + j);
                    Image image = (Image)this.FindName("i_" + i + j);

                    button.IsEnabled = true;
                    button.Content = image;

                    image.Visibility = Visibility.Collapsed;
                    image.Source = new BitmapImage(new Uri("/Minesweeper;component/img/mine.png", UriKind.Relative));
                    faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceNormal.png", UriKind.Relative));
                }
            }

            board.GenerateBoard();
            firstClick = true;
            gameOver = false;
            gameWon = false;
            numbersOfVisible = 81;
            numberOfMinesByPlayer = 10;
            time = 0;
            timeCounter.Content = 0;
            minesCounter.Content = 10 ;
            dispatcherTimer.Stop();
        }

        private void MenuItem_Restart(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        private void Wyniki_Click(object sender, RoutedEventArgs e)
        {
            DataBaseWindow dataBaseWindow = new DataBaseWindow();
            dataBaseWindow.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            string language = (sender as MenuItem).Tag.ToString();

            HelpWindow helpWindow = new HelpWindow(language);
            helpWindow.ShowDialog();
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            AddRecord addRecord = new AddRecord(time);
            addRecord.ShowDialog();
            Wyniki_Click(null,null);
        }
    }
}
