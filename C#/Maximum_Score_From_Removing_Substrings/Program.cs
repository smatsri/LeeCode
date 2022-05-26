using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using static System.Console;

//await Run("2");
RunBenchmark();

static async Task Run(string exampleId)
{
    var (input, x, y, expected) = await Loader.FromExampleFile(exampleId);
    var solution = new Solution();
    var res = solution.MaximumGain(input, x, y);
    var success = res == expected;
    var msg = success ? "success: " + res : $"Fail: expected {expected} got {res}";
    WriteLine(msg);
}

static void RunBenchmark()
{
    BenchmarkRunner.Run<SolutionBenchmark>();
}

public class Solution
{
    public int MaximumGain(string s, int x, int y)
    {
        var (a, b, max, min) = x < y ? ('a', 'b', y, x) : ('b', 'a', x, y);
        var arr = s.ToCharArray().ToList();
        var sum = 0;
        for (var i = arr.Count - 2; i >= 0; i--)
        {

            if (arr.Count == i + 1 || arr[i] != b || arr[i + 1] != a)
            {
                continue;
            }

            sum += max;
            arr.RemoveRange(i, 2);

        }

        for (var i = arr.Count - 2; i >= 0; i--)
        {

            if (arr.Count == i + 1 || arr[i] != a || (arr.Count > i + 1 && arr[i + 1] != b))
            {
                continue;
            }


            sum += min;
            arr.RemoveRange(i, 2);

        }


        return sum;
    }
}

public class Solution2
{
    public int MaximumGain(string s, int x, int y)
    {
        var (a, b, max, min) = x < y ? ('a', 'b', y, x) : ('b', 'a', x, y);
        var arr = s.ToCharArray().ToList();
        var sum = 0;
        for (var i = arr.Count - 2; i >= 0; i--)
        {

            if (arr.Count == i + 1 || arr[i] != b || arr[i + 1] != a)
            {
                continue;
            }

            sum += max;
            //arr.RemoveRange(i, 2);

        }

        for (var i = arr.Count - 2; i >= 0; i--)
        {

            if (arr.Count == i + 1 || arr[i] != a || (arr.Count > i + 1 && arr[i + 1] != b))
            {
                continue;
            }


            sum += min;
            //arr.RemoveRange(i, 2);

        }


        return sum;
    }
}

public record Input(string Txt, int X, int Y, int Expected);

public class Loader
{
    public static Task<Input> FromExampleFile(string id = "1")
        => FromFile($"input\\{id}.txt");
    public static async Task<Input> FromFile(string path)
    {
        var text = await File.ReadAllTextAsync(path);
        var parts = text.Split('\n');
        var input = parts[0].Trim().Replace("\"", "");
        var x = int.Parse(parts[1]);
        var y = int.Parse(parts[2]);
        var expected = int.Parse(parts[3]);
        return new Input(input, x, y, expected);
    }
}

public class SolutionBenchmark
{
    [Benchmark]
    public void Solution_1()
    {
        var sln = new Solution();
        sln.MaximumGain("bbaaabab", 10, 1);
    }

    [Benchmark]
    public void Solution_2()
    {
        var sln = new Solution2();
        sln.MaximumGain("bbaaabab", 10, 1);
    }
}

