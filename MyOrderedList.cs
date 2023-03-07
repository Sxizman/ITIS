using System.Collections;

namespace ITIS;

public class MyOrderedList<T> : IEnumerable<T> where T : IComparable<T>
{
    private class MyOrderedListNode
    {
        public T Data;
        public MyOrderedListNode? Next;

        public MyOrderedListNode(T data)
        {
            Data = data;
        }
    }

    private int count;

    private MyOrderedListNode? head;

    public MyOrderedList()
    {

    }

    public void Add(T element)
    {
        var node = new MyOrderedListNode(element);

        var prevNode = (MyOrderedListNode?)null;
        var nextNode = head;
        while (nextNode is not null)
        {
            if (element.CompareTo(nextNode.Data) <= 0)
                break;

            prevNode = nextNode;
            nextNode = nextNode.Next;
        }

        if (prevNode is not null)
            prevNode.Next = node;
        else
            head = node;

        node.Next = nextNode;
        ++count;
    }

    public void Delete(T element)
    {
        var prevNode = (MyOrderedListNode?)null;
        var node = head;
        while (node is not null)
        {
            if (node.Data.Equals(element))
                break;

            prevNode = node;
            node = node.Next;
        }

        if (node is null)
            return;

        if (prevNode is not null)
            prevNode.Next = node.Next;
        else
            head = node.Next;

        --count;
    }

    public void Merge(MyOrderedList<T> list)
    {
        var preHead = new MyOrderedListNode(default!);
        var node = preHead;

        var first = head;
        var second = list.head;
        while (first is not null || second is not null)
        {
            if (second is null || first!.Data.CompareTo(second.Data) < 0)
                node.Next = new MyOrderedListNode(first!.Data);
            else
                node.Next = new MyOrderedListNode(second!.Data);

            node = node.Next;
        }

        head = preHead.Next;
        count += list.count;
    }

    public int Count
    {
        get
        {
            return count;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var node = head;
        while (node is not null)
            yield return node.Data;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}