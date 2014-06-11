using System;

namespace Et075.Model
{
    public static class Constants
    {
        /// <summary>
        /// How many ets fit on single sheet
        /// </summary>
        public const int ETS_ON_SHEET = 16;
        
        /// <summary>
        /// Maximum difference between two runs 
        /// when smaller run is "normalized" to match the bigger run
        /// </summary>
        public const int RUN_DELTA = 500;

        /// <summary>
        /// Вартість друкування кожного наступного спуска: 2000 етикеток.
        /// (Або + 5 форм, + приладка на друк, + приладка на тиснення, + приладка на конгрев
        /// </summary>
        public const int LAYOUT_COSTS = 6400;

        /// <summary>
        /// How much 1/96 inches in 1 millimeter
        /// </summary>
        public const float MM = 96.0f / 25.4f;
    }
}
