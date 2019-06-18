using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    /// <summary>
    /// checks if sudoku is valid and finds it unique solution
    /// </summary>
    public class SudokuSolver
    {
        private int[,] _sudokuBoard;
        private int _maxRows;
        private int _maxColumns;
        public int RecursionDepth;
        public Common.Difficulty Difficulty;

        public int[,] SolveSudoku(int[,] inputBoard)
        {
            _sudokuBoard = inputBoard;
            _maxRows = _sudokuBoard.GetLength(0);
            _maxColumns = _sudokuBoard.GetLength(1);

            if (Solve(ref RecursionDepth))
            {
                EvaluateDifficulty();
                return _sudokuBoard;
            }
            throw new Exception("failed to solve sudoku");
        }

        /// <summary>
        /// solving problem
        /// </summary>
        /// <param name="recursionDepth">holds the value for recursion depth</param>
        /// <returns></returns>
        private bool Solve(ref int recursionDepth)
        {
            int row, column;
            if (AnyEmptyCell(out row, out column))
            {
                for (var value = 1; value <= 9; value++)
                {
                    if (IsValidChoice(value, row, column))
                    {
                        _sudokuBoard[row, column] = value;
                        recursionDepth++;
                        if (Solve(ref recursionDepth))
                        {
                            return true;
                        }
                        _sudokuBoard[row, column] = 0;
                    }
                }
                //not valid value
                return false;
            }
            //no empty cell
            return true;
        }

        /// <summary>
        /// checks for any empty cell, i.e. 0 value
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool AnyEmptyCell(out int row, out int column)
        {
            row = -1;
            column = -1;
            for (var rIndex = 0; rIndex < _maxRows; rIndex++)
            {
                for (int cIndex = 0; cIndex < _maxColumns; cIndex++)
                {
                    if (_sudokuBoard[rIndex, cIndex] == 0)
                    {
                        row = rIndex;
                        column = cIndex;

                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// check for sudoku row, column, sub-grid constraints
        /// </summary>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool IsValidChoice(int value, int row, int column)
        {
            //check for row
            for (var cIndex = 0; cIndex < _sudokuBoard.GetUpperBound(1); cIndex++)
            {
                if (_sudokuBoard[row, cIndex] == value)
                {
                    return false;
                }
            }

            //check for column
            for (var rIndex = 0; rIndex < _sudokuBoard.GetUpperBound(1); rIndex++)
            {
                if (_sudokuBoard[rIndex, column] == value)
                {
                    return false;
                }
            }

            //check for sub-grid
            var squareRoot = (int)Math.Sqrt(_sudokuBoard.GetLength(0));
            var subGridRowStart = row - row % squareRoot;
            var subGridColumnStart = column - column % squareRoot;

            for (var rIndex = subGridRowStart; rIndex < (subGridRowStart + squareRoot); rIndex++)
            {
                for (var cIndex = subGridColumnStart; cIndex < (subGridColumnStart + squareRoot); cIndex++)
                {
                    if (_sudokuBoard[rIndex, cIndex] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void EvaluateDifficulty()
        {
            if (RecursionDepth > Common.DifficultyUpperBoundMetrics[Common.Difficulty.Hard])
            {
                Difficulty = Common.Difficulty.Samurai;
            }
            else
            {
                if (RecursionDepth >
                    Common.DifficultyUpperBoundMetrics[Common.Difficulty.Medium] &&
                    RecursionDepth <= Common.DifficultyUpperBoundMetrics[Common.Difficulty.Hard])
                {
                    Difficulty = Common.Difficulty.Hard;
                }
                else
                {
                    if (RecursionDepth >
                        Common.DifficultyUpperBoundMetrics[Common.Difficulty.Easy] &&
                        RecursionDepth <=
                        Common.DifficultyUpperBoundMetrics[Common.Difficulty.Medium])
                    {
                        Difficulty = Common.Difficulty.Medium;
                    }
                    else
                    {
                        Difficulty = Common.Difficulty.Easy;
                    }
                }
            }
        }
    }
}
