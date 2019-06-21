using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuEngine
{
    /// <summary>
    /// includes common properties and functions for generator
    /// </summary>
    public interface IGenerator
    {
        int[,] BaseBoard { get; }
        int[,] Generate();
    }
}
