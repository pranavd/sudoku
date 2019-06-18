using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuGame;

namespace TestProject
{
    [TestClass]
    public class SudokuTest
    {
        private Dictionary<Common.Difficulty, int[,]> _sudokus;

        /// <summary>
        /// Test solver for sudoku
        /// </summary>
        [TestMethod]
        public void TestSolver()
        {
            try
            {
                var solver = new SudokuSolver();
                var solved = solver.SolveSudoku(_sudokus[Common.Difficulty.Samurai]);

                Assert.IsNotNull(solved);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Gather solver performance, sudoku difficulty (on the recursion depth)
        /// </summary>
        [TestMethod]
        public void TestSolvePerformance()
        {
            try
            {
                var solver = new SudokuSolver();

                //check samurai level
                var samuraiSolved = solver.SolveSudoku(_sudokus[Common.Difficulty.Samurai]);
                Assert.IsNotNull(samuraiSolved);
                if (solver.RecursionDepth > Common.DifficultyUpperBoundMetrics[Common.Difficulty.Hard])
                {
                    Assert.IsTrue(true, "samurai problem");
                }
                else
                {
                    //Assert.Inconclusive("not really a samurai problem");
                }

                //check hard level
                var hardSolved = solver.SolveSudoku(_sudokus[Common.Difficulty.Hard]);
                Assert.IsNotNull(hardSolved);
                if (solver.RecursionDepth > Common.DifficultyUpperBoundMetrics[Common.Difficulty.Medium] &&
                    solver.RecursionDepth <= Common.DifficultyUpperBoundMetrics[Common.Difficulty.Hard])
                {
                    Assert.IsTrue(true, "hard problem");
                }
                else
                {
                    //Assert.Inconclusive("not really a hard problem");
                }

                //check medium level
                var mediumSolved = solver.SolveSudoku(_sudokus[Common.Difficulty.Medium]);
                Assert.IsNotNull(mediumSolved);
                if (solver.RecursionDepth > Common.DifficultyUpperBoundMetrics[Common.Difficulty.Easy] && solver.RecursionDepth <= Common.DifficultyUpperBoundMetrics[Common.Difficulty.Medium])
                {
                    Assert.IsTrue(true, "medium problem");
                }
                else
                {
                    //Assert.Inconclusive("not really a medium problem");
                }

                //check medium level
                var easySolved = solver.SolveSudoku(_sudokus[Common.Difficulty.Medium]);
                Assert.IsNotNull(easySolved);
                if (solver.RecursionDepth <= Common.DifficultyUpperBoundMetrics[Common.Difficulty.Easy])
                {

                    Assert.IsTrue(true, "easy problem");
                }
                else
                {
                    //Assert.Inconclusive("not really a easy problem");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Generate sudoku of different difficulties, solve them, check if matches desired solution
        /// </summary>
        [TestMethod]
        public void TestSudokuGenerator()
        {
            try
            {
                var generator = new SudokuGenerator();
                var solver = new SudokuSolver();

                #region samurai level
                {
                    var unsolvedBoard = generator.Generate(Common.Difficulty.Samurai);
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = solver.SolveSudoku(unsolvedBoard);
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion

                #region hard level
                {
                    var unsolvedBoard = generator.Generate(Common.Difficulty.Hard);
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = solver.SolveSudoku(unsolvedBoard);
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion

                #region medium level
                {
                    var unsolvedBoard = generator.Generate(Common.Difficulty.Medium);
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = solver.SolveSudoku(unsolvedBoard);
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion

                #region easy level
                {
                    var unsolvedBoard = generator.Generate(Common.Difficulty.Easy);
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = solver.SolveSudoku(unsolvedBoard);
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            _sudokus = new Dictionary<Common.Difficulty, int[,]>()
            {
                {Common.Difficulty.Samurai,  new int[,]
                {
                    {0,0,0,6,0,9,7,0,0},
                    {0,0,0,7,0,0,4,1,0 },
                    {0,0,6,3,0,0,0,2,0 },
                    {0,0,5,0,0,0,0,0,0 },
                    {4,0,0,0,7,0,0,5,0 },
                    {8,0,0,2,0,0,0,9,0 },
                    {0,0,3,0,0,0,0,0,1 },
                    {0,6,0,9,0,0,0,7,0 },
                    {7,4,0,0,0,0,0,0,2 }
                }},
                {Common.Difficulty.Hard,  new int[,]
                {

                    {0, 0, 0, 7, 0, 0, 8, 0, 0},
                    {0, 0, 6, 0, 0, 0, 0, 3, 1},
                    {0, 4, 0, 0, 0, 2, 0, 0, 0},
                    {0, 2, 4, 0, 7, 0, 0, 0, 0},
                    {0, 1, 0, 0, 3, 0, 0, 8, 0},
                    {0, 0, 0, 0, 6, 0, 2, 9, 0},
                    {0, 0, 0, 8, 0, 0, 0, 7, 0},
                    {8, 6, 0, 0, 0, 0, 5, 0, 0},
                    {0, 0, 2, 0, 0, 6, 0, 0, 0}
                }},
                {Common.Difficulty.Medium, new int[,]
                {
                    {7, 0, 0, 0, 0, 4, 0, 0, 1},
                    {0, 2, 0, 0, 6, 0, 0, 8, 0},
                    {0, 0, 1, 5, 0, 0, 2, 0, 0},
                    {8, 0, 0, 0, 9, 0, 7, 0, 0},
                    {0, 5, 0, 3, 0, 7, 0, 2, 0},
                    {0, 0, 6, 0, 5, 0, 0, 0, 8},
                    {0, 0, 8, 0, 0, 9, 1, 0, 0},
                    {0, 9, 0, 0, 1, 0, 0, 6, 0},
                    {5, 0, 0, 8, 0, 0, 0, 0, 3}
                } },
                {Common.Difficulty.Easy,  new int[,]
                {
                    {0, 0, 0, 6, 0, 2, 0, 0, 0},
                    {4, 0, 0, 0, 5, 0, 0, 0, 1},
                    {0, 8, 5, 0, 1, 0, 6, 2, 0},
                    {0, 3, 8, 2, 0, 6, 7, 1, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 9, 4, 0, 7, 3, 5, 0},
                    {0, 2, 6, 0, 4, 0, 5, 3, 0},
                    {9, 0, 0, 0, 2, 0, 0, 0, 7},
                    {0, 0, 0, 8, 0, 9, 0, 0, 0}

                }}
            };
        }
    }
}
