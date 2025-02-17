﻿using System.Threading.Tasks;

namespace LinqBenchmarks.AsyncEnumerable.Int32;

public partial class AsyncEnumerableInt32WhereSelectToListAsync: AsyncEnumerableInt32BenchmarkBase
{
    [Benchmark(Baseline = true)]
    public async ValueTask<List<int>> ForeachLoop()
    {
        var list = new List<int>();
        await foreach (var item in source)
        {
            if (item.IsEven())
                list.Add(item * 3);
        }
        return list;
    }

    [Benchmark]
    public ValueTask<List<int>> Linq()
        => source
            .Where(item => item.IsEven())
            .Select(item => item * 3)
            .ToListAsync();

    [Benchmark]
    public ValueTask<List<int>> Hyperlinq()
        => source.AsAsyncValueEnumerable()
            .Where((item, _) => new ValueTask<bool>(item.IsEven()))
            .Select((item, _) => new ValueTask<int>(item * 3))
            .ToListAsync();

    [Benchmark]
    public ValueTask<List<int>> Hyperlinq_ValueDelegate()
        => source.AsAsyncValueEnumerable()
            .Where<Int32IsEven>()
            .Select<int, TripleOfInt32>()
            .ToListAsync();
}