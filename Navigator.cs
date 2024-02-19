namespace Navigator
{
    public class Navigator : INavigator
    {

        private LinkedList[] map;
        LinkedList linkedList = new LinkedList();
        private int size;

        public Navigator()
        {
            map = new LinkedList[20];
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

            if (map[index] == null)
            {
                map[index] = new LinkedList();
            }

            Map existingMap = map[index].Find(routeId);
            if (existingMap == null)
            {
                var kvp = new Map(routeId, route);
                map[index].Add(kvp);
                size++;
            }

            if (size >= map.Length * 0.75)
            {
                var temp = map;
                map = new LinkedList[map.Length * 2];
                foreach (var linkedList in temp.Where(linkedList => linkedList != null))
                {
                    foreach (var current in linkedList)
                    {
                        int innerIndex = GetIndex(current.Key);
                        if (map[innerIndex] == null)
                        {
                            map[innerIndex] = new LinkedList();
                        }
                        map[innerIndex].Add(current);
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

            LinkedList linkedList = map[index];
            if (linkedList != null)
            {
                Map mapToRemove = linkedList.FirstOrDefault(item => item.Key.Equals(hashedRouteId));
                if (mapToRemove != null)
                {
                    linkedList.Remove(mapToRemove);
                    size--;
                }
            }
        }
        public bool contains(Route route)
        {
            foreach (LinkedList linkedList in map)
            {
                if (linkedList != null)
                {
                    foreach (Map map in linkedList)
                    {
                        if (map.Value.Equals(route))
                        {
                            return true;
                        }
                    }
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

            LinkedList linkedList = map[index];
            if (linkedList != null)
            {
                Map mapToFind = linkedList.Find(hashedRouteId);
                if (mapToFind != null)
                {
                    return mapToFind.Value;
                }
            }

            return null;
        }

        public void chooseRoute(string routeId)
        {
            string hashedRouteId = routeId.GetHashCode().ToString();
            int index = GetIndex(hashedRouteId);

            LinkedList linkedList = map[index];
            if (linkedList != null)
            {
                Map mapToFind = linkedList.Find(hashedRouteId);
                if (mapToFind != null)
                {
                    mapToFind.Value.Popularity++;
                }
            }
        }
        public IEnumerable<Route> getAllRoutes()
        {
            List<Route> routesList = new List<Route>();
            foreach (LinkedList linkedList in map)
            {
                if (linkedList != null)
                {
                    foreach (Map map in linkedList)
                    {
                        routesList.Add(map.Value);
                    }
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