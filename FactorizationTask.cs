namespace ITIS;

public static class FactorizationTask
{
    private static List<int> _primes = new List<int>();

    static FactorizationTask()
    {
        for (var i = 2; i * i > 0; ++i)
        {
            bool isPrime = true;
            for (var j = 0; (j < _primes.Count) && isPrime; ++j)
                if (i % _primes[j] == 0)
                    isPrime = false;

            if (isPrime)
                _primes.Add(i);
        }
    }

    private static List<int> Factorize(int x)
    {
        if (x < 2)
            throw new ArgumentException("x must be greater then 1");

        var dividers = new List<int>();
        for (var i = 0; (i < _primes.Count) && (_primes[i] * _primes[i] <= x); ++i)
        {
            if (x % _primes[i] == 0)
            {
                dividers.Add(_primes[i]);
                do
                {
                    x /= _primes[i];
                } while (x % _primes[i] == 0);
            }
        }
        if (x > 1)
            dividers.Add(x);

        return dividers;
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Разложение на множители ==========");

        Console.Write("x = ");
        var x = int.Parse(Console.ReadLine() ?? "");

        var dividers = Factorize(x);
        Console.Write("Простые делители x: ");
        for (var i = 0; i < dividers.Count - 1; ++i)
            Console.Write($"{dividers[i]}, ");
        Console.WriteLine(dividers[dividers.Count - 1]);

        Console.WriteLine();
    }
}