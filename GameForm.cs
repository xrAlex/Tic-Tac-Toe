using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public sealed partial class GameForm : Form
    {
        private readonly bool _playVsAi;
        private readonly bool _isDemoMode;
        private Button[,] _buttons;
        private Game _game;
        private readonly int _boardSize;
        private char _turn;

        public GameForm(int boardSize, bool playVsAi, bool isDemoMode)
        {
            InitializeComponent();

            _boardSize = boardSize;
            _playVsAi = playVsAi;
            _isDemoMode = isDemoMode;
        }

        /// <summary>
        /// Game starts only when game window is shown
        /// </summary>
        private void GameForm_Shown(object sender, EventArgs e)
        {
            NewGame();

            if (_isDemoMode)
            {
                AiStart();
            }
        }

        /// <summary>
        /// Ai makes turn first
        /// </summary>
        private void AiStart()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var count = _buttons.Length;
            var randomRow = random.Next(0, count / _boardSize);
            var randomCell = random.Next(0, count / _boardSize);
            var btn = _buttons[randomRow, randomCell];
            btn.Enabled = false;

            var x = 0;
            var y = 0;

            for (var i = 0; i < _boardSize; i++)
            {
                for (var j = 0; j < _boardSize; j++)
                {
                    if (_buttons[i, j] == btn)
                    {
                        x = i;
                        y = j;
                        break;
                    }
                }
            }

            Play(x, y);
        }

        private void Game_Form_FormClosing(object sender, FormClosingEventArgs e)
            => Application.Exit();

        /// <summary>
        /// Draws a symbol when Player pressing button and go to next turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameButtonClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Enabled = false;

            var x = 0;
            var y = 0;

            for (var i = 0; i < _boardSize; i++)
            {
                for (var j = 0; j < _boardSize; j++)
                {
                    if (_buttons[i, j] == btn)
                    {
                        x = i;
                        y = j;
                        break;
                    }
                }
            }

            Play(x, y);
        }

        /// <summary>
        /// Filling a game field with buttons
        /// </summary>
        private void CreateButtons(float cellSize)
        {
            _buttons = new Button[_boardSize, _boardSize];

            for (var i = 0; i < _boardSize; i++)
            {
                for (var j = 0; j < _boardSize; j++)
                {
                    var btn = new Button
                    {
                        Font = new Font("Times New Roman", 24, FontStyle.Bold),
                        Text = "-",
                        Width = (int)cellSize * 10,
                        Height = (int)cellSize * 10,
                        TabStop = false
                    };

                    btn.Click += GameButtonClicked;

                    _buttons[i, j] = btn;
                }
            }
        }

        private void ClearGameField()
        {
            GameField.Controls.Clear();
            GameField.RowStyles.Clear();
        }

        private void NewGame()
        {
            float cellSize = 100 / _boardSize;

            ClearGameField();
            CreateButtons(cellSize);

            GameField.RowCount = _boardSize;
            GameField.ColumnCount = _boardSize;

            GameField.RowStyles.Clear();
            GameField.ColumnStyles.Clear();

            for (var i = 0; i < _boardSize; i++)
            {
                for (var j = 0; j < _boardSize; j++)
                {
                    GameField.RowStyles.Add(new RowStyle(SizeType.Percent, cellSize));
                    GameField.Controls.Add(_buttons[i, j], j, i);
                }

                GameField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, cellSize));
            }

            GameField.Update();

            _game = new Game(_boardSize);
            _turn = 'X';
        }

        private void PlayAgain()
        {
            var result = MessageBox
                .Show("Do you want to play again?", "Tic Tac Toe",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NewGame();

                if (_isDemoMode)
                {
                    AiStart();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void Play(int x, int y)
        {
            if (_isDemoMode)
            {
                Thread.Sleep(1000);
            }

            _buttons[x, y].Text = _turn.ToString();
            _buttons[x, y].Enabled = false;
            _buttons[x, y].Update();

            _game.MarkBoard(x, y, _turn);

            if (_game.IsWin(_turn))
            {
                MessageBox.Show(_turn + ": Won!!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PlayAgain();
            }
            else if (_game.IsTie())
            {
                MessageBox.Show("Game is Tie!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PlayAgain();
            }
            else
            {
                ChangeTurn();
            }
        }

        private void ChangeTurn()
        {
            if (_turn == 'X')
            {
                _turn = 'O';

                if (_playVsAi || _isDemoMode)
                {
                    AiTurn('O');
                }
            }
            else
            {
                _turn = 'X';

                if (_isDemoMode)
                {
                    AiTurn('X');
                }
            }
        }

        private void AiTurn(char p)
        {
            var ai = new AI(p);
            var move = ai.CalculateBestMove(_game);
            var x = move.X;
            var y = move.Y;
            Play(x, y);
        }
    }
}