namespace ITIS;

static class ArraysTask
{
    private static List<int> _primes = new List<int>();

    static ArraysTask()
    {
        for (var i = 2; i * i > 0; ++i)
        {
            bool isPrime = true;
            for (var j = 0; j < _primes.Count; ++j)
            {
                if (i % _primes[j] == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime)
            {
                _primes.Add(i);
            }
        }
    }

    private static bool IsPrime(int number)
    {
        if (number < 2)
            return false;

        foreach (var prime in _primes)
        {
            if (prime * prime > number)
                break;

            if (number % prime == 0)
                return false;
        }

        return true;
    }

    private static void Swap<T>(ref T x, ref T y)
    {
        var t = x;
        x = y;
        y = t;
    }

    private static int MaxNegative(int[] array)
    {
        var max = 0;
        foreach (var num in array)
        {
            if (num < 0 && (max == 0 || num > max))
            {
                max = num;
            }
        }

        return max;
    }

    private static int MinPositive(int[] array)
    {
        var min = 0;
        foreach (var num in array)
        {
            if (num > 0 && (min == 0 || num < min))
            {
                min = num;
            }
        }

        return min;
    }

    private static int IndexOfMax(int[] array)
    {
        if (array.Length == 0)
            return -1;

        var index = 0;
        var max = array[0];
        for (var i = 1; i < array.Length; ++i)
        {
            if (array[i] > max)
            {
                index = i;
                max = array[i];
            }
        }

        return index;
    }

    private static int CountOfMin(int[] array)
    {
        if (array.Length == 0)
            return 0;

        var min = array[0];
        var count = 1;
        for (var i = 1; i < array.Length; ++i)
        {
            if (array[i] < min)
            {
                min = array[i];
                count = 1;
            }
            else if (array[i] == min)
            {
                ++count;
            }
        }

        return count;
    }

    private static int PrimesCount(int[] array)
    {
        var count = 0;
        foreach (var num in array)
        {
            if (IsPrime(num))
            {
                ++count;
            }
        }

        return count;
    }

    private static void Reverse(int[] array)
    {
        for (int i = 0, j = array.Length - 1; i < j; ++i, --j)
        {
            Swap(ref array[i], ref array[j]);
        }
    }

    private static bool IsSorted(int[] array)
    {
        for (var i = 1; i < array.Length; ++i)
        {
            if (array[i] < array[i - 1])
                return false;
        }

        return true;
    }

    private static void RotateLeft(int[] array, int k)
    {
        var kClamped = k % array.Length;
        if (kClamped == 0 || array.Length == 0)
            return;
        if (kClamped < 0)
        {
            kClamped += array.Length;
        }

        var index = 0;
        var indexNext = 0;
        var chainStart = 0;
        var t = array[0];
        for (var i = 0; i < array.Length; ++i)
        {
            index = indexNext;
            indexNext += kClamped;
            if (indexNext >= array.Length)
            {
                indexNext -= array.Length;
            }

            if (indexNext == chainStart)
            {
                array[index] = t;
                indexNext = ++chainStart;
                t = array[indexNext];
            }
            else
            {
                array[index] = array[indexNext];
            }
        }
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Массивы ==========");

        Console.Write("Элементы массива (через пробел): ");
        var input = Console.ReadLine() ?? "";
        var array = input.Split(' ')
            .Select(x => int.Parse(x))
            .ToArray();

        var maxNegative = MaxNegative(array);
        Console.WriteLine(maxNegative < 0 ?
            $"Максимальное отрицательное число в массиве: {maxNegative}" :
            "В массиве нет отрицательных чисел");
        var minPositive = MinPositive(array);
        Console.WriteLine(minPositive > 0 ?
            $"Минимальное положительное число в массиве: {minPositive}" :
            "В массиве нет положительных чисел");

        Console.WriteLine($"Индекс максимального элемента: [{IndexOfMax(array)}]");

        Console.WriteLine($"Минимальный элемента массива повторяется {CountOfMin(array)} раз");

        Console.WriteLine($"В массиве {PrimesCount(array)} простых чисел");

        Reverse(array);
        Console.Write("Массив после инвертирования:");
        foreach (var num in array)
        {
            Console.Write($" {num}");
        }
        Console.WriteLine();
        Reverse(array);

        Console.Write("k = ");
        var k = int.Parse(Console.ReadLine() ?? "");
        RotateLeft(array, k);
        Console.Write("Массив после циклического сдвига влево на k позиций:");
        foreach (var num in array)
        {
            Console.Write($" {num}");
        }
        Console.WriteLine();
        RotateLeft(array, -k);

        Console.WriteLine($"Массив {(IsSorted(array) ? "отсортирован" : "не отсортирован")} по возрастанию");

        Console.WriteLine();
    }
}