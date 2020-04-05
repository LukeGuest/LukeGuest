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

        private List<Button> buttonList;

        private int turnNumber;

        private int xWin = 0;
        private int oWin = 0;

        public TicTacToeForm()
        {
            InitializeComponent();
            buttonList = Controls.OfType<Button>().ToList();
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

        private void SwitchPlayer()
        {
            if(currentPlayer == PlayerType.X)
            {
                currentPlayer = PlayerType.O;
                TurnLabel.Text = "O";
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
            }
            else
            {
                if(turnNumber == 9)
                {
                    MessageBox.Show("Draw!");
                }
            }
        }

        private void DisableButtons()
        {
            foreach (Button btn in buttonList)
            {
                btn.Enabled = false;
            }
        }
    }
}
