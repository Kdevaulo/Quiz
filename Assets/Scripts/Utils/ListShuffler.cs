using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Utils
{
    public static class ListShuffler
    {
        private static readonly Random Random = new Random();

        public static List<T> Shuffle<T>(this List<T> list)
        {
            return list.OrderBy(item => Random.Next()).ToList();
        }
    }
}