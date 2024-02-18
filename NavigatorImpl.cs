namespace Navigator
{
    public class NavigatorImpl : Navigator
    {
        private class KeyValuePair
        {
            public string Key;
            public Route Value;
            public KeyValuePair Next;

            public KeyValuePair(string key, Route value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private KeyValuePair[] map;
        private int size;

        public NavigatorImpl()
        {
            map = new KeyValuePair[20];
        }

        private int GetIndex(string key)
        {
            return Math.Abs(key.GetHashCode()) % map.Length;
        }

        public void addRoute(Route route)
        {
            if (route == null || contains(route))
            {
                throw new InvalidOperationException("Маршрут уже существует или не может быть добавлен!");
            }

            string routeId = route.Id.GetHashCode().ToString();
            int index = GetIndex(routeId);

            var kvp = new KeyValuePair(routeId, route)
            {
                Next = map[index]
            };
            map[index] = kvp;
            size++;

            if (size >= map.Length * 0.7)
            {
                var temp = map;
                map = new KeyValuePair[map.Length * 2];
                foreach (var pair in temp)
                {
                    var current = pair;
                    while (current != null)
                    {
                        index = GetIndex(current.Key);
                        var next = current.Next;
                        current.Next = map[index];
                        map[index] = current;
                        current = next;
                    }
                }
            }
        }

        public void removeRoute(string routeId)
        {
            if (string.IsNullOrEmpty(routeId))
            {
                return;
            }

            string hashedRouteId = routeId.GetHashCode().ToString();
            int index = GetIndex(hashedRouteId);

            KeyValuePair prev = null;
            KeyValuePair current = map[index];
            while (current != null)
            {
                if (current.Key.Equals(hashedRouteId))
                {
                    if (prev == null)
                    {
                        map[index] = current.Next;
                    }
                    else
                    {
                        prev.Next = current.Next;
                    }
                    size--;
                    return;
                }
                prev = current;
                current = current.Next;
            }
        }

        public bool contains(Route route)
        {
            foreach (KeyValuePair kvp in map)
            {
                KeyValuePair current = kvp;
                while (current != null)
                {
                    if (current.Value.Equals(route))
                    {
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        public int sizeAll()
        {
            return size;
        }

        public Route getRoute(string routeId)
        {
            string hashedRouteId = routeId.GetHashCode().ToString();
            int index = GetIndex(hashedRouteId);

            KeyValuePair current = map[index];
            while (current != null)
            {
                if (current.Key.Equals(hashedRouteId))
                {
                    return current.Value;
                }
                current = current.Next;
            }

            return null;
        }

        public void chooseRoute(string routeId)
        {
            string hashedRouteId = routeId.GetHashCode().ToString();
            int index = GetIndex(hashedRouteId);

            KeyValuePair current = map[index];
            while (current != null)
            {
                if (current.Key.Equals(hashedRouteId))
                {
                    current.Value.Popularity++;
                    return;
                }
                current = current.Next;
            }
        }
        public IEnumerable<Route> getAllRoutes()
        {
            List<Route> routesList = new List<Route>();
            foreach (KeyValuePair kvp in map)
            {
                KeyValuePair current = kvp;
                while (current != null)
                {
                    routesList.Add(current.Value);
                    current = current.Next;
                }
            }
            return routesList;
        }

        public IEnumerable<Route> searchRoutes(string startPoint, string endPoint)
        {
            return getAllRoutes()
                .Where(route => route.LocationPoints.Contains(startPoint) && route.LocationPoints.Contains(endPoint))
                .OrderByDescending(route => route.IsFavorite)
                .ThenBy(route => Math.Abs(route.LocationPoints.IndexOf(startPoint) - route.LocationPoints.IndexOf(endPoint)))
                .ThenByDescending(route => route.Popularity);
        }

        public IEnumerable<Route> getFavoriteRoutes(string destinationPoint)
        {
            return getAllRoutes().Where(route =>
                route.IsFavorite &&
                route.LocationPoints.Contains(destinationPoint) &&
                route.LocationPoints.First() != destinationPoint)
                .OrderBy(route => route.Distance)
                .ThenByDescending(route => route.Popularity);
        }

        public IEnumerable<Route> getTop5Routes()
        {
            return getAllRoutes()
                .OrderByDescending(route => route.Popularity)
                .ThenBy(route => route.Distance)
                .ThenBy(route => route.LocationPoints.Count)
                .Take(5);
        }

    }
}