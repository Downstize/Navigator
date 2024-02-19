namespace Navigator
{
    class Program
    {
        static void Main(string[] args)
        {
            Navigator navigator = new Navigator();

            InitRoutes(navigator);

            TypeRoutes(navigator);

            CountAllRoutes(navigator);

            DisplayAllRoutes(navigator);

            FindRouteByValue(navigator, "3");

            SearchRoute(navigator, "Владивосток", "Чита");

            IncreaseRoutePopularity(navigator, "8");

            DisplayTop5Routes(navigator);

            RemoveAndCheckRoute(navigator, "2");

            DisplayAllRoutes(navigator);

            DisplayFavoriteRoutes(navigator, "Москва");
        }

        static void TypeRoutes(Navigator navigator)
        {
            while (true)
            {
                Console.WriteLine("Укажите id для нового маршрута: ");
                string id = Console.ReadLine();

                Console.WriteLine("Укажите дистанцию: ");
                double distance = double.Parse(Console.ReadLine());

                Console.WriteLine("Популярность маршрута: ");
                int popularity = int.Parse(Console.ReadLine());

                Console.WriteLine("Маршрут является любимым? (да/нет): ");
                string response = Console.ReadLine();
                bool isFavourite;

                if (response.ToLower() == "да")
                {
                    isFavourite = true;
                }
                else
                {
                    isFavourite = false;
                }

                Console.WriteLine("Введите точки останова находящиеся в маршруте: ");
                List<string> locationPoints = new List<string>(Console.ReadLine().Split(","));

                Route newRoute = new Route(id, distance, popularity, isFavourite, locationPoints);
                if (!navigator.contains(newRoute))
                {
                    navigator.addRoute(newRoute);
                    Console.WriteLine("Маршрут был успешно добавлен!");
                }
                else
                {
                    Console.WriteLine("Маршрут с такими атрибутами уже существует!");
                }


                Console.WriteLine("Хотите добавить ещё один маршрут? (да/нет): ");
                string resp = Console.ReadLine();

                if (resp.ToLower() != "да")
                    break;
            }
        }

        static void InitRoutes(Navigator navigator)
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
            navigator.addRoute(new Route("12", 200.0, 2, true,
            new List<string> { "Владивосток", "Хабаровск", "Иркутск", "Чита" }));
        }

        static void CountAllRoutes(Navigator navigator)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine($"Кол-во маршрутов: {navigator.sizeAll()}");
        }

        static void FindRouteByValue(Navigator navigator, string routeId)
        {
            Route route = navigator.getRoute(routeId);
            if (route != null)
            {
                Console.WriteLine($"Маршрут {route.Id}: Дистанция - {route.Distance}, Популярность маршрута - {route.Popularity}, Города: {string.Join(",", route.LocationPoints)}");
                Console.WriteLine("--------------------");
            }
            else
            {
                Console.WriteLine($"Маршрут с ID {routeId} не был найден!");
                Console.WriteLine("--------------------");
            }
        }


        static void SearchRoute(Navigator navigator, string start, string end)
        {
            Console.WriteLine($"Маршурт из '{start}' в '{end}':");
            Console.WriteLine("---");

            bool routeFound = false;
            foreach (var r in navigator.searchRoutes(start, end))
            {
                Console.WriteLine($"Маршрут {r.Id}: Дистанция - {r.Distance}, Популярность маршрута - {r.Popularity}, Города: {string.Join(",", r.LocationPoints)}");
                routeFound = true;
            }

            if (!routeFound)
            {
                Console.WriteLine($"Маршрут из '{start}' в '{end}' не были найдены!");
            }

            Console.WriteLine("--------------------");
        }

        static void IncreaseRoutePopularity(Navigator navigator, string routeId)
        {
            navigator.chooseRoute(routeId);
        }

        static void DisplayTop5Routes(Navigator navigator)
        {
            Console.WriteLine("5 самых популярных маршрутов:");
            Console.WriteLine("---");
            foreach (var r in navigator.getTop5Routes())
            {
                Console.WriteLine($"Маршрут {r.Id}: Популярность маршрута - {r.Popularity}, Дистанция - {r.Distance}");
            }

            Console.WriteLine("--------------------");
        }

        static void RemoveAndCheckRoute(Navigator navigator, string routeId)
        {
            Route route = navigator.getRoute(routeId);

            if (route != null)
            {
                Console.WriteLine($"Удаление маршрута №{routeId}");
                navigator.removeRoute(route.Id);
                Console.WriteLine("---");

                route = navigator.getRoute(routeId);
                Console.WriteLine($"Маршрут №{routeId} существует? Ответ: {(route != null ? "Да" : "Нет")}");
                Console.WriteLine("--------------------");
            }
            else
            {
                Console.WriteLine($"Маршрут с ID {routeId} не найден!");
            }
        }





        static void DisplayFavoriteRoutes(Navigator navigator, string destination)
        {
            Console.WriteLine($"Любимый(е) маршруты с пунктом '{destination}':");
            Console.WriteLine("---");
            foreach (var r in navigator.getFavoriteRoutes(destination))
            {
                Console.WriteLine($"Маршрут {r.Id}: Дистанция - {r.Distance}, Популярность маршрута - {r.Popularity}");
            }
        }

        static void DisplayAllRoutes(Navigator navigator)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Все маршруты:");
            foreach (var r in navigator.getAllRoutes())
            {
                Console.WriteLine($"Маршрут {r.Id}: Дистанция - {r.Distance}, Популярность маршрута - {r.Popularity}, Города: {string.Join(",", r.LocationPoints)}");
            }

            Console.WriteLine("--------------------");
        }
    }
}