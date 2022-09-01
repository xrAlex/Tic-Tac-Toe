using System.Drawing;

namespace Tic_Tac_Toe.Game
{
    public sealed class AiPlayer
    {
        private readonly char _ai;
        private readonly char _enemy;

        public AiPlayer(char playerMark)
        {
            _ai = playerMark;
            _enemy = _ai == 'X' ? 'O' : 'X';
        }

        /// <summary>
        /// Calculates best move for AI
        /// </summary>
        public Point CalculateBestMove(GameBoard game)
        {
            double bestScore = -999999999;
            var bestMove = new Point();

            for (var i = 0; i < game.BoardSize; i++)
            {
                for (var j = 0; j < game.BoardSize; j++)
                {
                    if (game.Board[i, j] != 'X' && game.Board[i, j] != 'O')
                    {
                        game.Board[i, j] = _ai;
                        
                        var score = MiniMax(game, 1, false);
                        
                        game.Board[i, j] = '-';
                        
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove.X = i;
                            bestMove.Y = j;
                        }
                    }
                }
  
            }

            return bestMove;
        }

        /// <summary>
        /// Minimax algorithm implementation
        /// </summary>
        private double MiniMax(GameBoard gameBoard, int depth, bool maximazing)
        {
            if (gameBoard.BoardSize > 3)
            {
                if (depth > 3)
                {
                    return 0;
                }
            }
 
            var win = gameBoard.IsWin(_ai);
            var lose = gameBoard.IsWin(_enemy);
            
            if (win)
            {
                return 10 / depth;
            }
            if (lose)
            {
                return -10 / depth;
            }

            if (gameBoard.IsTie())
            {
                return 0;
            }

            if (maximazing)
            {
                double bestScoreMin = -999999999;
                for (var i = 0; i < gameBoard.BoardSize; i++)
                {
                    for (var j = 0; j < gameBoard.BoardSize; j++)
                    {
                        if (gameBoard.Board[i, j] != 'X' && gameBoard.Board[i, j] != 'O')
                        {
                            gameBoard.Board[i, j] = _ai;
                            var score = MiniMax(gameBoard, depth + 1, false);
                            gameBoard.Board[i, j] = '-';
                            
                            if (score > bestScoreMin)
                            {
                                bestScoreMin = score;
                            }
                        }
                    }
                }

                return bestScoreMin;
            }

            double bestScoreMax = 999999999;
            for (var i = 0; i < gameBoard.BoardSize; i++)
            {
                for (var j = 0; j < gameBoard.BoardSize; j++)
                {
                    if (gameBoard.Board[i, j] != 'X' && gameBoard.Board[i, j] != 'O')
                    {
                        gameBoard.Board[i, j] = _enemy;
                        var score = MiniMax(gameBoard, depth + 1, true);
                        gameBoard.Board[i, j] = '-';
                        
                        if (score < bestScoreMax)
                        {
                            bestScoreMax = score;
                        }
                    }
                }
            }

            return bestScoreMax;
        }
    }
}