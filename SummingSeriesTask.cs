namespace ITIS;

public static class SummingSeriesTask
{
    private static double CalculateFirstSum(double x, int k)
    {
        if (k < 0)
            throw new ArgumentException("k must be positive");

        var termAcos = 1.0;
        var sum = 0.0;

        for (var i = 0; i < k; ++i)
        {
            termAcos *= x;
            sum += Math.Cos(termAcos);
        }

        return sum;
    }

    private static double CalculateSecondSum(double x, int k)
    {
        if (k < 0)
            throw new ArgumentException("k must be positive");

        var term = x;
        var sum = 0.0;

        for (var i = 0; i < k; ++i)
        {
            term = Math.Sin(term);
            sum += term;
        }

        return sum;
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Вычисление суммы ряда ==========");

        Console.Write("x = ");
        var x = double.Parse(Console.ReadLine() ?? "");
        Console.Write("Предел вычислений k = ");
        var k = int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine($"cos(x) + cos(x^2) + ... + cos(x^k) = {CalculateFirstSum(x, k)}");
        Console.WriteLine($"sin(x) + sin(sin(x)) + ... + sin^k(x) = {CalculateSecondSum(x, k)}");

        Console.WriteLine();
    }
}