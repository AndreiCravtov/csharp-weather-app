using System.Text.Json;

namespace WeatherApp;

public static class WeatherAPI
{
    // delete key before pushing code
    public static readonly string APIKey = "PLACE YOUR API KEY HERE"; // get one at https://openweathermap.org/api
    private static HttpClient _httpClient = new HttpClient();

    public static DataObjects.CurrentWeatherData GetCurrentWeatherData(Location location)
    {
        DataObjects.CurrentWeatherData weatherData;
        string requestResult = GetRequest($"https://api.openweathermap.org/data/2.5/weather?lat={location.Coordinates.Latitude}&lon={location.Coordinates.Longitude}&appid={APIKey}&units=metric");   
        using (JsonDocument document = JsonDocument.Parse(requestResult))
        {
            JsonElement root = document.RootElement;

            // get time zone, time stamp, sunrise time, sunset time in local time
            DateTime dateTime;
            DateTime unixBase = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            int timeZoneSeconds = root.GetProperty("timezone").GetInt32();
            unixBase = unixBase.AddSeconds(timeZoneSeconds);
            string timeZone = $"UTC{((timeZoneSeconds<0)?"-":"+")}{$"{timeZoneSeconds.GetAbs()/3600}".PadLeft(2, '0')}:{$"{(timeZoneSeconds.GetAbs()%3600)/60}".PadLeft(2, '0')}:{$"{(timeZoneSeconds.GetAbs()%3600)%60}".PadLeft(2, '0')}";

            int timeCalculatedUnixTime = root.GetProperty("dt").GetInt32();
            dateTime = unixBase.AddSeconds(timeCalculatedUnixTime);
            string dateCalculated = dateTime.ToString();

            JsonElement sys = root.GetProperty("sys");
            int sunriseTimeUnixTime = sys.GetProperty("sunrise").GetInt32();
            dateTime = unixBase.AddSeconds(sunriseTimeUnixTime);
            string sunriseTime = dateTime.ToString("HH:mm");

            int sunsetTimeUnixTime = sys.GetProperty("sunset").GetInt32();
            dateTime = unixBase.AddSeconds(sunsetTimeUnixTime);
            string sunsetTime = dateTime.ToString("HH:mm");

            // get main and description
            JsonElement weather = root.GetProperty("weather").EnumerateArray().ElementAt(0);
            string main = weather.GetProperty("main").GetString()!.ToUpper();
            string description = weather.GetProperty("description").GetString()!;

            // get temp info, pressure and humidity
            JsonElement mainInfo = root.GetProperty("main");
            double temperatureAverage = mainInfo.GetProperty("temp").GetDouble();
            double temperatureMinimum = mainInfo.GetProperty("temp").GetDouble();
            double temperatureMaximum = mainInfo.GetProperty("temp").GetDouble();
            double temperatureFeelsLike = mainInfo.GetProperty("temp").GetDouble();
            double humidity = mainInfo.GetProperty("humidity").GetDouble();
            double atmosphericPressure = mainInfo.GetProperty("pressure").GetDouble();

            // get wind info
            JsonElement wind = root.GetProperty("wind");
            double windSpeed = wind.GetProperty("speed").GetDouble();
            double windDirection = wind.GetProperty("deg").GetDouble();

            // get cloudiness
            double cloudiness = root.GetProperty("clouds").GetProperty("all").GetDouble();

            // get visibility
            double visibility = root.GetProperty("visibility").GetDouble();

            weatherData = new DataObjects.CurrentWeatherData(
                dateCalculated,
                main,
                description,
                temperatureAverage,
                temperatureMinimum,
                temperatureMaximum,
                temperatureFeelsLike,
                windSpeed,
                windDirection,
                sunriseTime,
                sunsetTime,
                humidity,
                cloudiness,
                visibility,
                atmosphericPressure,
                timeZone
            );
        }

        return weatherData;
    }

    public static DataObjects.ForecastWeatherData GetForecastWeatherData(Location location)
    {
        DataObjects.ForecastWeatherData weatherData;
        string requestResult = GetRequest($"https://api.openweathermap.org/data/2.5/forecast?lat={location.Coordinates.Latitude}&lon={location.Coordinates.Longitude}&appid={APIKey}&units=metric");

        string[] timeStamps = new string[40];
        string[] mains = new string[40];
        string[] descriptions = new string[40];
        double[] temperatureAverages = new double[40];
        double[] temperatureMinimums = new double[40];
        double[] temperatureMaximums = new double[40];
        double[] temperatureFeelsLike = new double[40];
        double[] windSpeeds = new double[40];
        double[] windDirections = new double[40];
        double[] humidities = new double[40];
        double[] cloudinesses = new double[40];
        double[] visibilities = new double[40];
        double[] atmosphericPressures = new double[40];
        using (JsonDocument document = JsonDocument.Parse(requestResult))
        {
            JsonElement root = document.RootElement;

            // get time zone, sunrise time, sunset time in local time
            DateTime dateTime;
            DateTime unixBase = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            JsonElement city = root.GetProperty("city");
            int timeZoneSeconds = city.GetProperty("timezone").GetInt32();
            unixBase = unixBase.AddSeconds(timeZoneSeconds);
            string timeZone = $"UTC{((timeZoneSeconds < 0) ? "-" : "+")}{$"{timeZoneSeconds.GetAbs() / 3600}".PadLeft(2, '0')}:{$"{(timeZoneSeconds.GetAbs() % 3600) / 60}".PadLeft(2, '0')}:{$"{(timeZoneSeconds.GetAbs() % 3600) % 60}".PadLeft(2, '0')}";

            // iterate over each item in the list
            int iterator = 0;
            foreach (var item in root.GetProperty("list").EnumerateArray())
            {
                // time stamp in local time
                int timeCalculatedUnixTime = item.GetProperty("dt").GetInt32();
                dateTime = unixBase.AddSeconds(timeCalculatedUnixTime);
                timeStamps[iterator] = dateTime.ToString();

                // get main and description
                JsonElement weather = item.GetProperty("weather").EnumerateArray().ElementAt(0);
                mains[iterator] = weather.GetProperty("main").GetString()!.ToUpper();
                descriptions[iterator] = weather.GetProperty("description").GetString()!;

                // get temp info, pressure and humidity
                JsonElement mainInfo = item.GetProperty("main");
                temperatureAverages[iterator] = mainInfo.GetProperty("temp").GetDouble();
                temperatureMinimums[iterator] = mainInfo.GetProperty("temp").GetDouble();
                temperatureMaximums[iterator] = mainInfo.GetProperty("temp").GetDouble();
                temperatureFeelsLike[iterator] = mainInfo.GetProperty("temp").GetDouble();
                humidities[iterator] = mainInfo.GetProperty("humidity").GetDouble();
                atmosphericPressures[iterator] = mainInfo.GetProperty("pressure").GetDouble();

                // get wind info
                JsonElement wind = item.GetProperty("wind");
                windSpeeds[iterator] = wind.GetProperty("speed").GetDouble();
                windDirections[iterator] = wind.GetProperty("deg").GetDouble();

                // get cloudiness
                cloudinesses[iterator] = item.GetProperty("clouds").GetProperty("all").GetDouble();

                // get visibility
                visibilities[iterator] = item.GetProperty("visibility").GetDouble();

                iterator++;
            }

            weatherData = new DataObjects.ForecastWeatherData(
                timeStamps,
                mains,
                descriptions,
                temperatureAverages,
                temperatureMinimums,
                temperatureMaximums,
                temperatureFeelsLike,
                windSpeeds,
                windDirections,
                humidities,
                cloudinesses,
                visibilities,
                atmosphericPressures,
                timeZone
            );
        }

        return weatherData;
    }
    
    private static string GetRequest(string uri)
    {
        HttpContent httpContent = _httpClient.Send(new HttpRequestMessage(HttpMethod.Get, uri)).Content;
        Task<string> task = httpContent.ReadAsStringAsync(); task.Wait();
        return task.Result;
    }

    public static class DataObjects
    {
        public readonly struct CurrentWeatherData {
            public WeatherData WeatherData { get; init; }
            public string SunriseTime { get; init; } = "";
            public string SunsetTime { get; init; } = "";
            public string TimeZone { get; init; } = "";

            public CurrentWeatherData(
                string timeStamp,
                string main,
                string description,
                double temperatureAverage,
                double temperatureMinimum,
                double temperatureMaximum,
                double temperatureFeelsLike,
                double windSpeed,
                double windDirection,
                string sunriseTime,
                string sunsetTime,
                double humidity,
                double cloudiness,
                double visibility,
                double atmosphericPressure,
                string timeZone
            )
            {
                WeatherData = new WeatherData(
                    timeStamp,
                    main,
                    description,
                    temperatureAverage,
                    temperatureMinimum,
                    temperatureMaximum,
                    temperatureFeelsLike,
                    windSpeed,
                    windDirection,
                    humidity,
                    cloudiness,
                    visibility,
                    atmosphericPressure
                );
                SunriseTime = sunriseTime;
                SunsetTime = sunsetTime;
                TimeZone = timeZone;
            }
        }

        public readonly struct ForecastWeatherData
        {
            public WeatherData[] WeatherData { get; init; }
            public string TimeZone { get; init; } = "";

            public ForecastWeatherData(
                string[] timeStamps,
                string[] mains,
                string[] descriptions,
                double[] temperatureAverages,
                double[] temperatureMinimums,
                double[] temperatureMaximums,
                double[] temperatureFeelsLike,
                double[] windSpeeds,
                double[] windDirections,
                double[] humidities,
                double[] cloudinesses,
                double[] visibilities,
                double[] atmosphericPressures,
                string timeZone
            )
            {
                WeatherData = new WeatherData[40];
                for (int i=0; i<40; i++)
                {
                    WeatherData[i] = new WeatherData(
                        timeStamps[i],
                        mains[i],
                        descriptions[i],
                        temperatureAverages[i],
                        temperatureMinimums[i],
                        temperatureMaximums[i],
                        temperatureFeelsLike[i],
                        windSpeeds[i],
                        windDirections[i],
                        humidities[i],
                        cloudinesses[i],
                        visibilities[i],
                        atmosphericPressures[i]
                    );
                }
                TimeZone = timeZone;
            }
        }

        public readonly struct WeatherData
        {
            public string TimeStamp { get; init; } = "";
            public string Main { get; init; } = "";
            public string Description { get; init; } = "";
            public double TemperatureAverage { get; init; } = 0d;
            public double TemperatureMinimum { get; init; } = 0d;
            public double TemperatureMaximum { get; init; } = 0d;
            public double TemperatureFeelsLike { get; init; } = 0d;
            public double WindSpeed { get; init; } = 0d;
            public double WindDirection { get; init; } = 0d;
            public double Humidity { get; init; } = 0d;
            public double Cloudiness { get; init; } = 0d;
            public double Visibility { get; init; } = 0d;
            public double AtmosphericPressure { get; init; } = 0d;

            public WeatherData(
                string timeStamp,
                string main,
                string description,
                double temperatureAverage,
                double temperatureMinimum,
                double temperatureMaximum,
                double temperatureFeelsLike,
                double windSpeed,
                double windDirection,
                double humidity,
                double cloudiness,
                double visibility,
                double atmosphericPressure
            )
            {
                TimeStamp = timeStamp;
                Main = main;
                Description = description;
                TemperatureAverage = temperatureAverage;
                TemperatureMinimum = temperatureMinimum;
                TemperatureMaximum = temperatureMaximum;
                TemperatureFeelsLike = temperatureFeelsLike;
                WindSpeed = windSpeed;
                WindDirection = windDirection;
                Humidity = humidity;
                Cloudiness = cloudiness;
                Visibility = visibility;
                AtmosphericPressure = atmosphericPressure;
            }
        }

    }
}
