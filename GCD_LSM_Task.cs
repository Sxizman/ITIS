namespace ITIS;

public static class GCD_LSM_Task
{
    private static int GCD(int a, int b)
    {
        if (a < 1 || b < 1)
            throw new ArgumentException("a and b must be positive");

        while (b > 0)
        {
            var t = a;
            a = b;
            b = t % b;
        }

        return a;
    }

    private static long LCM(int a, int b)
    {
        return (long)a * b / GCD(a, b);
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== НОД и НОК ==========");

        Console.Write("a = ");
        var a = int.Parse(Console.ReadLine() ?? "");
        Console.Write("b = ");
        var b = int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine($"НОД(a, b) = {GCD(a, b)}");
        Console.WriteLine($"НОК(a, b) = {LCM(a, b)}");

        Console.WriteLine();
    }
}