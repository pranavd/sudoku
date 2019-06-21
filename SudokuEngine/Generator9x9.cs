using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuEngine
{
    /// <inheritdoc />
    /// <summary>
    /// 9x9 grid Sudoku generator
    /// </summary>
    public class Generator9X9 : IGenerator
    {
        /// <summary>
        /// initial board, for which valid sudoku will be generated through backtracking
        /// </summary>
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

        private readonly Common.Difficulty _difficulty;

        private int[,] _baseBoard;
        public int[,] BaseBoard => _baseBoard;

        /// <summary>
        /// generates sudoku with specified difficulty level
        /// </summary>
        /// <param name="squareDimension"></param>
        /// <param name="difficulty"></param>
        /// <returns></returns>

        public Generator9X9(Common.Difficulty difficulty)
        {
            _difficulty = difficulty;
        }

        public int[,] Generate()
        {
            var sudokuSolver = SudokuSolver.GetSolver(_zeroBoard);
            _baseBoard = sudokuSolver.Solve();

            if (_baseBoard == null) throw new Exception("failed to generate...");
            var indexes = IndexesTobeDeleted(_difficulty).ToList();

            var difficultyGenerator = new DifficultyGenerator();

            foreach (var index in indexes)
            {
                //applying difficulty levels  
                difficultyGenerator.MakeItDifficultBy(_difficulty, index, ref _baseBoard);
            }

            return _baseBoard;
        }

        /// <summary>
        /// generates random set of indexes, which will be used to delete items from sudoku
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        private IEnumerable<int[]> IndexesTobeDeleted(Common.Difficulty difficulty)
        {
            HashSet<int> set;
            var indexes = new List<int[]>();

            switch (difficulty)
            {
                case Common.Difficulty.Easy:
                    set = GenerateNumberSet(55 / 3);
                    break;
                case Common.Difficulty.Medium:
                    set = GenerateNumberSet(60 / 3);
                    break;
                case Common.Difficulty.Hard:
                    set = GenerateNumberSet(65 / 2);
                    break;
                case Common.Difficulty.Samurai:
                    set = GenerateNumberSet(75 / 2);
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
    }
}
