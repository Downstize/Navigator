namespace Navigator;

public class Route
{
    public string Id { get; set; }
    public double Distance { get; set; }
    public int Popularity { get; set; }
    public bool IsFavorite { get; set; }
    public List<string> LocationPoints { get; set; }

    public Route(string id, double distance, int popularity, bool isFavorite, List<string> locationPoints)
    {
        Id = id;
        Distance = distance;
        Popularity = popularity;
        IsFavorite = isFavorite;
        LocationPoints = locationPoints;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Route other = (Route)obj;

        return LocationPoints.First() == other.LocationPoints.First() &&
               LocationPoints.Last() == other.LocationPoints.Last() &&
               Math.Abs(Distance - other.Distance) < double.Epsilon;
    }


    public override int GetHashCode()
    {
        unchecked
        {
            int somePrimeNumber = 8;
            int hash = 22;
            hash = somePrimeNumber * hash * 22 + Id.GetHashCode();
            hash = somePrimeNumber * hash * 22 + Distance.GetHashCode();
            hash = somePrimeNumber * hash * 22 + Popularity.GetHashCode();
            hash = somePrimeNumber * hash * 22 + IsFavorite.GetHashCode();

            foreach (var point in LocationPoints)
            {
                hash = somePrimeNumber * hash * 22 + point.GetHashCode();
            }

            return hash;
        }
    }

}
