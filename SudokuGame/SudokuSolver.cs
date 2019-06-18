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
        public int RecursionDepth = 0;
        public int[,] SolveSudoku(string inputFile)
        {
            ReadInputs(inputFile);

            if (_sudokuBoard == null) throw new Exception("failed to fill sudoku");
            _maxRows = _sudokuBoard.GetLength(0);
            _maxColumns = _sudokuBoard.GetLength(1);

            if (Solve())
            {
                return _sudokuBoard;
            }
            throw new Exception("failed to solve sudoku");
        }

        public int[,] SolveSudoku(int[,] inputBoard)
        {
            _sudokuBoard = inputBoard;
            _maxRows = _sudokuBoard.GetLength(0);
            _maxColumns = _sudokuBoard.GetLength(1);

            if (Solve())
            {
                return _sudokuBoard;
            }
            throw new Exception("failed to solve sudoku");
        }

        /// <summary>
        /// input file should contain 0 for empty cells
        /// </summary>
        /// <param name="filePath"></param>
        private void ReadInputs(string filePath)
        {
            var rowCount = 0;
            var lines = File.ReadAllLines(filePath);
            _sudokuBoard = new int[lines.Length, lines.Length];

            foreach (var line in lines)
            {
                var columnCount = 0;
                var numbers = line.ToCharArray();

                foreach (var number in numbers)
                {
                    _sudokuBoard[rowCount, columnCount] = int.Parse(number.ToString());
                    columnCount++;
                }
                rowCount++;
            }
        }

        /// <summary>
        /// solving problem
        /// </summary>
        private bool Solve()
        {
            //measures recursion depth for judging difficulty
            RecursionDepth++;
            int row, column;
            if (AnyEmptyCell(out row, out column))
            {
                for (var value = 1; value <= 9; value++)
                {
                    if (IsValidChoice(value, row, column))
                    {
                        _sudokuBoard[row, column] = value;
                        if (Solve())
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
    }
}
