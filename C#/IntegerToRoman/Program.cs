using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

for (int i = 1250; i < 1260; i++)
{
    Console.WriteLine(SolutionV1.IntToRoman(i));

}
Console.WriteLine();
for (int i = 1; i < 10; i++)
{
    var v = (i + 10 * i);
    Console.WriteLine($"{v}: {SolutionV1.IntToRoman(v)}");

}

//BenchmarkRunner.Run<IntToRomanBench>();

//Console.WriteLine(SolutionV6.IntToRoman(4));


class SolutionV1
{
    public static string IntToRoman(int num)
    {
        var sb = new StringBuilder();
        var cur = num;
        var deg = 0;
        while (cur > 0)
        {
            var d = cur % 10;
            var (s, m, l) = Letters.Degs[deg];
            InsertDigit(d, s, m, l);
            cur = (cur - d) / 10;
            deg++;
        }
        return sb.ToString();


        void InsertDigit(int digit, char s, char m, char l)
        {
            switch (digit)
            {
                case < 4:
                    for (int i = 0; i < digit; i++)
                        sb.Insert(0, s);
                    break;
                case 4:
                    sb.Insert(0, m);
                    sb.Insert(0, s);
                    break;
                case 5: sb.Insert(0, m); break;
                case < 9:
                    for (int i = 0; i < digit - 5; i++)
                        sb.Insert(0, s);
                    sb.Insert(0, m);
                    break;

                case 9:
                    sb.Insert(0, l);
                    sb.Insert(0, s);
                    break;

            }
        }
    }

    class Letters
    {
        public const char I = 'I';
        public const char V = 'V';
        public const char X = 'X';
        public const char L = 'L';
        public const char C = 'C';
        public const char D = 'D';
        public const char M = 'M';

        public readonly (char, char, char) Deg1 = (I, V, X);

        public static readonly (char, char, char)[] Degs = new[]
        {
        (I, V, X),
        (X, L, C),
        (C, D, M),
        (M, M, M),
    };
    }

}


class SolutionV6
{
    public static string IntToRoman(int num)
    {
        var sb = new StringBuilder();
        var deg = (int)Math.Log10(num);
        while (deg >= 0)
        {
            var d = (int)(num / Math.Pow(10, deg)) % 10;
            var (s, m, l) = Letters.Degs[deg];
            InsertDigit(d, s, m, l);
            deg--;
        }
        return sb.ToString();


        void InsertDigit(int digit, char s, char m, char l)
        {
            switch (digit)
            {
                case 5: sb.Append(m); break;
                case 9:
                    sb.Append(s);
                    sb.Append(l);
                    break;
                case 4:
                    sb.Append(s);
                    sb.Append(m);
                    break;
                case < 4:
                    for (int i = 0; i < digit; i++)
                        sb.Append(s);
                    break;


                default:
                    sb.Append(m);
                    for (int i = 0; i < digit - 5; i++)
                        sb.Append(s);
                    break;
            }
        }
    }

    class Letters
    {
        public const char I = 'I';
        public const char V = 'V';
        public const char X = 'X';
        public const char L = 'L';
        public const char C = 'C';
        public const char D = 'D';
        public const char M = 'M';

        public readonly (char, char, char) Deg1 = (I, V, X);

        public static readonly (char, char, char)[] Degs = new[]
        {
            (I, V, X),
            (X, L, C),
            (C, D, M),
            (M, M, M),
        };
    }

}

[MemoryDiagnoser]
public class IntToRomanBench
{
    [Benchmark]
    public void V1()
    {
        SolutionV1.IntToRoman(3999);
    }


    [Benchmark]
    public void V6()
    {
        SolutionV6.IntToRoman(3999);
    }
}



