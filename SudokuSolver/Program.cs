using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        public static int[,] SudokuBoard;
        private static int maxRows;
        private static int maxColumns;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("please enter path of input txt file...\n");
                //ReadInputs(Console.ReadLine());
                ReadInputs("c:/sudoku.txt");

                if (SudokuBoard != null)
                {
                    maxRows = SudokuBoard.GetLength(0);
                    maxColumns = SudokuBoard.GetLength(1);

                    if (Solve())
                    {
                        Show();
                    }
                }
                Console.WriteLine("error while reading file..\n.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Show();
            }
            finally
            {
                Console.WriteLine("press any key to close...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// input file should contain 0 for empty cells
        /// </summary>
        /// <param name="filePath"></param>
        public static void ReadInputs(string filePath)
        {
            var rowCount = 0;
            var lines = File.ReadAllLines(filePath);
            SudokuBoard = new int[lines.Length, lines.Length];

            foreach (var line in lines)
            {
                var columnCount = 0;
                var numbers = line.ToCharArray();

                foreach (var number in numbers)
                {
                    SudokuBoard[rowCount, columnCount] = int.Parse(number.ToString());
                    columnCount++;
                }
                rowCount++;
            }
        }

        /// <summary>
        /// show contents of input file
        /// </summary>
        public static void Show()
        {
            for (var row = 0; row < maxRows; row++)
            {
                for (var column = 0; column < maxColumns; column++)
                {
                    Console.Write(SudokuBoard[row, column]);
                }
                Console.Write(Environment.NewLine);
            }
        }
        /// <summary>
        /// solving problem
        /// </summary>
        public static bool Solve()
        {
            int row, column;
            if (AnyEmptyCell(out row, out column))
            {
                if (row == 0 && column == 8)
                {

                }

                for (var value = 1; value <= 9; value++)
                {
                    if (IsValidChoice(value, row, column))
                    {
                        SudokuBoard[row, column] = value;
                        if (Solve())
                        {
                            return true;
                        }
                        SudokuBoard[row, column] = 0;
                    }
                }
                //not valid value
                return false;
            }
            //no empty cell
            return true;
        }

        public static bool AnyEmptyCell(out int row, out int column)
        {
            row = -1;
            column = -1;
            for (var rIndex = 0; rIndex < maxRows; rIndex++)
            {
                for (int cIndex = 0; cIndex < maxColumns; cIndex++)
                {
                    if (SudokuBoard[rIndex, cIndex] == 0)
                    {
                        row = rIndex;
                        column = cIndex;

                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsValidChoice(int value, int row, int column)
        {
            //check for row
            for (var cIndex = 0; cIndex < SudokuBoard.GetUpperBound(1); cIndex++)
            {
                if (SudokuBoard[row, cIndex] == value)
                {
                    return false;
                }
            }

            //check for column
            for (var rIndex = 0; rIndex < SudokuBoard.GetUpperBound(1); rIndex++)
            {
                if (SudokuBoard[rIndex, column] == value)
                {
                    return false;
                }
            }

            //check for sub-grid
            var squareRoot = (int)Math.Sqrt(SudokuBoard.GetLength(0));
            var subGridRowStart = row - row % squareRoot;
            var subGridColumnStart = column - column % squareRoot;

            for (var rIndex = subGridRowStart; rIndex < (subGridRowStart + squareRoot); rIndex++)
            {
                for (var cIndex = subGridColumnStart; cIndex < (subGridColumnStart + squareRoot); cIndex++)
                {
                    if (SudokuBoard[rIndex, cIndex] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
