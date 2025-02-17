﻿namespace LinqBenchmarks;

public class BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
        => Setup();
        
    protected virtual void Setup()
    {}

    [Params(100)]
    public int Count { get; set; }

    protected static int[] GetSequentialValues(int count)
    {
        var array = new int[count];

        for (var index = 0; index < array.Length; index++)
            array[index] = index;

        return array;
    }

    protected static int[] GetRandomValues(int count)
    {
        var array = new int[count];

        var random = new Random(42);
        for (var index = 0; index < array.Length; index++)
            array[index] = random.Next(count);

        return array;
    }
}