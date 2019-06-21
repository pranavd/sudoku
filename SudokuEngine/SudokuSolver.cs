using System;

namespace SudokuEngine
{
    /// <summary>
    /// Checks for right solver and returns it.
    /// </summary>
    public static class SudokuSolver
    {
        public static ISolver GetSolver(int[,] inputBoard)
        {
            if (inputBoard.GetLength(0) == 9)
            {
                return new Solver9X9(inputBoard);
            }
            else
            {
                throw new Exception("This grid cannot be solved currently");
            }
        }
    }
}
