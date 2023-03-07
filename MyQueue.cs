namespace ITIS;

public class MyQueue<T>
{
    private class MyQueueNode
    {
        public T Data;
        public MyQueueNode? Prev;
        public MyQueueNode? Next;

        public MyQueueNode(T data)
        {
            Data = data;
        }
    }

    private int count;

    private MyQueueNode? head;
    private MyQueueNode? tail;

    public MyQueue()
    {

    }

    public void Enqueue(T element)
    {
        var node = new MyQueueNode(element);
        node.Prev = tail;
        if (tail is not null)
            tail.Next = node;
        else
            head = node;

        ++count;
        tail = node;
    }

    public T Dequeue()
    {
        var node = head ?? throw new InvalidOperationException("Queue is empty");
        if (node.Next is not null)
            node.Next.Prev = null;
        else
            tail = null;

        --count;
        head = node.Next;

        return node.Data;
    }

    public T Peek()
    {
        var node = head ?? throw new InvalidOperationException("Queue is empty");
        return node.Data;
    }

    public void Clear()
    {
        count = 0;
        head = tail = null;
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