using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{
    internal class GameMath
    {
        public static float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
        }
        public static bool IsNumerical(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                {
                    Debugging.Log("Char not numberical: " + ((byte)c));
                    return false; // Character is not a digit
                }
            }
            return true; // All characters are digits

        }
    }
}
