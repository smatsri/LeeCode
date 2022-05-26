using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using static System.Console;

//Run();

RunBenchmark();

static void RunBenchmark()
{
    BenchmarkRunner.Run<SolutionBenchmark>();
}

static void Run()
{

    RunSolution<Solution>("XL", 40);
    RunSolution<Solution>("MCMXCIV", 1994);

    WriteLine();

    RunSolution<Solution2>("XL", 40);
    RunSolution<Solution2>("MCMXCIV", 1994);
}

static void RunSolution<TSolution>(string input, int expected)
    where TSolution : ISolution, new()
{
    var sln = new TSolution();
    var output = sln.RomanToInt(input);
    var success = expected == output;

    WriteLine($"{input} -> {output}");
    if (success)
    {
        WriteLine("success");
    }
    else
    {
        WriteLine($"failed expected {expected} got {output}");
    }
}

public interface ISolution
{
    int RomanToInt(string s);
}

public class Solution : ISolution
{
    static readonly Dictionary<char, int> CharValues = new()
    {
        { 'I' , 1 },
        { 'V' , 5 },
        { 'X' , 10 },
        { 'L' , 50 },
        { 'C' , 100 },
        { 'D' , 500 },
        { 'M' , 1000 },
    };

    public int RomanToInt(string s)
    {
        var (result, _) = s.Aggregate((0, 1000), (acc, c) =>
        {
            var value = CharValues[c];
            var (sum, prev) = acc;
            var newSum = value > prev ? (sum - prev) + (value - prev) : sum + value;
            return (newSum, value);
        });
        return result;

    }
}

public class Solution2 : ISolution
{
    static readonly Dictionary<char, int> CharValues = new()
    {
        { 'I' , 1 },
        { 'V' , 5 },
        { 'X' , 10 },
        { 'L' , 50 },
        { 'C' , 100 },
        { 'D' , 500 },
        { 'M' , 1000 },
    };

    public int RomanToInt(string s)
    {
        var sum = 0;
        var prev = 1000;
        foreach (var c in s)
        {
            var cur = CharValues[c];
            var newSum = cur > prev ? sum + cur - 2 * prev : sum + cur;
            sum = newSum;
            prev = cur;
        }

        return sum;
    }
}

[MemoryDiagnoser]
public class SolutionBenchmark
{
    [Benchmark]
    public void V1()
    {
        var sol = new Solution();

        sol.RomanToInt("MCMXCIV");
    }


    [Benchmark]
    public void V2()
    {
        var sol = new Solution2();
        sol.RomanToInt("MCMXCIV");
    }
}