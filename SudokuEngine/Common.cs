using System.Collections.Generic;

namespace SudokuEngine
{
    public static class Common
    {
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard,
            Samurai
        }

        public static Dictionary<Difficulty, int> DifficultyUpperBoundMetrics = new Dictionary<Difficulty, int>()
        {
            {Difficulty.Easy,  250000},
            {Difficulty.Medium,  500000},
            {Difficulty.Hard,  1000000},
            {Difficulty.Samurai,  int.MaxValue}
        };
    }
}
