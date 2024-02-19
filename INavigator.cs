namespace Navigator;

public interface INavigator
{
    void addRoute(Route route);

    void removeRoute(string routeId);

    bool contains(Route route);

    int sizeAll();

    Route getRoute(string routeId);

    void chooseRoute(string routeId);

    IEnumerable<Route> searchRoutes(string startPoint, string endPoint);

    IEnumerable<Route> getFavoriteRoutes(string destinationPoint);

    IEnumerable<Route> getTop5Routes();

    IEnumerable<Route> getAllRoutes();
}
