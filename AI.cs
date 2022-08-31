using System;
using System.Drawing;

namespace Tic_Tac_Toe
{
    public class AI
    {
        private readonly char _ai;
        private readonly char _enemy;
        private int _weight;

        public AI(char p)
        {
            _ai = p;
            _enemy = _ai == 'X' ? 'O' : 'X';
        }

        /// <summary>
        /// Calculates best move for AI
        /// </summary>
        public Point CalculateBestMove(Game game)
        {
            double bestScore = -999999999;
            var bestMove = new Point();
            _weight = 0;
            for (var i = 0; i < game.BoardSideLight; i++)
            {
                for (var j = 0; j < game.BoardSideLight; j++)
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

            Console.WriteLine(_weight);
            return bestMove;
        }

        /// <summary>
        /// Minimax algorithm implementation
        /// </summary>
        public double MiniMax(Game g, int depth, bool maximazing)
        {
            _weight++;
            
            if (g.BoardSideLight > 3)
            {
                if (depth > 3)
                {
                    return 0;
                }
            }
 
            var win = g.IsWin(_ai);
            var lose = g.IsWin(_enemy);
            
            if (win)
            {
                return 10 / depth;
            }
            if (lose)
            {
                return -10 / depth;
            }

            if (g.IsTie())
            {
                return 0;
            }

            if (maximazing)
            {
                double bestScoreMin = -999999999;
                for (var i = 0; i < g.BoardSideLight; i++)
                {
                    for (var j = 0; j < g.BoardSideLight; j++)
                    {
                        if (g.Board[i, j] != 'X' && g.Board[i, j] != 'O')
                        {
                            g.Board[i, j] = _ai;
                            var score = MiniMax(g, depth + 1, false);
                            g.Board[i, j] = '-';
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
            for (var i = 0; i < g.BoardSideLight; i++)
            {
                for (var j = 0; j < g.BoardSideLight; j++)
                {
                    if (g.Board[i, j] != 'X' && g.Board[i, j] != 'O')
                    {
                        g.Board[i, j] = _enemy;
                        var score = MiniMax(g, depth + 1, true);
                        g.Board[i, j] = '-';
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