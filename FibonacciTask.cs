using System.Numerics;

namespace ITIS;

public static class FibonacciTask
{
    private static BigInteger GetNthFibonacciNumber(int n)
    {
        if (n < 1)
            throw new ArgumentException("n must be positive");

        BigInteger fib1 = 1;
        BigInteger fib2 = 0;
        BigInteger fibPower1 = 0;
        BigInteger fibPower2 = 0;
        while (n > 0)
        {
            if (fibPower2 == 0)
            {
                fibPower2 = 1;
            }
            else
            {
                var t1 = fibPower1 * fibPower1;
                var t2 = fibPower2 * fibPower2;
                var t3 = 2 * fibPower1 * fibPower2;

                fibPower1 = t1 + t2;
                fibPower2 = t2 + t3;
            }

            if (n % 2 != 0)
            {
                var t1 = fibPower1 * fib1;
                var t2 = fibPower1 * fib2;
                var t3 = fibPower2 * fib1;
                var t4 = fibPower2 * fib2;

                fib1 = t1 + t4;
                fib2 = t2 + t3 + t4;
            }
            n /= 2;
        }

        return fib2;
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Числа фибоначчи ==========");

        Console.Write("n = ");
        var n = int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine($"n-ное число Фибоначчи: {GetNthFibonacciNumber(n)}");

        Console.WriteLine();
    }
}