﻿namespace LinqBenchmarks.ImmutableArray.Int32;

public class ImmutableArrayInt32Sum: ImmutableArrayInt32BenchmarkBase
{
    Func<int> linqOptimizerQuery;

    protected override void Setup()
    {
        base.Setup();

        linqOptimizerQuery = source
            .AsQueryExpr()
            .Sum()
            .Compile();
    }

    [Benchmark(Baseline = true)]
    public int ForLoop()
    {
        var sum = 0;
        var array = source;
        for (var index = 0; index < array.Length; index++)
        {
            var item = array[index];
            sum += item;
        }
        return sum;
    }

    [Benchmark]
    public int ForeachLoop()
    {
        var sum = 0;
        foreach (var item in source)
        {
            sum += item;
        }
        return sum;
    }

    [Benchmark]
    public int Linq()
        => source.Sum();

    [Benchmark]
    public int LinqFasterer()
        => EnumerableF.SumF(source);

    [Benchmark]
    public int LinqOptimizer()
        => linqOptimizerQuery.Invoke();

    [Benchmark]
    public int Streams()
        => source
            .AsStream()
            .Sum();

    [Benchmark]
    public int StructLinq()
        => source
            .ToStructEnumerable()
            .Sum();

    [Benchmark]
    public int StructLinq_ValueDelegate()
    {
        return source
            .ToStructEnumerable()
            .Sum(x => x);
    }

    [Benchmark]
    public int Hyperlinq()
        => source
            .AsValueEnumerable()
            .Sum();
}