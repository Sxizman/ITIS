namespace ITIS;

public static class ComplexTask
{
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Лабораторная работа 1. Комплексные числа ==========");

        var a1 = AlgebraicComplex.FromAlgebraic(1, 2);
        Console.WriteLine($"a1 = {a1.ToAlgebraicString()} = {a1.ToTrigonometricString()}");
        var a2 = AlgebraicComplex.FromTrigonometric(2, Math.PI * 3 / 4);
        Console.WriteLine($"a2 = {a2.ToAlgebraicString()} = {a2.ToTrigonometricString()}");

        Console.WriteLine($"a1 + a2 = {a1 + a2}");
        Console.WriteLine($"a1 - a2 = {a1 - a2}");
        Console.WriteLine($"a1 * a2 = {a1 * a2}");
        Console.WriteLine($"a1 / a2 = {a1 / a2}");

        Console.WriteLine();

        var t1 = TrigonometricComplex.FromAlgebraic(-4, -4);
        Console.WriteLine($"t1 = {t1.ToAlgebraicString()} = {t1.ToTrigonometricString()}");
        var t2 = TrigonometricComplex.FromTrigonometric(1, Math.PI * 5 / 2);
        Console.WriteLine($"t2 = {t2.ToAlgebraicString()} = {t2.ToTrigonometricString()}");

        Console.WriteLine($"t1 * t2 = {t1 * t2}");
        Console.WriteLine($"t1 / t2 = {t1 / t2}");

        Console.WriteLine();
    }
}