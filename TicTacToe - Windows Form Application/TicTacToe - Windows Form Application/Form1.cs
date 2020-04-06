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
        X,
        O
    }

    public partial class TicTacToeForm : Form
    {
        private PlayerType currentPlayer = PlayerType.X;

        //2D array representing game grid
        private Button[,] gameGrid;

        private int turnNumber;

        private int xWin = 0;
        private int oWin = 0;

        public TicTacToeForm()
        {
            InitializeComponent();

            //Initialise array with buttons from Form
            gameGrid = new Button[3,3] { {R1B1, R1B2, R1B3 },
                                         {R2B1, R2B2, R2B3 },
                                         {R3B1, R3B2, R3B3 }};

        }

        private void Button_Click(object sender, EventArgs e)
        {
            turnNumber++;
            Button btn = (Button)sender;

            if (btn.Text == "")
            {
                if (currentPlayer == PlayerType.X)
                {
                    btn.Text = "x";
                }
                else
                {
                    btn.Text = "o";
                }
                WinCheck();
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
            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for(int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    gameGrid[i,j].Enabled = true;
                    gameGrid[i,j].Text = "";
                }
            }

            currentPlayer = PlayerType.X;
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
            if(currentPlayer == PlayerType.X)
            {
                currentPlayer = PlayerType.O;
                TurnLabel.Text = "O";
                AIMinMax();
            }
            else
            {
                currentPlayer = PlayerType.X;
                TurnLabel.Text = "X";
            }
        }

        private void WinCheck()
        {
            bool winner = false;

            #region HorizontalChecks
            if((R1B1.Text == R1B2.Text) && (R1B2.Text == R1B3.Text) && R1B1.Text != "")
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
            }
            #endregion

            #region Vertical Checks
            else if ((R1B1.Text == R2B1.Text) && (R2B1.Text == R3B1.Text) && R1B1.Text != "")
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
            }
            #endregion

            #region Diagonal Checks
            else if((R1B1.Text == R2B2.Text) && (R2B2.Text == R3B3.Text) && R1B1.Text != "")
            {
                winner = true;
            }
            else if((R1B3.Text == R2B2.Text) && (R2B2.Text == R3B1.Text) && R3B1.Text != "")
            {
                winner = true;
            }
            #endregion

            if (winner)
            {
                DisableButtons();
                String winText = "";

                if(currentPlayer == PlayerType.O)
                {
                    winText = "O";
                    oWin++;
                    OScoreLabel.Text = oWin.ToString();
                }
                else
                {
                    winText = "X";
                    xWin++;
                    XScoreLabel.Text = xWin.ToString();
                }
                MessageBox.Show("Winner: " + winText);
                TurnLabel.Enabled = false;
            }
            else
            {
                if(turnNumber == 9)
                {
                    MessageBox.Show("Draw!");
                    TurnLabel.Enabled = false;
                }
            }
        }

        private void DisableButtons()
        {
            //Enable and reset buttons
            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    gameGrid[i, j].Enabled = false;
                }
            }
        }

        private void AIMinMax()
        {

        }
        #endregion
    }
}
