// See https://leetcode.com/problems/russian-doll-envelopes/

using static System.Console;


Run("[[5,4],[6,4],[6,7],[2,3]]", 3);
// Run("[[1,100],[2,50],[3,51],[4,4],[5,5],[6,6]]", 3);
// Run("[[1,6],[2,4],[3,3],[4,4],[5,5],[6,6]]", 4);


static void Run(string input, int expected = 0)
{
    var solution = new Solution();
    var output = solution.Solve(input);
    if (output == expected)
    {
        WriteLine("success");
    }
    else
    {
        WriteLine($"fail: \tfor input {input} \n\texpected {expected} got {output}");
    }

}

record Box(int Width, int Height);

public class Solution
{

    public int Solve(string input)
    {
        var boxes =
             Parse(input)
            .OrderBy(a => a.Width)
            .ThenBy(a => a.Height)
            //.GroupBy(a => a.Width)
            .ToArray();

        var slops = boxes.Zip(boxes.Skip(1), GetSlop);

        var max = 0;
        var cur = 0;
        foreach (var slop in slops)
        {
            WriteLine(slop);
            if (slop > 0)
            {
                cur++;
                if (cur > max)
                    max = cur;


            }
            else
            {
                cur = 0;
            }
        }



        return max + 1;
    }

    public int Solve2(string input)
    {
        var groups =
             Parse(input)
            .OrderBy(a => a.Width)
            .ThenBy(a => a.Height)
            .GroupBy(a => a.Width);


        Box? curBox = null;
        var length = 0;
        foreach (var g in groups)
        {

            if (curBox == null)
            {
                curBox = g.First();
                length++;
                continue;
            }

            var box = g.FirstOrDefault(a => IsRussianDoll(curBox, a));
            if (box != null)
            {
                curBox = box;
                length++;
            }
        }

        return length;
    }

    static bool IsRussianDoll(Box small, Box big)
    {
        return big.Width > small.Width && big.Height > small.Height;
    }

    static double GetSlop(Box b1, Box b2)
    {
        WriteLine($"{b1} {b2}");
        return ((double)b2.Height - (double)b1.Height) / ((double)b2.Width - (double)b1.Width);
    }
    static Box[] Parse(string str)
    {
        var arr = System.Text.Json.JsonSerializer.Deserialize<int[][]>(str);
        if (arr == null)
        {
            throw new NullReferenceException();
        }
        return arr.Select(a => new Box(a[0], a[1])).ToArray();
    }
}


