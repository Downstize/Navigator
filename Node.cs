namespace Navigator;

public class Node
{
    public Map Map { get; set; }
    public Node Next { get; set; }

    public Node(Map map)
    {
        Map = map;
        Next = null;
    }
}