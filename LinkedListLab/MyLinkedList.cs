using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListLab;

public class MyLinkedList<T> : IEnumerable<T>
{
    public sealed class Node
    {
        public T Value;
        public Node? Next;
        public Node? Prev;
        public Node(T value) { Value = value; }
    }

    public int Count { get; private set; }
    public Node? First { get; private set; }
    public Node? Last { get; private set; }

    public void AddFirst(T value)
    {
        var node = new Node(value);
        if (First == null)
        {
            First = Last = node;
        }
        else
        {
            node.Next = First;
            First.Prev = node;
            First = node;
        }
        Count++;
    }

    public void AddLast(T value)
    {
        var node = new Node(value);
        if (Last == null)
        {
            First = Last = node;
        }
        else
        {
            Last.Next = node;
            node.Prev = Last;
            Last = node;
        }
        Count++;
    }

    public void Clear()
    {
        First = Last = null;
        Count = 0;
    }

    public bool Contains(T value)
    {
        return Find(value) != null;
    }

    public Node? Find(T value)
    {
        var cmp = EqualityComparer<T>.Default;
        for (var cur = First; cur != null; cur = cur.Next)
        {
            if (cmp.Equals(cur.Value, value)) return cur;
        }
        return null;
    }

    public Node? FindLast(T value)
    {
        var cmp = EqualityComparer<T>.Default;
        for (var cur = Last; cur != null; cur = cur.Prev)
        {
            if (cmp.Equals(cur.Value, value)) return cur;
        }
        return null;
    }

    public bool Remove(T value)
    {
        var cmp = EqualityComparer<T>.Default;
        for (var cur = First; cur != null; cur = cur.Next)
        {
            if (cmp.Equals(cur.Value, value))
            {
                RemoveNode(cur);
                return true;
            }
        }
        return false;
    }

    public void RemoveFirst()
    {
        if (First == null) throw new InvalidOperationException("List is empty");
        RemoveNode(First);
    }

    public void RemoveLast()
    {
        if (Last == null) throw new InvalidOperationException("List is empty");
        RemoveNode(Last);
    }

    private void RemoveNode(Node node)
    {
        var prev = node.Prev;
        var next = node.Next;

        if (prev != null) prev.Next = next; else First = next;
        if (next != null) next.Prev = prev; else Last = prev;

        node.Next = null;
        node.Prev = null;
        Count--;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var cur = First; cur != null; cur = cur.Next)
        {
            yield return cur.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
