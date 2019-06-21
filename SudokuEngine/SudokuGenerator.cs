using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuEngine
{
    /// <summary>
    /// Checks for right generator and returns it.
    /// </summary>
    public static class SudokuGenerator
    {
        public static IGenerator GetGenerator(int squareDimension, Common.Difficulty difficulty)
        {
            if (squareDimension == 9)
            {
                return new Generator9X9(difficulty);
            }
            else
            {
                throw new Exception("This grid cannot be solved currently");
            }
        }
    }
}
