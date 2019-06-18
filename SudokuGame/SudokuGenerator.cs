﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    public class SudokuGenerator
    {
        private readonly int[,] _zeroBoard = new int[,]
        {
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 }
        };

        /// <summary>
        /// Represents desired solution
        /// </summary>
        public int[,] BaseBoard;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public int[,] Generate(Common.Difficulty difficulty)
        {
            var sudokuSolver = new SudokuSolver();
            BaseBoard = sudokuSolver.SolveSudoku(_zeroBoard);

            if (BaseBoard != null)
            {
                var indexes = IndexesTobeDeleted(difficulty);
                ApplyDifficultyLevel(difficulty, indexes, ref BaseBoard);

                return BaseBoard;
            }
            throw new Exception("failed to generate...");
        }

        private IEnumerable<int[]> IndexesTobeDeleted(Common.Difficulty difficulty)
        {
            HashSet<int> set;
            var indexes = new List<int[]>();

            switch (difficulty)
            {
                case Common.Difficulty.Easy:
                    set = GenerateNumberSet(47 / 3);
                    break;
                case Common.Difficulty.Medium:
                    set = GenerateNumberSet(50 / 3);
                    break;
                case Common.Difficulty.Hard:
                    set = GenerateNumberSet(53 / 2);
                    break;
                case Common.Difficulty.Samurai:
                    set = GenerateNumberSet(57 / 2);
                    break;
                default:
                    throw new Exception("difficulty not specified...");
            }

            foreach (var setItem in set)
            {
                var splits = setItem.ToString().ToCharArray();

                if (splits.Length == 2)
                {
                    var rIndex = int.Parse(splits[0].ToString());
                    var cIndex = int.Parse(splits[1].ToString());
                    indexes.Add(new[] { rIndex, cIndex });
                }
                else
                {
                    indexes.Add(new[] { 0, int.Parse(splits[0].ToString()) });
                }
            }

            return indexes;
        }

        private HashSet<int> GenerateNumberSet(int setCount)
        {
            var set = new HashSet<int>();

            while (set.Count < setCount)
            {
                var random = new Random();
                var number = random.Next(1, 81);
                var splits = number.ToString().ToCharArray();
                if (splits.Length == 2)
                {
                    if (int.Parse(splits[0].ToString()) < 9 && int.Parse(splits[1].ToString()) < 9)
                    {
                        set.Add(number);
                    }
                }
                else
                {
                    if (int.Parse(splits[0].ToString()) < 9)
                    {
                        set.Add(number);
                    }
                }
            }

            return set;
        }

        private void ApplyDifficultyLevel(Common.Difficulty difficulty, IEnumerable<int[]> indexes, ref int[,] board)
        {
            if (difficulty == Common.Difficulty.Easy || difficulty == Common.Difficulty.Medium)
            {
                foreach (var index in indexes)
                {
                    ExecuteDeletionRule(new[] { Common.DeletionRules.RowRule, Common.DeletionRules.ColumnRule, Common.DeletionRules.InvertedRowRule }, index, ref board);
                }
            }
            else
            {
                foreach (var index in indexes)
                {
                    ExecuteDeletionRule(new[] { Common.DeletionRules.InvertedRowRule }, index, ref board);
                }
            }
        }

        private void ExecuteDeletionRule(Common.DeletionRules[] rules, int[] index, ref int[,] board)
        {
            board[index[0], index[1]] = 0;
            foreach (var rule in rules)
            {
                switch (rule)
                {
                    case Common.DeletionRules.RowRule:
                        board[index[0], 8 - index[1]] = 0;
                        Debug.WriteLine($"({index[0]}, {index[1]}) --> ({index[0]}, {8 - index[1]})");
                        break;
                    case Common.DeletionRules.ColumnRule:
                        board[8 - index[0], index[1]] = 0;
                        break;
                    case Common.DeletionRules.InvertedRowRule:
                        board[8 - index[0], 8 - index[1]] = 0;
                        break;
                    default:
                        throw new Exception($"Failed to apply rule {rule.ToString()}");
                }
            }
        }
    }
}
