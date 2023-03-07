using System.Collections;

namespace ITIS;

public class WordsEnumerable : IEnumerable<string>
{
    private IEnumerable<string> words;

    public WordsEnumerable(string line)
    {
        words = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .OrderBy(word => word.Length)
            .ThenBy(word => word);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return words.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class WordsEnumerator : IEnumerator<string>
{
    private IEnumerator<string> words;

    public WordsEnumerator(IEnumerable<string> words)
    {
        this.words = words.GetEnumerator();
    }

    public string Current
    {
        get
        {
            return words.Current;
        }
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    bool IEnumerator.MoveNext()
    {
        return words.MoveNext();
    }

    void IEnumerator.Reset()
    {
        words.Reset();
    }

    public void Dispose()
    {
        words.Dispose();
    }
}