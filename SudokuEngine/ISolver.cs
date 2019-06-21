using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuEngine
{
    /// <summary>
    /// includes common properties and functions for solver
    /// </summary>
    public interface ISolver
    {
        int RecursionDepth { get; }
        Common.Difficulty Difficulty { get; }
        int[,] Solve();
    }
}
