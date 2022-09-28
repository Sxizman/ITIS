using System.Numerics;

namespace ITIS;

public static class FibonacciTask
{
    private static BigInteger GetNthFibonacciNumber(int n)
    {
        if (n < 1)
            throw new ArgumentException("n must be positive");

        BigInteger fib0 = 0;
        BigInteger fib1 = 1;

        for (var bitMask = 1 << (int)Math.Log2(n); bitMask > 0; bitMask >>= 1)
        {
            var t0 = fib0 * fib0;
            var t1 = 2 * fib0 * fib1;
            var t2 = fib1 * fib1;

            if ((n & bitMask) == 0)
            {
                fib0 = t1 - t0;
                fib1 = t2 + t0;
            }
            else
            {
                fib0 = t0 + t2;
                fib1 = t1 + t2;
            }
        }

        return fib0;
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