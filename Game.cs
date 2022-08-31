namespace Tic_Tac_Toe
{
    public class Game
    {
        public readonly int BoardSideLight;
        public char[,] Board { get; private set; }
        
        public Game(int boardSize)
        {
            BoardSideLight = boardSize;
            InitializeBoard();
        }

        /// <summary>
        /// Filling game board with buttons
        /// </summary>
        private void InitializeBoard()
        {
            Board = new char[BoardSideLight, BoardSideLight];

            for (var i = 0; i < BoardSideLight; i++)
            {
                for (var j = 0; j < BoardSideLight; j++)
                {
                    Board[i, j] = '-';
                }
            }
        }

        /// <summary>
        /// Drawing a symbol on game board
        /// </summary>
        public void MarkBoard(int i, int j, char turn)
        {
            Board[i, j] = turn;
        }

        /// <summary>
        /// Checks if game is finished with tie
        /// </summary>
        public bool IsTie()
        {
            var marks = 0;
            for (var i = 0; i < BoardSideLight; i++)
            {
                for (var j = 0; j < BoardSideLight; j++)
                {
                    if (Board[i, j] == 'X' || Board[i, j] == 'O')
                    {
                        marks++;
                    }
                }
            }

            return marks == BoardSideLight * BoardSideLight;
        }

        /// <summary>
        /// Checks if game is finished with win
        /// </summary>
        public bool IsWin(char player)
            => IsColumnWin(player)
               || IsRowWin(player)
               || IsDiagonalWin(player)
               || IsHorizontalWin(player);

        private bool IsRowWin(char player)
        {
            for (var i = 0; i < BoardSideLight; i++)
            {
                var total = 0;
                for (var j = 0; j < BoardSideLight; j++)
                {
                    if (Board[i, j] == player)
                    {
                        total++;
                    }
                }

                if (total == BoardSideLight) return true;
            }
            
            return false;
        }

        private bool IsColumnWin(char player)
        {
            for (var j = 0; j < BoardSideLight; j++)
            {
                var total = 0;
                for (var i = 0; i < BoardSideLight; i++)
                {
                    if (Board[i, j] == player)
                    {
                        total++;
                    }
                }

                if (total == BoardSideLight) return true;
            }

            return false;
        }

        private bool IsDiagonalWin(char player)
        {
            var total = 0;
            
            for (var i = 0; i < BoardSideLight; i++)
            {
                if (Board[i, i] == player)
                {
                    total++;
                }
            }

            return total == BoardSideLight;
        }

        private bool IsHorizontalWin(char player)
        {
            var total = 0;
            for (var i = 0; i < BoardSideLight; i++)
            {
                if (Board[i, BoardSideLight - i - 1] == player)
                {
                    total++;
                }
            }

            return total == BoardSideLight;
        }
    }
}