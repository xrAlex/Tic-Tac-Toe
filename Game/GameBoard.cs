namespace Tic_Tac_Toe.Game
{
    public sealed class GameBoard
    {
        public readonly int BoardSize;
        public char[,] Board { get; private set; }
        
        public GameBoard(int boardSize)
        {
            BoardSize = boardSize;
            InitializeBoard();
        }

        /// <summary>
        /// Filling game board with buttons
        /// </summary>
        private void InitializeBoard()
        {
            Board = new char[BoardSize, BoardSize];

            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
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
        /// Checks if game is finished with win
        /// </summary>
        public bool IsWin(char playerMark)
            => WinCondition.IsColumnWin(BoardSize, Board, playerMark)
               || WinCondition.IsRowWin(BoardSize, Board, playerMark)
               || WinCondition.IsDiagonalLeftWin(BoardSize, Board, playerMark)
               || WinCondition.IsDiagonalRightWin(BoardSize, Board, playerMark);

        /// <summary>
        /// Checks if game is finished with tie
        /// </summary>
        public bool IsTie()
        {
            var marks = 0;

            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if (Board[i, j] == 'X' || Board[i, j] == 'O')
                    {
                        marks++;
                    }
                }
            }

            return marks == BoardSize * BoardSize;
        }

    }
}