using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor___GuessingGame
{
    public static class Extensions
    {
        public static bool Range(this int checkMe, int minValue, int maxValue)
        {
            return checkMe >= minValue && checkMe <= maxValue;
        }
    }
}
