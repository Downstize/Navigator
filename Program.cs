namespace Navigator
{
    class Program
    {
        static void Main(string[] args)
        {
            NavigatorImpl navigator = new NavigatorImpl();

            InitializeRoutes(navigator);

            DisplayTotalRoutes(navigator);

            DisplayAllRoutes(navigator);

            DisplayRouteById(navigator, "7");

            SearchAndDisplayRoutes(navigator, "Владивосток", "Чита");

            IncreaseRoutePopularity(navigator, "8");

            DisplayTop3Routes(navigator);

            RemoveAndCheckRoute(navigator, "2");

            DisplayFavoriteRoutes(navigator, "Москва");
        }

        static void InitializeRoutes(NavigatorImpl navigator)
        {
            navigator.addRoute(new Route("1", 100.0, 5, true,
                new List<string> { "Москва", "Тверь", "Санкт-Петербург" }));
            navigator.addRoute(new Route("2", 150.0, 3, false,
                new List<string> { "Сочи", "Краснодар", "Ростов" }));
            navigator.addRoute(new Route("3", 75.0, 7, true,
                new List<string> { "Казань", "Нижний Новгород", "Самара" }));
            navigator.addRoute(new Route("4", 200.0, 2, false,
                new List<string> { "Владивосток", "Хабаровск", "Чита", "Иркутск" }));
            navigator.addRoute(new Route("5", 500.0, 4, true,
                new List<string> { "Ярославль", "Москва", "Тула", "Воронеж" }));
            navigator.addRoute(new Route("6", 350.0, 1, false,
                new List<string> { "Саратов", "Пенза", "Чебоксары", "Казань" }));
            navigator.addRoute(new Route("7", 600.0, 6, true,
                new List<string> { "Екатеринбург", "Тюмень", "Омск", "Новосибирск" }));
            navigator.addRoute(new Route("8", 280.0, 5, false,
                new List<string> { "Санкт-Петербург", "Великие Луки", "Псков" }));
            navigator.addRoute(new Route("9", 400.0, 3, true,
                new List<string> { "Калининград", "Вильнюс", "Гродно", "Минск" }));
            navigator.addRoute(new Route("10", 450.0, 2, false,
                new List<string> { "Уфа", "Оренбург", "Самара" }));
            navigator.addRoute(new Route("11", 800.0, 4, false,
                new List<string> { "Мурманск", "Кандалакша", "Кемь", "Архангельск" }));
            navigator.addRoute(new Route("12", 600.0, 5, true,
                new List<string> { "Новгород", "Псков", "Рига", "Калининград" }));
            navigator.addRoute(new Route("13", 400.0, 2, false,
                new List<string> { "Волгоград", "Элиста", "Астрахань" }));
        }

        static void DisplayTotalRoutes(NavigatorImpl navigator)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine($"Кол-во маршрутов: {navigator.size()}");
        }

        static void DisplayRouteById(NavigatorImpl navigator, string routeId)
        {
            Route route = navigator.getRoute(routeId);
            if (route != null)
            {
                Console.WriteLine($"Маршрут {route.Id}: Дистанция - {route.Distance}");
                Console.WriteLine("--------------------");
            }
            else
            {
                Console.WriteLine($"Маршрут {routeId} не был найден!");
                Console.WriteLine("--------------------");
            }
        }

        static void SearchAndDisplayRoutes(NavigatorImpl navigator, string start, string end)
        {
            Console.WriteLine($"Маршурт из '{start}' в '{end}':");
            Console.WriteLine("---");

            bool routeFound = false;
            foreach (var r in navigator.searchRoutes(start, end))
            {
                Console.WriteLine($"Маршрут {r.Id}: Дистанция - {r.Distance}, Популярность маршрута - {r.Popularity}, Города: {string.Join(",",r.LocationPoints)}");
                routeFound = true;
            }

            if (!routeFound)
            {
                Console.WriteLine($"Маршрут из '{start}' в '{end}' не были найдены!");
            }

            Console.WriteLine("--------------------");
        }
        
        static void IncreaseRoutePopularity(NavigatorImpl navigator, string routeId)
        {
            navigator.chooseRoute(routeId);
        }

        static void DisplayTop3Routes(NavigatorImpl navigator)
        {
            Console.WriteLine("5 самых популярных маршрутов:");
            Console.WriteLine("---");
            foreach (var r in navigator.getTop5Routes())
            {
                Console.WriteLine($"Маршрут {r.Id}: Популярность маршрута - {r.Popularity}, Дистанция - {r.Distance}");
            }

            Console.WriteLine("--------------------");
        }

        static void RemoveAndCheckRoute(NavigatorImpl navigator, string routeId)
        {
            navigator.removeRoute(routeId);
            Console.WriteLine($"Маршрут {routeId} существует? Ответ: {navigator.contains(new Route
                (routeId, 0, 0, false, new List<string>()))}");
        }

        static void DisplayFavoriteRoutes(NavigatorImpl navigator, string destination) ///
        {
            Console.WriteLine($"Любимые маршруты с пункотом '{destination}':");
            foreach (var r in navigator.getFavoriteRoutes(destination))
            {
                Console.WriteLine($"Маршрут {r.Id}: Дистанция - {r.Distance}, Популярность маршрута - {r.Popularity}");
            }
        }

        static void DisplayAllRoutes(NavigatorImpl navigator)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Все маршруты:");
            foreach (var r in navigator.getAllRoutes())
            {
                Console.WriteLine($"Маршрут {r.Id}: Дистанция - {r.Distance}, Популярность маршрута - {r.Popularity}");
            }

            Console.WriteLine("--------------------");
        }
    }
}