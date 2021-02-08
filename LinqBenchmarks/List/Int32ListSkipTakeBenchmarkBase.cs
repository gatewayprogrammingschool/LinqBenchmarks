﻿using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace LinqBenchmarks
{
    public class Int32ListSkipTakeBenchmarkBase : SkipTakeBenchmarkBase
    {
        protected List<int> source;

        [GlobalSetup]
        public void GlobalSetup()
            => source = GetRandomValues(Skip + Count).ToList();
    }
}
