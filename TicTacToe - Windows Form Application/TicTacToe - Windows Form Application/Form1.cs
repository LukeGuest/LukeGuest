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
        o,
        None
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
            public int Row = -1;
            public int Column = -1;
        }

        private PlayerType currentPlayer = PlayerType.x;

        //2D array representing game grid
        private Button[,] buttonGrid;
        private string[,] gameGrid = new string[3,3];

        private int turnNumber;

        private int xWin = 0;
        private int oWin = 0;

        PositionCoords move = new PositionCoords();

        public TicTacToeForm()
        {
            InitializeComponent();

            //Initialise array with buttons from Form
            buttonGrid = new Button[3,3] { {R1B1, R1B2, R1B3 },
                                       { R2B1, R2B2, R2B3},
                                       { R3B1, R3B2, R3B3 } };
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
                MiniMax();
                //gameGrid[move.Row, move.Column] = currentPlayer.ToString();
                SwitchPlayer();
            }
            else
            {
                currentPlayer = PlayerType.x;
                TurnLabel.Text = "x";
            }
        }

        private bool WinBoolCheck(PlayerType player)
        {
            for(int i = 0; i < 3; i++)
            {
                if(gameGrid[i,0] == player.ToString() && gameGrid[i,1] == player.ToString() && gameGrid[i,2] == player.ToString())
                {
                    return true;
                }

                if(gameGrid[0,i] == player.ToString() && gameGrid[1,i] == player.ToString() && gameGrid[2,i] == player.ToString())
                {
                    return true;
                }
            }

            if(gameGrid[0,0] == player.ToString() && gameGrid[1,1] == player.ToString() && gameGrid[2,2] == player.ToString())
            {
                return true;
            }
            if(gameGrid[0,2] == player.ToString() && gameGrid[1,1] == player.ToString() && gameGrid[2,0] == player.ToString())
            {
                return true;
            }

            return false;
        }

        private bool IsTie()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameGrid[i, 0] == "" || gameGrid[i, 1] == "" || gameGrid[i, 2] == "")
                    return false;
            }
            return true;
            /*if (turnNumber == 9)
            {
                return true;
            }
            return false;*/
        }

        private WinType WinCheck(string[,] grid, bool minMaxCheck)
        {
            bool winner = false;

            #region HorizontalChecks
            if ((grid[0, 0] == grid[0, 1]) && (grid[0, 1] == grid[0, 2]) && grid[0, 0] != "")
            {
                winner = true;
            }
            else if ((grid[1, 0] == grid[1, 1]) && (grid[1, 1] == grid[1, 2]) && grid[1, 0] != "")
            {
                winner = true;
            }
            else if ((grid[2, 0] == grid[2, 1]) && (grid[2, 1] == grid[2, 2]) && grid[2, 0] != "")
            {
                winner = true;
            }
            #endregion

            #region Vertical Checks
            else if ((grid[0, 0] == grid[1, 0]) && (grid[1, 0] == grid[2, 0]) && grid[0, 0] != "")
            {
                winner = true;
            }
            else if ((grid[0, 1] == grid[1, 1]) && (grid[1, 1] == grid[2, 1]) && grid[0, 1] != "")
            {
                winner = true;
            }
            else if ((grid[0, 2] == grid[1, 2]) && (grid[1, 2] == grid[2, 2]) && grid[0, 2] != "")
            {
                winner = true;
            }
            #endregion

            #region Diagonal Checks
            else if ((grid[0, 0] == grid[1, 1]) && (grid[1, 1] == grid[2, 2]) && grid[0, 0] != "")
            {
                winner = true;
            }
            else if ((grid[0, 2] == grid[1, 1]) && (grid[1, 1] == grid[2, 0]) && grid[0, 2] != "")
            {
                winner = true;
            }
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

        private /*PositionCoords*/void MiniMax()
        {
            int score = 1000000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(gameGrid[i,j] == "")
                    {
                        gameGrid[i,j] = PlayerType.o.ToString();

                        int temp = MaxSearch();

                        if (temp < score)
                        {
                            score = temp;
                            move.Row = i;
                            move.Column = j;
                            Console.WriteLine("{0},{1}", move.Row, move.Column);
                        }

                        gameGrid[i, j] = "";
                    }
                }
            }
            Console.WriteLine(move.Column + " " + move.Row);

            PrintGameGrid();
            //return move;
            gameGrid[move.Row, move.Column] = PlayerType.o.ToString();
            buttonGrid[move.Row, move.Column].Text = PlayerType.o.ToString();
        }

        private int MaxSearch()
        {
            int score = -10000;

            if (WinBoolCheck(PlayerType.x)){ return 10 ; }
            else if(WinBoolCheck(PlayerType.o)){ return -10; }
            else if (IsTie()) {return 0;}

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
            int score = 10000;

            if (WinBoolCheck(PlayerType.x)) { return 10; }
            else if (WinBoolCheck(PlayerType.o)) { return -10; }
            else if (IsTie()) { return 0; }

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

        private void PrintGameGrid()
        {
            Console.WriteLine("+------------+");
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(gameGrid[i,j] + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("+------------+");
        }

        #endregion
    }
}
