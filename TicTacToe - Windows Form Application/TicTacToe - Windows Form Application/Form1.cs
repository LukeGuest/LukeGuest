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

        public TicTacToeForm()
        {
            InitializeComponent();
            buttonList = Controls.OfType<Button>().ToList();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if(currentPlayer == PlayerType.X)
            {
                btn.Text = "x";
            }
            else
            {
                btn.Text = "o";
            }
            SwitchPlayer();

            btn.Enabled = false;
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
            }
            else
            {
                currentPlayer = PlayerType.X;
            }
            Console.WriteLine("Switched Player");
        }


    }
}
