﻿using System;
using System.Collections.Generic;

namespace Events.Utilities
{
    public static class RandomExtensions
    {
        public static IEnumerable<int> Ints(this Random random)
        {
            while (true)
                yield return random.Next();
        }
    }
}
