using System;
using System.Collections.Generic;

namespace TriviaGame.Api.Utils;

public static class ListExtension
{
    private static readonly Random s_rng = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = s_rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}