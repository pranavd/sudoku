using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    /// <summary>
    /// implements difficulty levels
    /// </summary>
    public class DifficultyGenerator
    {
        private enum DeletionRules
        {
            RowRule,
            ColumnRule,
            DiagonalRule,
        }

        /// <summary>
        /// applies deletions rules on the basis of difficulty level
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="index"></param>
        /// <param name="board"></param>
        public void MakeItDifficultBy(Common.Difficulty difficulty, int[] index, ref int[,] board)
        {
            if (difficulty == Common.Difficulty.Easy || difficulty == Common.Difficulty.Medium)
            {
                ExecuteRules(new[] { DeletionRules.RowRule, DeletionRules.ColumnRule, DeletionRules.DiagonalRule }, index, ref board);
            }
            else
            {
                ExecuteRules(new[] { DeletionRules.DiagonalRule }, index, ref board);
            }
        }

        /// <summary>
        /// deletes values on the basis of rules provided
        /// </summary>
        /// <param name="deletionRules"></param>
        /// <param name="index"></param>
        /// <param name="board"></param>
        private void ExecuteRules(IEnumerable<DeletionRules> deletionRules, int[] index, ref int[,] board)
        {
            board[index[0], index[1]] = 0;
            foreach (var rule in deletionRules)
            {
                switch (rule)
                {
                    case DeletionRules.RowRule:
                        board[index[0], 8 - index[1]] = 0;
                        Debug.WriteLine($"({index[0]}, {index[1]}) --> ({index[0]}, {8 - index[1]})");
                        break;
                    case DeletionRules.ColumnRule:
                        board[8 - index[0], index[1]] = 0;
                        break;
                    case DeletionRules.DiagonalRule:
                        board[8 - index[0], 8 - index[1]] = 0;
                        break;
                    default:
                        throw new Exception($"Failed to apply rule {rule.ToString()}");
                }
            }
        }
    }
}
