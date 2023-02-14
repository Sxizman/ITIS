namespace ITIS;

public class MyStack<T>
{
    public const int MaxSizeDefault = 1024;

    private int maxSize;
    private int count;
    private T[] array;

    public MyStack() : this(MaxSizeDefault)
    {

    }

    public MyStack(int maxSize)
    {
        if (maxSize < 0)
            throw new ArgumentOutOfRangeException("Stack size can not be negative");

        this.maxSize = maxSize;
        array = new T[maxSize];
    }

    public void Push(T element)
    {
        if (count == maxSize)
            throw new InvalidOperationException("Stack overflow");

        array[count++] = element;
    }

    public T Pop()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

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
}