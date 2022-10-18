namespace ITIS;

public static class SquareMatrixTask
{
    private static bool IsIdentity(int[,] matrix)
    {
        var size = matrix.GetLength(0);

        for (var i = 0; i < size; ++i)
            for (var j = 0; j < size; ++j)
                if (matrix[i, j] != (i == j ? 1 : 0))
                    return false;

        return true;
    }

    private static bool IsUpperTriangular(int[,] matrix)
    {
        var size = matrix.GetLength(0);

        for (var i = 0; i < size; ++i)
            for (var j = 0; j < i; ++j)
                if (matrix[i, j] != 0)
                    return false;

        return true;
    }

    private static int GetEdgeElementsSum(int[,] matrix)
    {
        var size = matrix.GetLength(0);
        if (size == 1)
            return matrix[0, 0];

        var sum = 0;
        for (var i = 0; i < size - 1; ++i)
        {
            sum += matrix[i, 0];
            sum += matrix[i + 1, size - 1];
            sum += matrix[size - 1, i];
            sum += matrix[0, i + 1];
        }

        return sum;
    }

    private static int GetDiagonalElementsSum(int[,] matrix)
    {
        var size = matrix.GetLength(0);

        var sum = (size % 2 == 0) ? 0 : -matrix[size / 2, size / 2];
        for (var i = 0; i < size; ++i)
        {
            sum += matrix[i, i];
            sum += matrix[size - i - 1, i];
        }

        return sum;
    }

    private static int GetUpperAndLowerQuartersSum(int[,] matrix)
    {
        var size = matrix.GetLength(0);

        var sum = 0;
        for (var i = 0; i < size; ++i)
        {
            var from = Math.Min(i, size - i - 1);
            var to = Math.Max(i, size - i - 1);

            for (var j = from; j <= to; ++j)
                sum += matrix[i, j];
        }

        return sum;
    }

    private static void RotateRight(int[,] matrix)
    {
        var size = matrix.GetLength(0);

        for (var i = 0; i < size / 2; ++i)
        {
            for (var j = i; j < size - i - 1; ++j)
            {
                var t = matrix[i, j];
                matrix[i, j] = matrix[size - j - 1, i];
                matrix[size - j - 1, i] = matrix[size - i - 1, size - j - 1];
                matrix[size - i - 1, size - j - 1] = matrix[j, size - i - 1];
                matrix[j, size - i - 1] = t;
            }
        }
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Матрицы ==========");

        Console.Write("Введите размер квадратной матрицы: ");
        var n = int.Parse(Console.ReadLine() ?? "");
        if (n < 1)
            throw new Exception("Размер матрицы должен быть положительным");

        var matrix = new int[n, n];
        Console.WriteLine("Введите строки матрицы через Enter (элементы строки через пробел):");
        for (var i = 0; i < n; ++i)
        {
            var row = (Console.ReadLine() ?? "").Split(' ')
            .Select(x => int.Parse(x))
            .ToArray();
            if (row.Length != n)
                throw new Exception("Некорректное количество элементов в строке");

            for (var j = 0; j < n; ++j)
                matrix[i, j] = row[j];
        }

        Console.WriteLine($"Матрица {(IsIdentity(matrix) ? "является" : "не является")} единичной");
        Console.WriteLine($"Матрица {(IsUpperTriangular(matrix) ? "является" : "не является")} верхне-треугольной");

        Console.WriteLine($"Сумма крайних элементов: {GetEdgeElementsSum(matrix)}");
        Console.WriteLine($"Сумма диагональных элементов: {GetDiagonalElementsSum(matrix)}");
        Console.WriteLine($"Сумма элементов верхней и нижней четверти: {GetUpperAndLowerQuartersSum(matrix)}");

        RotateRight(matrix);
        Console.WriteLine("Матрица после поворота на 90 градусов по часовой стрелке:");
        for (var i = 0; i < n; ++i)
        {
            for (var j = 0; j < n; ++j)
                Console.Write(matrix[i, j] + " ");
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}