using System;

namespace BeFaster.App.Solutions.SUM
{
    public static class SumSolution
    {
        public static int Sum(int x, int y)
        {
            if (x < 0 || x > 100 || y < 0 || y > 100)
            {
                throw new ArgumentOutOfRangeException("Parameter not within the required range [0-100].");
            }

            return x + y;
        }
    }
}
