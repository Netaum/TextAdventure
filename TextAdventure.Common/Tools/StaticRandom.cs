using System;
using System.Threading;

namespace TextAdventure.Common.Tools
{
    public static class StaticRandom
    {
        private static int seed;
        private static ThreadLocal<Random> threadLocal = 
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
        public static int RollDice(int numberOfDice = 1) 
        {
            int minDice = numberOfDice;
            int maxDice = (numberOfDice * 6) + 1;
            return Instance.Next(minDice, maxDice);
        }
    }
}