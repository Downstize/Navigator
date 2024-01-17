namespace Navigator;

public class Map<K, V>
{
    private class Node
    {
        public K Key;
        public V Value;
        public Node Next;

        public Node(K key, V value)
        {
            this.Key = key;
            this.Value = value;
            this.Next = null;
        }
    }

    private Node head;

    public void Put(K key, V value)
    {
        Node newNode = new Node(key, value);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Node current = head;
        while (current.Next != null)
        {
            if (current.Key.Equals(key))
            {
                current.Value = value;
                return;
            }

            current = current.Next;
        }

        if (current.Key.Equals(key))
        {
            current.Value = value;
        }
        else
        {
            current.Next = newNode;
        }
    }

    public V Get(K key)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                return current.Value;
            }

            current = current.Next;
        }

        return default(V); // или бросить исключение, если ключ не найден
    }

    public bool Remove(K key)
    {
        if (head == null)
        {
            return false;
        }

        if (head.Key.Equals(key))
        {
            head = head.Next;
            return true;
        }

        Node current = head;
        while (current.Next != null)
        {
            if (current.Next.Key.Equals(key))
            {
                current.Next = current.Next.Next;
                return true;
            }

            current = current.Next;
        }

        return false;
    }
}