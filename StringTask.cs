namespace ITIS;

public static class StringTask
{
    public static List<string> GetPalindromes(string[] words)
    {
        var list = new List<string>();

        foreach (var word in words)
        {
            bool isPalindrome = true;
            for (int i = 0, j = word.Length - 1; i < j; ++i, --j)
            {
                if (word[i] != word[j])
                {
                    isPalindrome = false;
                    break;
                }
            }

            if (isPalindrome && !list.Contains(word))
                list.Add(word);
        }

        return list;
    }

    public static List<string> GetOrderedLettersWords(string[] words)
    {
        var list = new List<string>();

        foreach (var word in words)
        {
            bool isOrdered = true;
            for (var i = 1; i < word.Length; ++i)
            {
                if (word[i].CompareTo(word[i - 1]) < 0)
                {
                    isOrdered = false;
                    break;
                }
            }

            if (isOrdered && !list.Contains(word))
                list.Add(word);
        }

        return list;
    }

    public static string GetLongestOrShortestWord(string[] words, bool longest)
    {
        if (words.Length == 0)
            return null;

        var word = words[0];
        for (var i = 1; i < words.Length; ++i)
        {
            if (longest ? (words[i].Length > word.Length) : (words[i].Length < word.Length))
                word = words[i];
        }

        return word;
    }

    public static string MakeWordsListString(List<string> words)
    {
        if (words.Count == 0)
            return null;

        return words.Aggregate((str, word) => $"{str}, {word}");
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Строки ==========");

        Console.Write("Введите строку: ");
        var line = Console.ReadLine() ?? "";
        var words = line.Split(' ');

        Console.Write("Палиндромы: ");
        Console.WriteLine(MakeWordsListString(GetPalindromes(words)));

        Console.Write("Слова с упорядоченными по алфавиту буквами: ");
        Console.WriteLine(MakeWordsListString(GetOrderedLettersWords(words)));

        Console.WriteLine();

        Console.WriteLine("Самое длинное слово: " + GetLongestOrShortestWord(words, true));
        Console.WriteLine("Самое короткое слово: " + GetLongestOrShortestWord(words, false));

        Console.WriteLine();
    }
}