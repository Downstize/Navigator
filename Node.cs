namespace Navigator;

public class Node
{
    public Navigator.Map Map { get; set; }
    public Node Next { get; set; }

    public Node(Navigator.Map map)
    {
        Map = map;
        Next = null;
    }
}