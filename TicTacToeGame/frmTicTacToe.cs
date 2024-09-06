using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeGame.Properties;

namespace TicTacToeGame
{
    public partial class frmTicTacToe : Form
    {
        enum enPlayer { Player1, Player2 }
        enum enWinner { Player1, Player2, Draw, GameInProgress }
        enPlayer PlayerTurn = enPlayer.Player1;
        stGameStatus GameStatus;
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        public frmTicTacToe()
        {
            InitializeComponent();
        }

        void _highlightWinningButtons(Button btn1, Button btn2, Button btn3)
        {
            btn1.BackColor = Color.GreenYellow;
            btn2.BackColor = Color.GreenYellow;
            btn3.BackColor = Color.GreenYellow;
        }
        private bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag == btn2.Tag && btn2.Tag == btn3.Tag)
            {
                _highlightWinningButtons(btn1, btn2, btn3);
                GameStatus.Winner = (btn1.Tag.ToString() == "X") ? enWinner.Player1 : enWinner.Player2;
                GameStatus.GameOver = true;
                EndGame();

                return true;
            }


            return false;
        }

        private void CheckWinner()
        {

            //Horizontal Wins
            if (CheckValues(button1, button2, button3) ||
                CheckValues(button4, button5, button6) ||
                CheckValues(button7, button8, button9))
                return;


            //Vertical Wins
            if (CheckValues(button1, button4, button7) ||
                CheckValues(button2, button5, button8) ||
                CheckValues(button3, button6, button9))
                return;

            //Diagonal Wins
            if (CheckValues(button1, button5, button9) ||
                CheckValues(button7, button5, button3))
                return;


        }
        private void EndGame()
        {
            lblTurn.Text = "GameOver";

            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {

                btn.Image = (PlayerTurn == enPlayer.Player1) ? Resources.X : Resources.O;
                btn.Tag = (PlayerTurn == enPlayer.Player1) ? "X" : "O";
                lblTurn.Text = (PlayerTurn == enPlayer.Player1) ? "Player 2" : "Player 1";
                PlayerTurn = (PlayerTurn == enPlayer.Player1) ? enPlayer.Player2 : enPlayer.Player1;
                GameStatus.PlayCount++;
                CheckWinner();
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrone", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9 && !GameStatus.GameOver)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }
        }
        private void ResetButton(Button btn)
        {
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
            btn.BackColor = Color.Transparent;
        }
        private void RestartGame()
        {

            foreach (var button in new[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 })
            { 
                ResetButton(button); 
            }

            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";

            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 15)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);
        }
        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}

