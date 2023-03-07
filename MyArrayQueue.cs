namespace ITIS;

public class MyArrayQueue<T>
{
    public const int MaxSizeDefault = 1024;

    private int maxSize;
    private int count;
    private T[] array;

    public MyArrayQueue() : this(MaxSizeDefault)
    {

    }

    public MyArrayQueue(int maxSize)
    {
        if (maxSize < 0)
            throw new ArgumentOutOfRangeException("Queue max size can not be negative");

        this.maxSize = maxSize;
        array = new T[maxSize];
    }

    public void Enqueue(T element)
    {
        if (count == maxSize)
            throw new InvalidOperationException("Queue overflow");

        array[count++] = element;
    }

    public T Dequeue()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

        var element = array[0];
        --count;

        for (var i = 0; i < count; ++i)
            array[i] = array[i + 1];

        return element;
    }

    public T Peek()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

        return array[0];
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