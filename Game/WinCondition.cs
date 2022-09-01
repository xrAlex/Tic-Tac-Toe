namespace Tic_Tac_Toe.Game
{
    internal static class WinCondition
    {
        public static bool IsRowWin(int boardSize, char[,] board, char playerMark)
        {
            for (var i = 0; i < boardSize; i++)
            {
                var total = 0;
                for (var j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == playerMark)
                    {
                        total++;
                    }
                }

                if (total == boardSize)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsColumnWin(int boardSize, char[,] board, char playerMark)
        {
            for (var j = 0; j < boardSize; j++)
            {
                var total = 0;
                for (var i = 0; i < boardSize; i++)
                {
                    if (board[i, j] == playerMark)
                    {
                        total++;
                    }
                }

                if (total == boardSize)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDiagonalLeftWin(int boardSize, char[,] board, char playerMark)
        {
            var total = 0;

            for (var i = 0; i < boardSize; i++)
            {
                if (board[i, i] == playerMark)
                {
                    total++;
                }
            }

            return total == boardSize;
        }

        public static bool IsDiagonalRightWin(int boardSize, char[,] board, char playerMark)
        {
            var total = 0;

            for (var i = 0; i < boardSize; i++)
            {
                if (board[i, boardSize - i - 1] == playerMark)
                {
                    total++;
                }
            }

            return total == boardSize;
        }
    }
}
