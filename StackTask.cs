namespace ITIS;

public static class StackTask
{
    public static bool IsOpenBracket(char c)
    {
        return c == '(' || c == '[' || c == '{';
    }

    public static bool IsCloseBracket(char c)
    {
        return c == ')' || c == ']' || c == '}';
    }

    public static bool IsSameBrackets(char c1, char c2)
    {
        return (c1 == '(' && c2 == ')') || (c1 == '[' && c2 == ']') || (c1 == '{' && c2 == '}');
    }

    public static bool CheckBracketsParity(string line)
    {
        var brackets = new MyStack<char>();

        foreach (var c in line)
        {
            if (IsOpenBracket(c))
            {
                brackets.Push(c);
            }
            else if (IsCloseBracket(c))
            {
                if (brackets.IsEmpty())
                    return false;
                if (!IsSameBrackets(brackets.Peek(), c))
                    return false;

                brackets.Pop();
            }
        }

        return brackets.IsEmpty();
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Стек и операции с ним ==========");

        Console.Write("Введите строку, содержащую скобки: ");
        var line = Console.ReadLine();
        Console.WriteLine($"Скобки в строке расставлены {(CheckBracketsParity(line) ? "корректно" : "некорректно")}");

        Console.WriteLine();
    }
}