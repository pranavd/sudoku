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
        private int[,] zeroBoard = new int[,]
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

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard,
            Samurai
        }

        public int[,] Generate(Difficulty difficulty)
        {
            var sudokuSolver = new SudokuSolver();
            var solvedBoard = sudokuSolver.SolveSudoku(zeroBoard);

            if (solvedBoard != null)
            {
                var randomNumbers = GenerateRandomNumbers(difficulty);

                foreach (var number in randomNumbers)
                {
                    var splits = number.ToString().ToCharArray();

                    if (splits.Length == 2)
                    {
                        var row = int.Parse(splits[0].ToString());
                        var column = int.Parse(splits[1].ToString());

                        solvedBoard[row, column] = 0;
                    }
                    else
                    {
                        solvedBoard[0, int.Parse(splits[0].ToString())] = 0;
                    }
                }

                return solvedBoard;
            }
            throw new Exception("failed to generate...");
        }

        private List<int> GenerateRandomNumbers(Difficulty difficulty)
        {
            HashSet<int> set;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    set = AddToSet(50);
                    break;
                case Difficulty.Medium:
                    set = AddToSet(55);
                    break;
                case Difficulty.Hard:
                    set = AddToSet(60);
                    break;
                case Difficulty.Samurai:
                    set = AddToSet(65);
                    break;
                default:
                    throw new Exception("difficulty not specified...");
            }

            return set.ToList();
        }

        private HashSet<int> AddToSet(int setCount)
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
