namespace WeatherApp;

public readonly struct Coordinates
{
    public double Latitude { get; init; } = 0;
    public double Longitude { get; init; } = 0;

    public Coordinates(double lat, double lon) => (Latitude, Longitude) = (lat, lon);

    public string LatLong() => $"{Latitude},{Longitude}";
}

public class Location
{
    // create enum instances
    public static readonly Location AMSTERDAM = new Location("Amsterdam, Netherlands", new Coordinates(52.370216, 4.895168));
    public static readonly Location BANGKOK = new Location("Bangkok, Thailand", new Coordinates(13.756331, 100.501762));
    public static readonly Location BARCELONA = new Location("Barcelona, Spain", new Coordinates(41.385063, 2.173404));
    public static readonly Location DELHI = new Location("Delhi, India", new Coordinates(28.704060, 77.102493));
    public static readonly Location DUBAI = new Location("Dubai, United Arab Emirates", new Coordinates(25.0742823, 55.1885387));
    public static readonly Location HONG_KONG = new Location("Hong Kong, China", new Coordinates(22.2793278, 114.1628131));
    public static readonly Location ISTANBUL = new Location("Istanbul, Turkey", new Coordinates(41.0091982, 28.9662187));
    public static readonly Location KUALA_LUMPUR = new Location("Kuala Lumpur, Malaysia", new Coordinates(3.1516964, 101.6942371));
    public static readonly Location LONDON = new Location("London, England", new Coordinates(51.5073219, -0.1276474));
    public static readonly Location MEXICO_CITY = new Location("Mexico City, Mexico", new Coordinates(19.4326296, -99.1331785));
    public static readonly Location MOSCOW = new Location("Moscow, Russia", new Coordinates(55.7504461, 37.6174943));
    public static readonly Location NEW_YORK_CITY = new Location("New York City, USA", new Coordinates(40.7127281, -74.0060152));
    public static readonly Location NULL_ISLAND = new Location("Null Island", new Coordinates());
    public static readonly Location PARIS = new Location("Paris, France", new Coordinates(48.8588897, 2.320041));
    public static readonly Location PORTO = new Location("Porto, Portugal", new Coordinates(41.1494512, -8.6107884));
    public static readonly Location ROME = new Location("Rome, Italy", new Coordinates(41.8933203, 12.4829321));
    public static readonly Location SINGAPORE = new Location("Singapore", new Coordinates(1.357107, 103.8194992));
    public static readonly Location SYDNEY = new Location("Sydney, Australia", new Coordinates(-33.8698439, 151.2082848));
    public static readonly Location TOKYO = new Location("Tokyo, Japan", new Coordinates(35.6828387, 139.7594549));
    public static readonly Location VIENNA = new Location("Vienna, Austria", new Coordinates(48.2083537, 16.3725042));

    // assign values to the enums
    public string Name { get; private set; }
    public Coordinates Coordinates { get; private set; }
    private Location(string name, Coordinates coordinates) => (Name, Coordinates) = (name, coordinates);

    // make enum iterable
    public static IEnumerable<Location> Values
    {
        get
        {
            yield return AMSTERDAM;
            yield return BANGKOK;
            yield return BARCELONA;
            yield return DELHI;
            yield return DUBAI;
            yield return HONG_KONG;
            yield return ISTANBUL;
            yield return KUALA_LUMPUR;
            yield return LONDON;
            yield return MEXICO_CITY;
            yield return MOSCOW;
            yield return NEW_YORK_CITY;
            yield return NULL_ISLAND;
            yield return PARIS;
            yield return PORTO;
            yield return ROME;
            yield return SINGAPORE;
            yield return SYDNEY;
            yield return TOKYO;
            yield return VIENNA;
        }
    }
}
