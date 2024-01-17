namespace Navigator;

public class NavigatorImpl : Navigator
{
    private class Node
    {
        public string Key;
        public Route Value;
        public Node Next;

        public Node(string key, Route value, Node next = null)
        {
            this.Key = key;
            this.Value = value;
            this.Next = next;
        }
    }

    private Node head = null;
    private List<Route> routesList = new List<Route>();

    public void addRoute(Route route)
    {
        if (route == null || contains(route))
        {
            return;
        }

        head = new Node(route.Id, route, head);
        routesList.Add(route);
    }

    public void removeRoute(string routeId)
    {
        if (string.IsNullOrEmpty(routeId))
        {
            return;
        }

        if (head == null)
        {
            return;
        }

        if (head.Key.Equals(routeId))
        {
            routesList.Remove(head.Value);
            head = head.Next;
            return;
        }

        Node current = head;
        while (current.Next != null)
        {
            if (current.Next.Key.Equals(routeId))
            {
                routesList.Remove(current.Next.Value);
                current.Next = current.Next.Next;
                return;
            }

            current = current.Next;
        }
    }

    public bool contains(Route route)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Key.Equals(route.Id))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public int size()
    {
        return routesList.Count;
    }

    public Route getRoute(string routeId)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Key.Equals(routeId))
            {
                return current.Value;
            }

            current = current.Next;
        }

        return null;
    }

    public void chooseRoute(string routeId)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Key.Equals(routeId))
            {
                current.Value.Popularity++;
                return;
            }
            current = current.Next;
        }
    }


    public IEnumerable<Route> searchRoutes(string startPoint, string endPoint)
    {
        return routesList
            .Where(route => route.LocationPoints.Contains(startPoint) && route.LocationPoints.Contains(endPoint))
            .OrderByDescending(route => route.IsFavorite)
            .ThenBy(route => Math.Abs(route.LocationPoints.IndexOf(startPoint) - route.LocationPoints.IndexOf(endPoint)))
            .ThenByDescending(route => route.Popularity);
    }


    public IEnumerable<Route> getFavoriteRoutes(string destinationPoint)
    {
        var favoriteRoutes = routesList.Where(route =>
            route.IsFavorite && 
            route.LocationPoints.Contains(destinationPoint) && 
            route.LocationPoints.First() != destinationPoint);

        var sortedRoutes = favoriteRoutes.OrderBy(route => route.Distance)
            .ThenByDescending(route => route.Popularity);

        return sortedRoutes;
    }

    
    public IEnumerable<Route> getTop5Routes()
    {
        return routesList
            .OrderByDescending(route => route.Popularity)
            .ThenBy(route => route.Distance)
            .ThenBy(route => route.LocationPoints.Count)
            .Take(5);
    }

    
    public IEnumerable<Route> getAllRoutes()
    {
        Node current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }
}