using System.Text.Json;
using static System.Console;



var input = System.IO.File.ReadAllText("input.simple.txt");
var lines = input.Split('\n');
var calls = JsonSerializer.Deserialize<string[]>(lines[0]) ?? Array.Empty<string>();
var callsParams = JsonSerializer.Deserialize<int[][]>(lines[1]) ?? Array.Empty<int[]>();

StockPrice stockPrice = new();
Write("[");
for (int i = 0; i < calls.Length; i++)
{
    var call = calls[i];
    switch (call)
    {
        case "StockPrice":
            stockPrice = new();
            Write("null");
            break;

        case "update":
            var prms = callsParams[i];
            stockPrice.Update(prms[0], prms[1]);
            Write("null");
            break;
        case "current":
            Write(stockPrice.Current());
            break;
        case "maximum":
            Write(stockPrice.Maximum());
            break;
        case "minimum":
            Write(stockPrice.Minimum());
            break;
    }

    if (i < calls.Length - 1)
    {
        Write(",");
    }
}

Write("]");

public class StockPrice
{
    int cur = 0;
    int max = 0;
    int min = 0;
    int curTimestamp = -1;
    bool init = false;
    readonly Dictionary<int, int> prices = new();


    public void Update(int timestamp, int price)
    {
        if (timestamp >= curTimestamp)
        {
            cur = price;
            curTimestamp = timestamp;
        }

        if (prices.ContainsKey(timestamp))
        {
            var prevPrice = prices[timestamp];
            prices[timestamp] = price;
            if (prevPrice == max)
            {
                if (price > prevPrice)
                {
                    max = price;
                }
                else
                {
                    max = prices.Values.Max();
                }
            }
            else
            {
                max = Math.Max(max,price);
            }

            if (prevPrice == min)
            {
                if (price < prevPrice)
                {
                    min = price;
                }
                else
                {
                    min = prices.Values.Min();

                }
            }
            else
            {
                max = Math.Max(max, price);
            }
        }
        else
        {
            if (!init || price > max)
            {
                max = price;
            }

            if (!init || price < min)
            {
                min = price;
            }
        }
        prices.TryAdd(timestamp, cur);
        init = true;
    }

    public int Current()
    {
        return cur;

    }

    public int Maximum()
    {
        return max;

    }

    public int Minimum()
    {
        return min;
    }
}



