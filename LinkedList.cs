using System.Collections;

namespace Navigator
{
    public class LinkedList : IEnumerable<Map>
    {
        public Node Head { get; private set; }

        public LinkedList()
        {
            Head = null;
        }

        public void Add(Map map)
        {
            var node = new Node(map);
            if (Head == null)
            {
                Head = node;
            }
            else
            {
                var current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node;
            }
        }

        public bool Remove(Map map)
        {
            if (Head == null)
            {
                return false;
            }

            if (Head.Map == map)
            {
                Head = Head.Next;
                return true;
            }

            var current = Head;
            while (current.Next != null)
            {
                if (current.Next.Map == map)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

       public Map Find(string key)
{
    var current = Head;
    while (current != null)
    {
        if (current.Map.Key == key)
        {
            return current.Map;
        }
        current = current.Next;
    }
    return null;
}
        public IEnumerator<Map> GetEnumerator()
        {
            Node current = Head;
            while (current != null)
            {
                yield return current.Map;
                current = current.Next;
            }
        }

        IEnumerator<Map> IEnumerable<Map>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}