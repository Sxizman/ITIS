namespace ITIS;

public class MyStackWithMax<T> where T : IComparable<T>
{
    public const int MaxSizeDefault = 1024;

    private int maxSize;
    private int count;
    private T[] array;

    private int countOfMax;
    private T[] arrayOfMax;

    public MyStackWithMax() : this(MaxSizeDefault)
    {

    }

    public MyStackWithMax(int maxSize)
    {
        if (maxSize < 0)
            throw new ArgumentOutOfRangeException("Stack max size can not be negative");

        this.maxSize = maxSize;
        array = new T[maxSize];
        arrayOfMax = new T[maxSize];
    }

    public void Push(T element)
    {
        if (count == maxSize)
            throw new InvalidOperationException("Stack overflow");

        if (countOfMax == 0 || element.CompareTo(arrayOfMax[countOfMax - 1]) >= 0)
            arrayOfMax[countOfMax++] = element;
        array[count++] = element;
    }

    public T Pop()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

        if (array[count - 1].CompareTo(arrayOfMax[countOfMax - 1]) == 0)
            --countOfMax;
        return array[--count];
    }

    public T Peek()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

        return array[count - 1];
    }

    public void Clear()
    {
        count = 0;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public int Count
    {
        get
        {
            return count;
        }
    }

    public T Max
    {
        get
        {
            return arrayOfMax[countOfMax - 1];
        }
    }
}