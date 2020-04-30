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
    public enum MatchType
    {
        PlayerVPlayer,
        PlayerVAI
    }

    //Human is always player X
    //AI is always player O
    public enum PlayerType
    {
        x,
        o, 
    }

    public partial class TicTacToeForm : Form
    {
        class PositionCoords
        {
            public int Row = -1;
            public int Column = -1;
        }

        private PlayerType currentPlayer = PlayerType.x;

        //2D array representing UI button grid
        private Button[,] buttonGrid;
        //2D string array representing the current game state
        private string[,] gameGrid = new string[3,3];

        //Storing how many times x and o have won
        private int xWin = 0;
        private int oWin = 0;


        //Keep track of whether its a player vs player or player vs AI match
        MatchType currentMatchType;

        public TicTacToeForm()
        {
            InitializeComponent();

            //Initialise array with buttons from Form
            buttonGrid = new Button[3,3] { {R1B1, R1B2, R1B3 },
                                       { R2B1, R2B2, R2B3},
                                       { R3B1, R3B2, R3B3 } };
        }

        #region UI Methods
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Text == "")
            {
                btn.Text = currentPlayer.ToString();

                UpdateGameGrid();
                EndGameCheck();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGame();
            currentMatchType = MatchType.PlayerVPlayer;
        }

        private void playerVsAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGame();
            currentMatchType = MatchType.PlayerVAI;
        }

        private void resetScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XScoreLabel.Text = 0.ToString();
            OScoreLabel.Text = 0.ToString();
            xWin = 0;
            oWin = 0;
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Functions to check that the text in the Form Buttons matches what's in the 2D string array
        /// </summary>
        private void UpdateGameGrid()
        {
            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    if (gameGrid[i, j] != buttonGrid[i, j].Text)
                    {
                        gameGrid[i, j] = buttonGrid[i, j].Text;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Resets variables ready for new game to commence
        /// </summary>
        private void SetupGame()
        {
            for (int i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for (int j = 0; j < buttonGrid.GetLength(1); j++)
                {
                    buttonGrid[i, j].Enabled = true;
                    buttonGrid[i, j].Text = "";
                }
            }

            currentPlayer = PlayerType.x;
            TurnLabel.Text = "X";
        }

        private void SwitchPlayer()
        {
            if(currentPlayer == PlayerType.x)
            {
                currentPlayer = PlayerType.o;
                TurnLabel.Text = "o";
                if(currentMatchType == MatchType.PlayerVAI)
                {
                    BestMove();
                }                
            }
            else
            {
                currentPlayer = PlayerType.x;
                TurnLabel.Text = "x";
            }
        }

        /// <summary>
        /// Used to check if game has ended.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool WinBoolCheck(PlayerType player)
        {
            #region Horizontal + Vertical Win Checks
            for (int i = 0; i < 3; i++)
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
            #endregion

            #region Diagonal Win Checks
            if (gameGrid[0,0] == player.ToString() && gameGrid[1,1] == player.ToString() && gameGrid[2,2] == player.ToString())
            {
                return true;
            }
            if(gameGrid[0,2] == player.ToString() && gameGrid[1,1] == player.ToString() && gameGrid[2,0] == player.ToString())
            {
                return true;
            }
            #endregion

            return false;
        }

        /// <summary>
        /// Check to see if the game has ended in a tie
        /// </summary>
        /// <returns></returns>
        private bool IsTie()
        {
            //Iterate through each row, seeing if there are any spaces free
            for (int i = 0; i < 3; i++)
            {
                if (gameGrid[i, 0] == "" || gameGrid[i, 1] == "" || gameGrid[i, 2] == "")
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check to see if end game state has been reached.
        /// Changes corresponding Windows Form components respectively.
        /// </summary>
        private void EndGameCheck()
        {
            if (WinBoolCheck(currentPlayer))
            {
                EndGameMessage(currentPlayer);
                DisableButtons();
                if (currentPlayer == PlayerType.o)
                {
                    oWin++;
                    OScoreLabel.Text = oWin.ToString();
                }
                else
                {
                    xWin++;
                    XScoreLabel.Text = xWin.ToString();
                }

            }
            else if (IsTie())
            {
                DisableButtons();
                MessageBox.Show("Draw!");
            }
            else
            {
                SwitchPlayer();
            }
        }

        private void EndGameMessage(PlayerType playerType)
        {
            if(playerType == PlayerType.x)
            {
                MessageBox.Show("X Wins");
            }
            else
            {
                MessageBox.Show("O Wins!");
            }
        }

        private void DisableButtons()
        {
            for (int i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for(int j = 0; j < buttonGrid.GetLength(1); j++)
                {
                    buttonGrid[i,j].Enabled = false;
                }
            }
        }

        /// <summary>
        /// Function called to find the optimal move using MiniMax.
        /// </summary>
        private void BestMove()
        {
            //Storing the optimal move when MiniMax executed
            PositionCoords move = new PositionCoords();
            int score = 100000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameGrid[i, j] == "")
                    {
                        gameGrid[i, j] = PlayerType.o.ToString();

                        //Call Minimax function
                        int minimaxVal = MiniMaxAlphaBeta(0, true);

                        gameGrid[i, j] = "";

                        if (minimaxVal < score)
                        {
                            score = minimaxVal;
                            move.Row = i;
                            move.Column = j;
                        }
                    }
                }
            }

            //Add new position to the game board and the UI
            gameGrid[move.Row, move.Column] = PlayerType.o.ToString();
            buttonGrid[move.Row, move.Column].Text = PlayerType.o.ToString();

            //Check if there is a winner after new position added
            EndGameCheck();
        }

        /// <summary>
        /// Core recursive MiniMax function
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="isMaximising"></param>
        /// <returns></returns>
        private int MiniMax(int depth, bool isMaximising)
        {
            //Check if a player has won at the start of each function call
            if (WinBoolCheck(PlayerType.x)) { return 10; }
            else if (WinBoolCheck(PlayerType.o)) { return -10; }
            else if (IsTie()) { return 0; }

            //Maximising Player (Human) - Trying to reach the largest possible score
            if (isMaximising)
            {
                int best = -10000;

                for(int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if(gameGrid[i,j] == "")
                        {
                            gameGrid[i, j] = PlayerType.x.ToString();

                            best = Math.Max(best, MiniMax(depth + 1, false));

                            gameGrid[i, j] = "";
                        }
                    }
                }

                return best - depth;
            }
            //Minimising player (AI) - trying to reach the minimal score
            else
            {
                int best = 10000;

                for(int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if(gameGrid[i,j] == "")
                        {
                            gameGrid[i, j] = PlayerType.o.ToString();

                            best = Math.Min(best, MiniMax(depth + 1, true));

                            gameGrid[i, j] = "";
                        }
                    }
                }

                return best + depth;
            }
        }
        /// <summary>
        /// Core Minimax algorithm with Alpha Beta pruning
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="isMaximising"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        private int MiniMaxAlphaBeta(int depth, bool isMaximising, int alpha = -10000, int beta = 10000)
        {
            //Check if a player has won at the start of each function call
            if (WinBoolCheck(PlayerType.x)) { return 10; }
            else if (WinBoolCheck(PlayerType.o)) { return -10; }
            else if (IsTie()) { return 0; }

            //Maximising Player (Human) - Trying to reach the largest possible score
            if (isMaximising)
            {
                int best = -10000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (gameGrid[i, j] == "")
                        {
                            gameGrid[i, j] = PlayerType.x.ToString();

                            int recursiveCall = MiniMaxAlphaBeta(depth + 1, false, alpha, beta);

                            best = Math.Max(best, recursiveCall);

                            gameGrid[i, j] = "";

                            alpha = Math.Max(alpha, recursiveCall);

                            if(beta <= alpha)
                            {
                                break;
                            }
                        }
                    }
                }

                return best - depth;
            }
            //Minimising player (AI) - trying to reach the minimal score
            else
            {
                int best = 10000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (gameGrid[i, j] == "")
                        {
                            gameGrid[i, j] = PlayerType.o.ToString();

                            int recursiveCall = MiniMaxAlphaBeta(depth + 1, true, alpha, beta);

                            best = Math.Min(best, recursiveCall);

                            gameGrid[i, j] = "";

                            beta = Math.Min(beta, recursiveCall);

                            if(alpha >= beta)
                            {
                                break;
                            }
                        }
                    }
                }

                return best + depth;
            }
        }

        #endregion
    }
}
