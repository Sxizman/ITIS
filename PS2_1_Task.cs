namespace ITIS;

public static class PS2_1_Task
{
    private delegate (double, int) CalculationFunc(double value, double epsilon);

    private const int StepsLimit = 1000000;

    private static double alpha;

    private static (double, int) Exp(double x, double epsilon)
    {
        var expectedSum = Math.Exp(x);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= x / step;
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) ExpInv(double x, double epsilon)
    {
        var expectedSum = Math.Exp(1 / x);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term /= x * x * (2 * step - 1) * (2 * step);
            sum += term * (2 * step * x + 1);
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) Cos(double x, double epsilon)
    {
        var expectedSum = Math.Cos(x);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= -x * x / ((double)(2 * step - 1) * (2 * step));
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) Log(double x, double epsilon)
    {
        var expectedSum = Math.Log(x + 1);

        var step = 0;
        var term = -1.0;
        var sum = 0.0;
        do
        {
            ++step;
            term *= -x;
            sum += term / step;

        } while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit);

        return (sum, step);
    }

    private static (double, int) Pow(double x, double epsilon)
    {
        var expectedSum = Math.Pow(1 + x, alpha);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= (alpha - step + 1) * x / step;
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) AtanSmall(double x, double epsilon)
    {
        var expectedSum = Math.Atan(x);

        var step = 1;
        var term = x;
        var sum = x;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= -x * x;
            sum += term / (2 * step + 1);
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) AtanBig(double x, double epsilon)
    {
        var expectedSum = Math.Atan(x);

        var step = 1;
        var term = -1 / x;
        var sum = (x > 0 ? 1 : -1) * Math.PI / 2 - 1 / x;
        do
        {
            ++step;
            term /= -x * x;
            sum += term / (2 * step - 1);

        } while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit);

        return (sum, step);
    }

    private static (double, int) Sinh(double x, double epsilon)
    {
        var expectedSum = Math.Sinh(x);

        var step = 1;
        var term = x;
        var sum = x;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= x * x / ((double)(2 * step + 1) * (2 * step));
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) Cosh(double x, double epsilon)
    {
        var expectedSum = Math.Cosh(x);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= x * x / ((double)(2 * step - 1) * (2 * step));
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) Sqrt(double x, double epsilon)
    {
        var expectedSum = Math.Sqrt(x + 1);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= -x * (step - 0.5) / step;
            sum += term / (1 - 2 * step);
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) Pow2(double x, double epsilon)
    {
        var expectedSum = Math.Pow(x, alpha);

        var step = 1;
        var term = x;
        var sum = x;
        var logX = Math.Log(x);
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= (alpha - 1) * logX / step;
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) SqrCos(double x, double epsilon)
    {
        var expectedSum = Math.Pow(Math.Cos(x), 2);

        var step = 1;
        var term = 0.5;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= -x * x / ((step - 0.5) * step);
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) InvSqrt(double x, double epsilon)
    {
        var expectedSum = 1 / Math.Sqrt(x + 1);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= -x * (step - 0.5) / step;
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) InvSquare(double x, double epsilon)
    {
        var expectedSum = 1 / ((1 + x) * (1 + x));

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= -x;
            sum += term * (step + 1);
            ++step;
        }

        return (sum, step);
    }

    private static (double, int) PowOf3(double x, double epsilon)
    {
        var expectedSum = Math.Pow(3, x);

        var step = 1;
        var term = 1.0;
        var sum = 1.0;
        var log3 = Math.Log(3);
        while (!(Math.Abs(expectedSum - sum) < epsilon) && step < StepsLimit)
        {
            term *= x * log3 / step;
            sum += term;
            ++step;
        }

        return (sum, step);
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Вычисление сходящихся рядов ==========");

        var summingSeries = new (string Caption, CalculationFunc Func)[]
        {
            ("e^x", Exp),
            ("e^(1/x)", ExpInv),
            ("cos(x)", Cos),
            ("ln(1 + x)", Log),
            ("(1 + x)^alpha", Pow),
            ("arctg(x)", AtanSmall),
            ("arctg(x)", AtanBig),
            ("sinh(x)", Sinh),
            ("cosh(x)", Cosh),
            ("sqrt(1 + x)", Sqrt),
            ("x^alpha", Pow2),
            ("cos^2(x)", SqrCos),
            ("1/sqrt(1 + x)", InvSqrt),
            ("1/((1 + x)^2)", InvSquare),
            ("3^x", PowOf3)
        };

        Console.Write("Номер задачи: ");
        var taskIndex = int.Parse(Console.ReadLine() ?? "") - 1;

        Console.Write("x = ");
        var x = double.Parse(Console.ReadLine() ?? "");
        if (taskIndex == 4 || taskIndex == 10)
        {
            Console.Write("alpha = ");
            alpha = double.Parse(Console.ReadLine() ?? "");
        }

        Console.Write("Точность вычислений epsilon = ");
        var epsilon = double.Parse(Console.ReadLine() ?? "");
        if (epsilon <= 0)
            throw new Exception("epsilon должен быть больше нуля");

        (var result, var steps) = summingSeries[taskIndex].Func(x, epsilon);
        Console.WriteLine($"{summingSeries[taskIndex].Caption} ~ {result}");
        if (steps < StepsLimit)
            Console.WriteLine($"Требуемая точность достигнута за {steps} шагов");
        else
            Console.WriteLine("Не удалось достичь требуемой точности");

        Console.WriteLine();
    }
}