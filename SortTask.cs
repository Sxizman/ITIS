namespace ITIS;

public static class SortTask
{
    private static void Swap<T>(ref T x, ref T y)
    {
        var t = x;
        x = y;
        y = t;
    }

    private static void BubbleSort<T>(T[] array) where T : IComparable<T>
    {
        for (var i = 1; i < array.Length; ++i)
            for (var j = 0; j < array.Length - i; ++j)
                if (array[j].CompareTo(array[j + 1]) > 0)
                    Swap(ref array[j], ref array[j + 1]);
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Сортировка по возрастанию ==========");

        var array = new double[3];
        Console.Write("a = ");
        array[0] = double.Parse(Console.ReadLine() ?? "");
        Console.Write("b = ");
        array[1] = double.Parse(Console.ReadLine() ?? "");
        Console.Write("c = ");
        array[2] = double.Parse(Console.ReadLine() ?? "");

        BubbleSort(array);

        Console.WriteLine("После сортировки:");
        Console.WriteLine($"a = {array[0]}");
        Console.WriteLine($"b = {array[1]}");
        Console.WriteLine($"c = {array[2]}");

        Console.WriteLine();
    }
}