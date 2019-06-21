using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuEngine;

namespace TestProject
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class Sudoku9X9Test
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
                var solver = SudokuSolver.GetSolver(_sudokus[Common.Difficulty.Samurai]);
                var solved = solver.Solve();

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
                //check samurai level
                var samuraiSolved = SudokuSolver.GetSolver(_sudokus[Common.Difficulty.Samurai]).Solve();
                Assert.IsNotNull(samuraiSolved);

                //check hard level
                var hardSolved = SudokuSolver.GetSolver(_sudokus[Common.Difficulty.Hard]).Solve();
                Assert.IsNotNull(hardSolved);

                //check medium level
                var mediumSolved = SudokuSolver.GetSolver(_sudokus[Common.Difficulty.Medium]).Solve();
                Assert.IsNotNull(mediumSolved);

                //check easy level
                var easySolved = SudokuSolver.GetSolver(_sudokus[Common.Difficulty.Easy]).Solve();
                Assert.IsNotNull(easySolved);

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
                #region samurai level
                {
                    var generator = SudokuGenerator.GetGenerator(9, Common.Difficulty.Samurai);
                    var unsolvedBoard = generator.Generate();
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = SudokuSolver.GetSolver(unsolvedBoard).Solve();
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion

                #region hard level
                {
                    var generator = SudokuGenerator.GetGenerator(9, Common.Difficulty.Hard);
                    var unsolvedBoard = generator.Generate();
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = SudokuSolver.GetSolver(unsolvedBoard).Solve();
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion

                #region medium level
                {
                    var generator = SudokuGenerator.GetGenerator(9, Common.Difficulty.Medium);
                    var unsolvedBoard = generator.Generate();
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = SudokuSolver.GetSolver(unsolvedBoard).Solve();
                    Assert.AreEqual(generator.BaseBoard, solvedBoard);
                }
                #endregion

                #region easy level
                {
                    var generator = SudokuGenerator.GetGenerator(9, Common.Difficulty.Easy);
                    var unsolvedBoard = generator.Generate();
                    Assert.IsNotNull(unsolvedBoard);
                    var solvedBoard = SudokuSolver.GetSolver(unsolvedBoard).Solve();
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
