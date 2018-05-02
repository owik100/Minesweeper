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
using System.Windows.Threading;


namespace Minesweeper.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board = new Board();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(PoleClickDown), true);
            AddHandler(FrameworkElement.MouseUpEvent, new MouseButtonEventHandler(PoleClickUp), true);

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            timeCounter.Content = 0;

            GenerateControlls.GenerateGrid(grid);
            GenerateControlls.GeneateBoardControlls(grid, Pole_Click ,poleRightClick);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           GameLogic.time++;
           timeCounter.Content = GameLogic.time;
        }

        public void Pole_Click(object sender, EventArgs e)
        {
            GameLogic.Pole_Click(sender, e, board, dispatcherTimer, grid);
        }

        private void poleRightClick(object sender, MouseButtonEventArgs e)
        {
            GameLogic.poleRightClick(sender, e, board, grid);
        }

        private void PoleClickDown(object sender, MouseButtonEventArgs e)
        {
            bool mouseIsDown = Mouse.LeftButton == MouseButtonState.Pressed;
            if (mouseIsDown && GameLogic.gameOver == false && !GameLogic.gameWon)
                   faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceWarning.png", UriKind.Relative));
            
        }

        private void PoleClickUp(object sender, MouseButtonEventArgs e)
        {
            if (!GameLogic.gameOver && !GameLogic.gameWon)
                faceImage.Source = new BitmapImage(new Uri("/Minesweeper;component/img/faceNormal.png", UriKind.Relative));
        }

        private void faceImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GenerateControlls.RemoveOldControlls(grid);
            GenerateControlls.GeneateBoardControlls(grid, Pole_Click, poleRightClick);
            GameLogic.RestartGame(grid,board,dispatcherTimer);
        }

        private void MenuItem_Restart(object sender, RoutedEventArgs e)
        {
            GenerateControlls.RemoveOldControlls(grid);
            GenerateControlls.GeneateBoardControlls(grid, Pole_Click, poleRightClick);
            GameLogic.RestartGame(grid, board, dispatcherTimer);
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
            AddRecord addRecord = new AddRecord(GameLogic.time);
            addRecord.ShowDialog();
            Wyniki_Click(sender, e);
        }

    }
}
