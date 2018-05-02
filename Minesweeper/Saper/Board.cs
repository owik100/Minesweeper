using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Minesweeper.Models;
using System.Diagnostics;


namespace Minesweeper.Saper
{
    public class Board
    {
       public Pole[,] pola;
       public int numbersOfMines=10;

        public Board()
        {
            GenerateBoard();
        }

        public void GenerateBoard()
        {
            pola = new Pole[9, 9];

            for (int i = 0; i < pola.GetLength(0); i++)
            {
                for (int j = 0; j < pola.GetLength(1); j++)
                {
                    pola[i, j] = new Pole();
                }
            }
        }

        public void GenerateMines(int firstClickX, int firstClickY)
        {
            Random random = new Random();

           int numbersOfMinesLocal = numbersOfMines;

            while(numbersOfMinesLocal > 0)
            {
                int x = random.Next(0, 8);
                int y = random.Next(0, 8);

                if(pola[x,y].value!=9 && x!=firstClickX && y!=firstClickY)
                {
                    pola[x, y].value = 9;
                    Debug.WriteLine(x + " " + y);

                    for (int i = x-1; i <= x+1; i++)
                    {
                        for (int j = y-1; j <= y+1; j++)
                        {  

                            if(i<0 || i >8 || j<0 || j>8)
                            {
                                continue;
                            }

                            if (pola[i, j].value == 9)
                            {
                                continue;
                            }

                            pola[i, j].value++;
                            
                        }
                    }

                    numbersOfMinesLocal--;
                }
            }
        }
    }
}
