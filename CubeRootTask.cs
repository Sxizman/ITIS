namespace ITIS;

public static class CubeRootTask
{
    private static double Cbrt(double x)
    {
        if (x == 0)
            return 0;

        var xBits = BitConverter.DoubleToUInt64Bits(x);

        var sign = xBits >> 63;
        var exponent = (xBits << 1) >> 53;
        var mantissa = (xBits << 12) >> 12;

        mantissa = mantissa + (1UL << 52) * (exponent % 3) / 3;
        exponent = exponent / 3 + 682;

        var value = BitConverter.UInt64BitsToDouble((sign << 53) | (exponent << 52) | mantissa);
        for (var i = 0; i < 6; ++i)
            value = value - (value - x / (value * value)) / 3;

        return value;
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Вычисление кубического корня ==========");

        Console.Write("x = ");
        var x = double.Parse(Console.ReadLine() ?? "");

        Console.WriteLine($"cbrt(x) = {Cbrt(x)}");

        Console.WriteLine();
    }
}