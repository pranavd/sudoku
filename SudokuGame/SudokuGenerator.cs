using System;
using System.Collections.Generic;
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
                var randomNumbers = GenerateRandomNumbers(difficulty);

                foreach (var number in randomNumbers)
                {
                    var splits = number.ToString().ToCharArray();

                    if (splits.Length == 2)
                    {
                        var row = int.Parse(splits[0].ToString());
                        var column = int.Parse(splits[1].ToString());

                        BaseBoard[row, column] = 0;
                    }
                    else
                    {
                        BaseBoard[0, int.Parse(splits[0].ToString())] = 0;
                    }
                }

                return BaseBoard;
            }
            throw new Exception("failed to generate...");
        }

        private IEnumerable<int> GenerateRandomNumbers(Common.Difficulty difficulty)
        {
            HashSet<int> set;

            switch (difficulty)
            {
                case Common.Difficulty.Easy:
                    set = GenerateNumberSet(50);
                    break;
                case Common.Difficulty.Medium:
                    set = GenerateNumberSet(55);
                    break;
                case Common.Difficulty.Hard:
                    set = GenerateNumberSet(60);
                    break;
                case Common.Difficulty.Samurai:
                    set = GenerateNumberSet(65);
                    break;
                default:
                    throw new Exception("difficulty not specified...");
            }

            return set.ToList();
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
    }
}
