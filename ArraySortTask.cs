namespace ITIS;

public static class ArraySortTask
{
    private enum ArraySourceOptions
    {
        UserInput,
        Random,
        Permutation
    }

    private static readonly string[] arraySourceOptionsText = new string[]
    {
        "ручной ввод с клавиатуры",
        "случайное заполнение",
        "перемешивание"
    };

    private enum SortAlgorythmOptions
    {
        Cancel,
        BubbleSort,
        InsertionSort,
        MergeSort,
        MergeSortWithInsertion,
        QuickSort,
        QuickSortWithInsertion
    }

    private static readonly string[] sortAlgorythmOptionsText = new string[]
    {
        "выход",
        "сортировка пузырьком",
        "сортировка вставками",
        "сортировка слиянием",
        "сортировка слиянием + сортировка вставками",
        "быстрая сортировка",
        "быстрая сортировка + сортировка вставками"
    };

    private enum QuickSortPivotOptions
    {
        First,
        Last,
        Middle,
        Median
    }

    private static readonly string[] quickSortPivotOptionsText = new string[]
    {
        "первый элемент",
        "последний элемент",
        "срединный элемент",
        "медиана трёх элементов"
    };

    private struct OperationsCounter
    {
        public long FunctionCalls { get; set; }
        public long CycleIterations { get; set; }
        public long Comparisons { get; set; }
        public long Assignments { get; set; }

        public void Reset()
        {
            FunctionCalls = 0;
            CycleIterations = 0;
            Comparisons = 0;
            Assignments = 0;
        }

        private static void PrintCounterInfo(string message, long counterValue)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(counterValue);
        }

        public void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Выполнено операций:");

            PrintCounterInfo("Вызовы функций  - ", FunctionCalls);
            PrintCounterInfo("Итерации циклов - ", CycleIterations);
            PrintCounterInfo("Сравнения       - ", Comparisons);
            PrintCounterInfo("Присваивания    - ", Assignments);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }

    private static OperationsCounter operationsCounter;

    private static int ReadInt(string message, int min = int.MinValue, int max = int.MaxValue)
    {
        int input;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);

            var isParsed = int.TryParse(Console.ReadLine(), out input);
            if (isParsed && input >= min && input <= max)
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Некорректный ввод");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        return input;
    }

    private static int[] ReadIntArray(string message)
    {
        int[] array;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.Yellow;
            var input = Console.ReadLine()?.Split(' ') ?? Array.Empty<string>();
            array = new int[input.Length];

            var isParsed = false;
            for (var i = 0; i < input.Length; ++i)
            {
                isParsed = int.TryParse(input[i], out array[i]);
                if (!isParsed)
                    break;
            }

            if (isParsed)
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Некорректный ввод");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        return array;
    }

    private static int ReadUserChoice(string message, params string[] options)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        for (var i = 0; i < options.Length; ++i)
        {
            Console.WriteLine($"{i} - {options[i]}");
        }

        return ReadInt("Выбор: ", 0, options.Length - 1);
    }

    private static int[] GetArrayFromFile()
    {
        throw new NotImplementedException();
    }

    private static int[] GetArrayFromUser()
    {
        return ReadIntArray("Введите элементы массива через пробел:");
    }

    private static int[] GetRandomArray()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Введите длину массива от 1 до 1.000.000");
        var size = ReadInt("size = ", 1, 1000000);

        int min, max;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Введите минимальное значение элементов");
            min = ReadInt("min = ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Введите максимальное значение элементов");
            max = ReadInt("max = ");

            if (max >= min)
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Минимальное значение не должно превышать максимальное!");
            Console.WriteLine();
        }

        var array = new int[size];
        var random = new Random();
        for (var i = 0; i < size; ++i)
        {
            array[i] = random.Next(min, max + 1);
        }

        return array;
    }

    private static int[] GetPermutedArray()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Введите длину массива от 1 до 1.000.000");
        var size = ReadInt("size = ", 1, 1000000);

        var array = new int[size];
        for (var i = 0; i < size; ++i)
        {
            array[i] = i;
        }

        var random = new Random();
        for (var i = 0; i < size; ++i)
        {
            var j = random.Next(i, size);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }

    private static int[] GetInputArray()
    {
        var arraySourceOption = (ArraySourceOptions)ReadUserChoice("Выберите способ инициализации массива", arraySourceOptionsText);
        return arraySourceOption switch
        {
            ArraySourceOptions.UserInput => GetArrayFromUser(),
            ArraySourceOptions.Random => GetRandomArray(),
            ArraySourceOptions.Permutation => GetPermutedArray(),
            _ => throw new Exception("Что-то пошло не так..."),
        };
    }

    private static void PrintArrayBriefly(int[] array)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("array = [");
        if (array.Length <= 10)
        {
            for (var i = 0; i < array.Length; ++i)
            {
                Console.Write($"{array[i]}, ");
            }
        }
        else
        {
            for (var i = 0; i < 5; ++i)
            {
                Console.Write($"{array[i]}, ");
            }

            Console.Write("..., ");

            for (var i = array.Length - 5; i < array.Length; ++i)
            {
                Console.Write($"{array[i]}, ");
            }
        }

        Console.WriteLine("\b\b]");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    private static int GetMedian(int a, int b, int c)
    {
        operationsCounter.Comparisons += 2;

        if (a > b)
        {
            if (b > c)
                return b;

            ++operationsCounter.Comparisons;
            if (c > a)
                return a;
            return c;
        }
        else
        {
            if (c > b)
                return b;

            ++operationsCounter.Comparisons;
            if (a > c)
                return a;
            return c;
        }
    }

    private static void BubbleSort(int[] array, int startIndex, int count)
    {
        ++operationsCounter.FunctionCalls;

        var endIndex = startIndex + count;
        for (var i = 1; i < count; ++i)
        {
            ++operationsCounter.CycleIterations;

            for (var j = startIndex; j < endIndex - i; ++j)
            {
                ++operationsCounter.CycleIterations;

                ++operationsCounter.Comparisons;
                if (array[j] > array[j + 1])
                {
                    operationsCounter.Assignments += 3;
                    var t = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = t;
                }
            }
        }
    }

    private static void InsertionSort(int[] array, int startIndex, int count)
    {
        ++operationsCounter.FunctionCalls;

        for (var i = 1; i < count; ++i)
        {
            ++operationsCounter.CycleIterations;
            for (var j = startIndex + i; j > startIndex; --j)
            {
                ++operationsCounter.CycleIterations;

                ++operationsCounter.Comparisons;
                if (array[j] >= array[j - 1])
                    break;

                operationsCounter.Assignments += 3;
                var t = array[j];
                array[j] = array[j - 1];
                array[j - 1] = t;
            }
        }
    }

    private static void Merge(int[] tempArray, int[] array, int startIndex1, int startIndex2, int count)
    {
        ++operationsCounter.FunctionCalls;

        var endIndex = startIndex1 + count;

        var index1 = startIndex1;
        var index2 = startIndex2;
        for (var i = 0; i < count; ++i)
        {
            ++operationsCounter.CycleIterations;

            if (index1 < startIndex2 && index2 < endIndex)
                ++operationsCounter.Comparisons;

            ++operationsCounter.Assignments;
            if (index2 == endIndex || index1 < startIndex2 && array[index1] < array[index2])
            {
                tempArray[i] = array[index1];
                ++index1;
            }
            else
            {
                tempArray[i] = array[index2];
                ++index2;
            }
        }
    }

    private const int MergeSortThreshold = 16;

    private static void MergeSort(int[] array, int startIndex, int count, bool useInsertion)
    {
        ++operationsCounter.FunctionCalls;

        if (useInsertion && count < MergeSortThreshold)
        {
            InsertionSort(array, startIndex, count);
            return;
        }

        if (count <= 1)
            return;

        var halfCount = count >> 1;
        MergeSort(array, startIndex, halfCount, useInsertion);
        MergeSort(array, startIndex + halfCount, count - halfCount, useInsertion);

        var tempArray = new int[count];
        Merge(tempArray, array, startIndex, startIndex + halfCount, count);

        for (var i = 0; i < count; ++i)
        {
            ++operationsCounter.CycleIterations;

            ++operationsCounter.Assignments;
            array[startIndex + i] = tempArray[i];
        }
    }

    private const int QuickSortThreshold = 16;

    private static void QuickSort(int[] array, int startIndex, int count, QuickSortPivotOptions pivotOption, bool useInsertion)
    {
        ++operationsCounter.FunctionCalls;

        if (useInsertion && count < QuickSortThreshold)
        {
            InsertionSort(array, startIndex, count);
            return;
        }

        if (count <= 1)
            return;

        var pivot = pivotOption switch
        {
            QuickSortPivotOptions.First => array[startIndex],
            QuickSortPivotOptions.Last => array[startIndex + count - 1],
            QuickSortPivotOptions.Middle => array[startIndex + (count >> 1)],
            QuickSortPivotOptions.Median => GetMedian(array[startIndex], array[startIndex + (count >> 1)], array[startIndex + count - 1]),
            _ => throw new Exception("Что-то пошло не так...")
        };

        var index1 = startIndex;
        var index2 = startIndex + count - 1;
        while (index1 <= index2)
        {
            ++operationsCounter.CycleIterations;

            ++operationsCounter.Comparisons;
            if (array[index1] < pivot)
            {
                ++index1;
                continue;
            }

            ++operationsCounter.Comparisons;
            if (array[index2] > pivot)
            {
                --index2;
                continue;
            }

            operationsCounter.Assignments += 3;
            var t = array[index1];
            array[index1] = array[index2];
            array[index2] = t;

            ++index1;
            --index2;
        }

        QuickSort(array, startIndex, index2 + 1 - startIndex, pivotOption, useInsertion);
        QuickSort(array, index1, startIndex + count - index1, pivotOption, useInsertion);
    }

    public static void Run()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("========== Сортировка массивов ==========");
        Console.WriteLine();

        var array = GetInputArray();
        PrintArrayBriefly(array);

        while (true)
        {
            var sortAlgorythmOption = (SortAlgorythmOptions)ReadUserChoice("Выберите алгоритм сортировки", sortAlgorythmOptionsText);
            if (sortAlgorythmOption == SortAlgorythmOptions.Cancel)
                break;

            QuickSortPivotOptions quickSortPivotOption = 0;
            if (sortAlgorythmOption == SortAlgorythmOptions.QuickSort || sortAlgorythmOption == SortAlgorythmOptions.QuickSortWithInsertion)
                quickSortPivotOption = (QuickSortPivotOptions)ReadUserChoice("Укажите способ выбора опорного элемента", quickSortPivotOptionsText);

            var arrayCopy = new int[array.Length];
            Array.Copy(array, arrayCopy, array.Length);
            operationsCounter.Reset();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Сортировка...");
            var startTime = DateTime.Now;

            switch (sortAlgorythmOption)
            {
                case SortAlgorythmOptions.BubbleSort:
                    BubbleSort(arrayCopy, 0, array.Length);
                    break;

                case SortAlgorythmOptions.InsertionSort:
                    InsertionSort(arrayCopy, 0, array.Length);
                    break;

                case SortAlgorythmOptions.MergeSort:
                    MergeSort(arrayCopy, 0, array.Length, false);
                    break;

                case SortAlgorythmOptions.MergeSortWithInsertion:
                    MergeSort(arrayCopy, 0, array.Length, true);
                    break;

                case SortAlgorythmOptions.QuickSort:
                    QuickSort(arrayCopy, 0, array.Length, quickSortPivotOption, false);
                    break;

                case SortAlgorythmOptions.QuickSortWithInsertion:
                    QuickSort(arrayCopy, 0, array.Length, quickSortPivotOption, true);
                    break;

                default:
                    throw new Exception("Что-то пошло не так...");
            }

            var endTime = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Массив отсортирован за {(endTime - startTime).TotalSeconds} секунд");

            PrintArrayBriefly(arrayCopy);
            operationsCounter.PrintInfo();

            Console.ForegroundColor = ConsoleColor.White;
            for (var i = 0; i < 4; ++i)
            {
                Console.WriteLine();
            }
        }
    }
}