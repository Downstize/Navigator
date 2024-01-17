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
    
    public override bool Equals(object o)
    {
        if (o == null || GetType() != o.GetType())
        {
            return false;
        }

        Route another = (Route)o;
        return Id == another.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
