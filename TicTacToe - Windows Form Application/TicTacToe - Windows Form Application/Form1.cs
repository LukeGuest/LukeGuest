using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe___Windows_Form_Application
{
    public enum PlayerType
    {
        x,
        o
    }

    public enum WinType
    {
        X,
        O,
        Draw,
        None
    }

    public partial class TicTacToeForm : Form
    {
        class PositionCoords
        {
            public int Row, Column;
        }

        private PlayerType currentPlayer = PlayerType.x;

        //2D array representing game grid
        private Button[,] buttonGrid;
        private string[,] gameGrid;

        private int turnNumber;

        private int xWin = 0;
        private int oWin = 0;

        

        public TicTacToeForm()
        {
            InitializeComponent();

            //Initialise array with buttons from Form
            buttonGrid = new Button[3,3] { {R1B1, R1B2, R1B3 },
                                       { R2B1, R2B2, R2B3},
                                       { R3B1, R3B2, R3B3 } };
            gameGrid = new string[3, 3]
            {
                { "","","" },
                { "","",""},
                { "","","" }
            };
        }

        private void Button_Click(object sender, EventArgs e)
        {
            turnNumber++;
            Button btn = (Button)sender;

            if (btn.Text == "")
            {
                btn.Text = currentPlayer.ToString();

                for (int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        if (gameGrid[i, j] != buttonGrid[i, j].Text)
                        {
                            gameGrid[i, j] = buttonGrid[i, j].Text;
                            Console.WriteLine("CHANGED GAMEGRID VALUE: {0}{1} WITH VALUE IN: {2}", i, j, buttonGrid[i, j].Name);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                WinType winCheck = WinCheck(gameGrid,false);
                if(winCheck != WinType.None)
                {
                    EndGameMessage(winCheck);
                }

                SwitchPlayer();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turnNumber = 0;

            //Enable and reset buttons
            for (int i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for(int j = 0; j < buttonGrid.GetLength(1); j++)
                {
                    buttonGrid[i,j].Enabled = true;
                    buttonGrid[i,j].Text = "";
                }
            }

            currentPlayer = PlayerType.x;
            TurnLabel.Text = "X";

            //AIBestMove();
            //PositionCoords bestPosition = AIBestMove();
            //buttonGrid[bestPosition.Row, bestPosition.Column].Text = PlayerType.x.ToString();
            //SwitchPlayer();
            
        }

        private void resetScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XScoreLabel.Text = 0.ToString();
            OScoreLabel.Text = 0.ToString();
            xWin = 0;
            oWin = 0;
        }

        #region Custom Methods
        private void SwitchPlayer()
        {
            if(currentPlayer == PlayerType.x)
            {
                currentPlayer = PlayerType.o;
                TurnLabel.Text = "o";
                PositionCoords minMax = MiniMax();
                Console.WriteLine("{0},{1}", minMax.Row, minMax.Column);
                buttonGrid[minMax.Row, minMax.Column].Text = currentPlayer.ToString();
                SwitchPlayer();
            }
            else
            {
                currentPlayer = PlayerType.x;
                
                //PositionCoords bestPosition = AIBestMove();
                //buttonGrid[bestPosition.Row, bestPosition.Column].Text = currentPlayer.ToString();
                
            }
        }

        private WinType WinCheck(string[,] grid, bool minMaxCheck)
        {
            //Console.WriteLine("Win Checking");
            bool winner = false;

            if ((grid[0,0] == grid[0,1]) && (grid[0,1] == grid[0,2]) && grid[0,0] != "")
            {
                winner = true;
            }
            else if ((grid[1,0] == grid[1,1]) && (grid[1,1] == grid[1,2]) && grid[1,0] != "")
            {
                winner = true;
            }
            else if ((grid[2,0] == grid[2,1]) && (grid[2,1] == grid[2,2]) && grid[2,0] != "")
            {
                winner = true;
            }

            #region HorizontalChecks
            /*if ((R1B1.Text == R1B2.Text) && (R1B2.Text == R1B3.Text) && R1B1.Text != "")
            {
                winner = true;
            }
            else if ((R2B1.Text == R2B2.Text) && (R2B2.Text == R2B3.Text) && R2B1.Text != "")
            {
                winner = true;
            }
            else if ((R3B1.Text == R3B2.Text) && (R3B2.Text == R3B3.Text) && R3B1.Text != "")
            {
                winner = true;
            }*/
            #endregion

            else if ((grid[0,0] == grid[1,0]) && (grid[1,0] == grid[2,0]) && grid[0,0] != "")
            {
                winner = true;
            }
            else if ((grid[0,1] == grid[1,1]) && (grid[1,1] == grid[2,1]) && grid[0,1] != "")
            {
                winner = true;
            }
            else if ((grid[0,2] == grid[1,2]) && (grid[1,2] == grid[2,2]) && grid[0,2] != "")
            {
                winner = true;
            }

            #region Vertical Checks
            /*else if ((R1B1.Text == R2B1.Text) && (R2B1.Text == R3B1.Text) && R1B1.Text != "")
            {
                winner = true;
            }
            else if ((R1B2.Text == R2B2.Text) && (R2B2.Text == R3B2.Text) && R1B2.Text != "")
            {
                winner = true;
            }
            else if ((R1B3.Text == R2B3.Text) && (R2B3.Text == R3B3.Text) && R1B3.Text != "")
            {
                winner = true;
            }*/
            #endregion

            else if ((grid[0,0] == grid[1,1]) && (grid[1,1] == grid[2,2]) && grid[0,0] != "")
            {
                winner = true;
            }
            else if ((grid[0,2] == grid[1,1]) && (grid[1,1] == grid[2,0]) && grid[0,2] != "")
            {
                winner = true;
            }

            #region Diagonal Checks
            /*else if((R1B1.Text == R2B2.Text) && (R2B2.Text == R3B3.Text) && R1B1.Text != "")
            {
                winner = true;
            }
            else if((R1B3.Text == R2B2.Text) && (R2B2.Text == R3B1.Text) && R1B3.Text != "")
            {
                winner = true;
            }*/
            #endregion

            if (winner)
            {
                if (!minMaxCheck)
                {
                    DisableButtons();
                }

                if(currentPlayer == PlayerType.o)
                {

                    if (!minMaxCheck)
                    {
                        oWin++;
                        OScoreLabel.Text = oWin.ToString();
                        TurnLabel.Enabled = false;
                    }
                    return WinType.O;
                }
                else
                {
                    if (!minMaxCheck)
                    {
                        xWin++;
                        XScoreLabel.Text = xWin.ToString();
                        TurnLabel.Enabled = false;
                    }
                    return WinType.X;
                }               
            }
            else
            {
                if(turnNumber == 9)
                {
                    if (!minMaxCheck)
                    {
                        TurnLabel.Enabled = false;
                    }
                    return WinType.Draw;
                }
            }

            return WinType.None;
        }

        private void EndGameMessage(WinType winType)
        {
            if (winType == WinType.O)
            {
                MessageBox.Show("Winner: O!");
            }
            else if(winType == WinType.X)
            {
                MessageBox.Show("Winner: X!");
            }
            else
            {
                MessageBox.Show("Draw!");
            }
        }

        private void DisableButtons()
        {
            //Enable and reset buttons
            for (int i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for(int j = 0; j < buttonGrid.GetLength(1); j++)
                {
                    buttonGrid[i,j].Enabled = false;
                }
            }
        }

        private PositionCoords MiniMax()
        {
            int score = 1000000;
            PositionCoords bestMove = new PositionCoords
            {
                Row = -1,
                Column = -1
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(gameGrid[i,j] == "")
                    {
                        Console.WriteLine("{0}, {1}", i, j);
                        gameGrid[i,j] = PlayerType.o.ToString();

                        int temp = MaxSearch();

                        gameGrid[i, j] = "";

                        if (temp < score)
                        {
                            score = temp;
                            bestMove.Row = i;
                            bestMove.Column = j;
                        }
                    }
                }
            }
            Console.WriteLine("TRUE BESTVAL: {0}, {1}", bestMove.Row, bestMove.Column);
            Console.WriteLine("----BREAK----");

            return bestMove;
        }

        private int MaxSearch()
        {
            WinType result = WinCheck(gameGrid, true);
            if (result == WinType.X)
            {
                return 10;
            }
            if (result == WinType.O)
            {
                return -10;
            }
            if (result == WinType.Draw)
            {
                Console.WriteLine("D");
                return 0;
            }

            int score = 1000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(gameGrid[i,j] == "")
                    {
                        gameGrid[i, j] = PlayerType.x.ToString();
                        score = Math.Max(score, MinSearch());
                        gameGrid[i, j] = "";
                    }
                }
            }

            return score;
        }

        private int MinSearch()
        {
            WinType result = WinCheck(gameGrid, true);
            if (result == WinType.X)
            {
                return 10;
            }
            if (result == WinType.O)
            {
                return -10;
            }
            if (result == WinType.Draw)
            {
                Console.WriteLine("D");
                return 0;
            }

            int score = -1000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameGrid[i, j] == "")
                    {
                        gameGrid[i, j] = PlayerType.o.ToString();
                        score = Math.Min(score, MaxSearch());
                        gameGrid[i, j] = "";
                    }
                }
            }

            return score;
        }

        #region hide
        /*private void AIBestMove()
        {
            int bestValue = -10000;
            PositionCoords bestMovePosition = new PositionCoords();
            bestMovePosition.Row = -1;
            bestMovePosition.Column = -1;

            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for(int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    if (gameGrid[i,j] == "")
                    {
                        gameGrid[i,j] = PlayerType.o.ToString();

                        int moveVal = AIMiniMax(gameGrid, 0, false);

                        

                        if (moveVal < bestValue)
                        {
                            bestValue = moveVal;
                            bestMovePosition.Row = i;
                            bestMovePosition.Column = j;
                        }

                        gameGrid[i, j] = "";
                    }
                }
            }
            
            buttonGrid[bestMovePosition.Row,bestMovePosition.Column].Text = PlayerType.x.ToString();

            SwitchPlayer();
        }*/

        /*private int AIMiniMax(string[,] board, int depth, bool isMax)
        {
            WinType result = WinCheck(board, true);
            if (result == WinType.O)
            {
                return 10;
            }
            if (result == WinType.X)
            {
                return -10;
            }
            if (result == WinType.Draw)
            {
                Console.WriteLine("D");
                return 0;
            }

            if (isMax)
            {
                int best = -1000;

                for(int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        if (gameGrid[i,j] == "")
                        {
                            gameGrid[i,j] = PlayerType.o.ToString();

                            int score = AIMiniMax(gameGrid, depth + 1, false);

                            best = Math.Max(best, /*AIMiniMax(gameGrid, depth + 1, false)*/
        //score);

                            /*gameGrid[i,j] = "";
                        }
                    }
                }
                return best;
            }
            else
            {
                int best = 1000;

                for(int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        if (gameGrid[i,j] == "")
                        {
                            gameGrid[i,j] = PlayerType.x.ToString();

                            int score = AIMiniMax(gameGrid, depth + 1, true);

                            best = Math.Min(best, /*AIMiniMax(gameGrid, depth + 1, true)score);*/

                            /*gameGrid[i,j] = "";
                        }
                    }
                }
                return best;
            }

            //Console.WriteLine("AIMiniMax Called");
            //Terminal State - ***NEED TO RETURN SCORE***
            //WinType result = WinCheck();

            /*Button[,] grid = CopyGameBoard(gameGrid);

            if(result == WinType.X)
            {
                Console.WriteLine("X");
                return 1;
            }
            if(result == WinType.O)
            {
                Console.WriteLine("O");
                return -1;
            }
            if(result == WinType.Draw)
            {
                Console.WriteLine("D");
                return 0;
            }

            if (isMax)
            {
                int bestScore = -10000;
                for (int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        if (grid[i, j].Text == "")
                        {
                            grid[i, j].Text = PlayerType.x.ToString();
                            int score = AIMiniMax(grid, depth + 1, false);
                            //copyBoard[i, j].Text = "";

                            bestScore = Math.Max(score, bestScore);
                            
                        }
                        
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = 10000;
                for (int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        if (grid[i, j].Text == "")
                        {
                            grid[i, j].Text = PlayerType.o.ToString();
                            int score = AIMiniMax(grid, depth + 1, true);
                            //gameGrid[i, j].Text = "";

                            bestScore = Math.Min(score, bestScore);
                            
                        }
                    }
                }
                return bestScore;
            }*/
        //}
        #endregion

        #endregion
    }
}
