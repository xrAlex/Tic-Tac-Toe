using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Tic_Tac_Toe.Game;


namespace Tic_Tac_Toe.UI
{
    public sealed partial class GameBoardForm : Form
    {
        private readonly bool _playVsAi;
        private readonly bool _isDemoMode;
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);
        private readonly int _boardSize;

        private Button[,] _buttons;
        private GameBoard _game;
        private char _currentTurn;

        public GameBoardForm(int boardSize, bool playVsAi, bool isDemoMode)
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
            => NewGame();

        private void GameForm_Closing(object sender, FormClosingEventArgs e)
            => Application.Exit();

        private void NewGame()
        {
            _currentTurn = 'X';

            InitUi();
            _game = new GameBoard(_boardSize);

            if (_isDemoMode)
            {
                _currentTurn = 'O';
                AiStart();
                return;
            }

            if (_playVsAi)
            {
                if (_random.Next(0, 100) > 50)
                {
                    _currentTurn = 'O';
                    AiStart();
                }
            }
        }


        /// <summary>
        /// Ai makes turn first, uses random cell for it
        /// </summary>
        private void AiStart()
        {
            var count = _buttons.Length;
            var randomRow = _random.Next(0, count / _boardSize);
            var randomCell = _random.Next(0, count / _boardSize);
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

        /// <summary>
        /// Draws a symbol on board when player press button and go to next turn
        /// </summary>
        private void BoardButtonClicked(object sender, EventArgs e)
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

                    btn.Click += BoardButtonClicked;

                    _buttons[i, j] = btn;
                }
            }
        }

        private void InitUi()
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
        }

        private void ClearGameField()
        {
            GameField.Controls.Clear();
            GameField.RowStyles.Clear();
        }

        private void PlayAgain()
        {
            var result = MessageBox
                .Show("Do you want to play again?", "Tic Tac Toe",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NewGame();
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

            _buttons[x, y].Text = _currentTurn.ToString();
            _buttons[x, y].Enabled = false;
            _buttons[x, y].Update();

            _game.MarkBoard(x, y, _currentTurn);

            if (_game.IsWin(_currentTurn))
            {
                MessageBox.Show(_currentTurn + ": Won!!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PlayAgain();
            }
            else if (_game.IsTie())
            {
                MessageBox.Show("Game is Tie!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PlayAgain();
            }
            else
            {
                NextTurn();
            }
        }

        private void NextTurn()
        {
            if (_currentTurn == 'X')
            {
                _currentTurn = 'O';

                if (_playVsAi || _isDemoMode)
                {
                    AiTurn(_currentTurn);
                }
            }
            else
            {
                _currentTurn = 'X';

                if (_isDemoMode)
                {
                    AiTurn(_currentTurn);
                }
            }
        }

        private void AiTurn(char playerMark)
        {
            var ai = new AiPlayer(playerMark);
            var move = ai.CalculateBestMove(_game);
            var x = move.X;
            var y = move.Y;
            Play(x, y);
        }
    }
}