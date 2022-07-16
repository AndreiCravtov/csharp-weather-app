namespace WeatherApp;

public static class ConsoleDisplay
{
    public const int REFRESH_TIME = 50;

    public static class MainMenu
    {
        public enum ExitCode
        {
            QUIT,
            SEE_CURRENT_WEATHER,
            SEE_WEATHER_FORECAST,
            CHANGE_LOCATIONS
        }

        public static ExitCode Display(Location location)
        {
            string printable;
            ExitCode exitCode = ExitCode.QUIT;

            bool[] selector = { true, false, false };
            Input input = Input.NULL;
            while (input != Input.EXIT && input != Input.SELECT)
            {
                // lazy clear screen
                ClearScreen();

                // get input
                input = Input.GetLatestInput();

                // display menu
                printable =
                    " ---------                                       --                                       --------- \n" +
                    " QUIT=ESC UP=W DOWN=S SELECT=SPACE                                                                  \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                             MAIN MENU:                                             \n" +
                    "                                                                                                    \n" +
                   $"                                       {(selector[0] ? ">" : " ")} SEE CURRENT WEATHER {(selector[0] ? "<" : " ")}\n" +
                   $"                                   {(selector[1] ? ">" : " ")} SEE 5 DAY WEATHER FORECAST {(selector[1] ? "<" : " ")}\n" +
                   $"                                        {(selector[2] ? ">" : " ")} CHANGE LOCATIONS {(selector[2] ? "<" : " ")}\n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                   $" LOCATION=\"{location.Name}\" LATITUDE={location.Coordinates.Latitude} LONGITUDE={location.Coordinates.Longitude}\n" +
                    " ---------                                       --                                       --------- \n";
                Console.WriteLine(printable);

                // perform input logic
                int selected = Array.IndexOf(selector, true);
                if (input == Input.SELECT)
                    exitCode = (ExitCode) selected + 1;
                else if (input == Input.UP && !selector[0])
                {
                    selector[selected] = false;
                    selector[selected - 1] = true;
                }
                else if (input == Input.DOWN && !selector[selector.Length - 1])
                {
                    selector[selected] = false;
                    selector[selected + 1] = true;
                }

                // sleep
                Thread.Sleep(REFRESH_TIME);
            }
            // strict clear screen
            ClearScreen(true);

            return exitCode;
        }
    }

    public static class ChangeLocations
    {
        public static Location Display(Location location)
        {
            string printable;
            Location newLocation = location;

            // create selector at location
            bool[] selector = new bool[Location.Values.Count()];
            int index = 0;
            foreach (Location l in Location.Values)
            {
                if (l == location)
                    selector[index] = true;
                else
                    selector[index] = false;

                index++;
            }

            Input input = Input.NULL;
            while (input != Input.BACK && input != Input.SELECT)
            {
                // lazy clear screen
                ClearScreen();

                // get input
                input = Input.GetLatestInput();

                // display menu
                printable =
                    " ---------                                       --                                       --------- \n" +
                    " BACK=BACKSPACE UP=W DOWN=S SELECT=SPACE                                                            \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                        SELECT NEW LOCATION:                                        \n" +
                    "                                                                                                    \n" +
                   $"                                    {(selector[0] ? ">" : " ")} \"Amsterdam, Netherlands\" {(selector[0] ? "<" : " ")}\n" +
                   $"                                      {(selector[1] ? ">" : " ")} \"Bangkok, Thailand\" {(selector[1] ? "<" : " ")}\n" +
                   $"                                       {(selector[2] ? ">" : " ")} \"Barcelona, Spain\" {(selector[2] ? "<" : " ")}\n" +
                   $"                                         {(selector[3] ? ">" : " ")} \"Delhi, India\" {(selector[3] ? "<" : " ")}\n" +
                   $"                                 {(selector[4] ? ">" : " ")} \"Dubai, United Arab Emirates\" {(selector[4] ? "<" : " ")}\n" +
                   $"                                       {(selector[5] ? ">" : " ")} \"Hong Kong, China\" {(selector[5] ? "<" : " ")}\n" +
                   $"                                       {(selector[6] ? ">" : " ")} \"Istanbul, Turkey\" {(selector[6] ? "<" : " ")}\n" +
                   $"                                    {(selector[7] ? ">" : " ")} \"Kuala Lumpur, Malaysia\" {(selector[7] ? "<" : " ")}\n" +
                   $"                                       {(selector[8] ? ">" : " ")} \"London, England\" {(selector[8] ? "<" : " ")}\n" +
                   $"                                     {(selector[9] ? ">" : " ")} \"Mexico City, Mexico\" {(selector[9] ? "<" : " ")}\n" +
                   $"                                        {(selector[10] ? ">" : " ")} \"Moscow, Russia\" {(selector[10] ? "<" : " ")}\n" +
                   $"                                      {(selector[11] ? ">" : " ")} \"New York City, USA\" {(selector[11] ? "<" : " ")}\n" +
                   $"                                          {(selector[12] ? ">" : " ")} \"Null Island\" {(selector[12] ? "<" : " ")}\n" +
                   $"                                        {(selector[13] ? ">" : " ")} \"Paris, France\" {(selector[13] ? "<" : " ")}\n" +
                   $"                                       {(selector[14] ? ">" : " ")} \"Porto, Portugal\" {(selector[14] ? "<" : " ")}\n" +
                   $"                                         {(selector[15] ? ">" : " ")} \"Rome, Italy\" {(selector[15] ? "<" : " ")}\n" +
                   $"                                          {(selector[16] ? ">" : " ")} \"Singapore\" {(selector[16] ? "<" : " ")}\n" +
                   $"                                      {(selector[17] ? ">" : " ")} \"Sydney, Australia\" {(selector[17] ? "<" : " ")}\n" +
                   $"                                         {(selector[18] ? ">" : " ")} \"Tokyo, Japan\" {(selector[18] ? "<" : " ")}\n" +
                   $"                                       {(selector[19] ? ">" : " ")} \"Vienna, Austria\" {(selector[19] ? "<" : " ")}\n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                   $" LOCATION=\"{location.Name}\" LATITUDE={location.Coordinates.Latitude} LONGITUDE={location.Coordinates.Longitude}\n" +
                    " ---------                                       --                                       --------- \n";
                Console.WriteLine(printable);

                // perform input logic
                int selected = Array.IndexOf(selector, true);
                if (input == Input.SELECT)
                    newLocation = Location.Values.ElementAt(selected);
                else if (input == Input.UP && !selector[0])
                {
                    selector[selected] = false;
                    selector[selected - 1] = true;
                }
                else if (input == Input.DOWN && !selector[selector.Length - 1])
                {
                    selector[selected] = false;
                    selector[selected + 1] = true;
                }

                // sleep
                Thread.Sleep(REFRESH_TIME);
            }
            // strict clear screen
            ClearScreen(true);

            return newLocation;
        }
    }

    public static class SeeCurrentWeather
    {
        public static void Display(Location location)
        {
            var currentWeatherData = WeatherAPI.GetCurrentWeatherData(location);
            string printable;

            ushort[] animation = { 0, 8 };
            Input input = Input.NULL;
            while (input != Input.BACK)
            {
                // lazy clear screen
                ClearScreen();

                // get input
                input = Input.GetLatestInput();

                // display menu
                printable =
                    " ---------                                       --                                       --------- \n" +
                    " BACK=BACKSPACE                                                                                     \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                          CURRENT WEATHER:                                          \n" +
                    "                                                                                                    \n" +
                   $"                                DATE CALCULATED: {currentWeatherData.WeatherData.TimeStamp}         \n" +
                   $"                           {$"{currentWeatherData.WeatherData.Main}: \"{currentWeatherData.WeatherData.Description}\"".PadBoth(47)}\n" +
                    "                                                                                                    \n" +
                   $" TEMPERATURE {((animation[0] < animation[1]/2)?"->":"  ")} AVERAGE: {currentWeatherData.WeatherData.TemperatureAverage}°C\n" +
                   $"                MINIMUM: {currentWeatherData.WeatherData.TemperatureMinimum}10°C                    \n" +
                   $"                MAXIMUM: {currentWeatherData.WeatherData.TemperatureMaximum}°C                      \n" +
                   $"                TO A HUMAN IT FEELS LIKE: {currentWeatherData.WeatherData.TemperatureFeelsLike}°C   \n" +
                   $"        WIND {((animation[0] < animation[1] / 2) ? "->" : "  ")} SPEED: {currentWeatherData.WeatherData.WindSpeed} m/s\n" +
                   $"                DIRECTION: {currentWeatherData.WeatherData.WindDirection}°                          \n" +
                   $"        TIME {((animation[0] < animation[1] / 2) ? "->" : "  ")} SUNRISE: {currentWeatherData.SunriseTime}\n" +
                   $"                SUNSET: {currentWeatherData.SunsetTime}                                             \n" +
                   $"       OTHER {((animation[0] < animation[1] / 2) ? "->" : "  ")} HUMIDITY: {currentWeatherData.WeatherData.Humidity}%\n" +
                   $"                CLOUDINESS: {currentWeatherData.WeatherData.Cloudiness}%                            \n" +
                   $"                VISIBILITY: {currentWeatherData.WeatherData.Visibility} m                           \n" +
                   $"                ATMOSPHERIC PRESSURE: {currentWeatherData.WeatherData.AtmosphericPressure} hPA      \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                   $" LOCATION=\"{location.Name}\" TIME ZONE={currentWeatherData.TimeZone}                               \n" +
                    " ---------                                       --                                       --------- \n";
                Console.WriteLine(printable);

                // perform animation logic
                animation[0] = (ushort)((animation[0] + 1) % animation[1]);

                // sleep
                Thread.Sleep(REFRESH_TIME);
            }
            // strict clear screen
            ClearScreen(true);
        }
    }

    public static class SeeWeatherForecast
    {
        public static void Display(Location location)
        {
            var forecastWeatherData = WeatherAPI.GetForecastWeatherData(location);
            string printable;

            ushort[] selector = { 0, 0 };
            Input input = Input.NULL;
            while (input != Input.BACK)
            {
                // lazy clear screen
                ClearScreen();

                // get input
                input = Input.GetLatestInput();

                // display menu
                printable =
                    " ---------                                       --                                       --------- \n" +
                    " BACK=BACKSPACE UP=W DOWN=S SELECT=SPACE                                                            \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                       5 DAY WEATHER FORECAST:                                      \n" +
                    "                                                                                                    \n" +
                   $" {((selector[0] == 0) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[0 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 0) ? "  " : "")} -| {forecastWeatherData.WeatherData[0 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[0 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 1) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[1 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 1) ? "  " : "")} -| {forecastWeatherData.WeatherData[1 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[1 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 2) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[2 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 2) ? "  " : "")} -| {forecastWeatherData.WeatherData[2 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[2 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 3) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[3 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 3) ? "  " : "")} -| {forecastWeatherData.WeatherData[3 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[3 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 4) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[4 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 4) ? "  " : "")} -| {forecastWeatherData.WeatherData[4 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[4 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 5) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[5 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 5) ? "  " : "")} -| {forecastWeatherData.WeatherData[5 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[5 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 6) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[6 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 6) ? "  " : "")} -| {forecastWeatherData.WeatherData[6 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[6 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 7) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[7 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 7) ? "  " : "")} -| {forecastWeatherData.WeatherData[7 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[7 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 8) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[8 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 8) ? "  " : "")} -| {forecastWeatherData.WeatherData[8 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[8 + selector[1] * 10].Description}\"  \n" +
                   $" {((selector[0] == 9) ? "->" : "")} FORECAST MADE FOR {forecastWeatherData.WeatherData[9 + selector[1] * 10].TimeStamp}  \n" +
                   $" {((selector[0] == 9) ? "  " : "")} -| {forecastWeatherData.WeatherData[9 + selector[1] * 10].Main}: \"{forecastWeatherData.WeatherData[9 + selector[1] * 10].Description}\"  \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                   $" LOCATION=\"{location.Name}\" TIME ZONE={forecastWeatherData.TimeZone}                              \n" +
                    " ---------                                       --                                       --------- \n";
                Console.WriteLine(printable);

                // perform input logic
                if (input == Input.SELECT)
                {
                    ClearScreen(true);
                    DisplayWeatherData(location, forecastWeatherData.WeatherData[selector[0] + selector[1] * 10]);
                }
                else if (input == Input.UP && !(selector[0] == 0 && selector[1] == 0))
                {
                    if (selector[0] == 0)
                    {
                        ClearScreen(true);
                        selector[0] = 9;
                        selector[1]--;
                    }
                    else
                        selector[0]--;

                }
                else if (input == Input.DOWN && !(selector[0] == 9 && selector[1] == 3))
                {
                    if (selector[0] == 9)
                    {
                        ClearScreen(true);
                        selector[0] = 0;
                        selector[1]++;
                    }
                    else
                        selector[0]++;
                }

                // sleep
                Thread.Sleep(REFRESH_TIME);
            }
            // strict clear screen
            ClearScreen(true);
        }

        private static void DisplayWeatherData(Location location, WeatherAPI.DataObjects.WeatherData weatherData)
        {
            string printable;

            ushort[] animation = { 0, 8 };
            Input input = Input.NULL;
            while (input != Input.BACK)
            {
                // lazy clear screen
                ClearScreen();

                // get input
                input = Input.GetLatestInput();

                // display menu
                printable =
                    " ---------                                       --                                       --------- \n" +
                    " BACK=BACKSPACE                                                                                     \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                          CURRENT WEATHER:                                          \n" +
                    "                                                                                                    \n" +
                   $" TEMPERATURE {((animation[0] < animation[1] / 2) ? "->" : "  ")} AVERAGE: {weatherData.TemperatureAverage}°C\n" +
                   $"                MINIMUM: {weatherData.TemperatureMinimum}10°C                                       \n" +
                   $"                MAXIMUM: {weatherData.TemperatureMaximum}°C                                         \n" +
                   $"                TO A HUMAN IT FEELS LIKE: {weatherData.TemperatureFeelsLike}°C                      \n" +
                   $"        WIND {((animation[0] < animation[1] / 2) ? "->" : "  ")} SPEED: {weatherData.WindSpeed} m/s \n" +
                   $"                DIRECTION: {weatherData.WindDirection}°                                             \n" +
                   $"       OTHER {((animation[0] < animation[1] / 2) ? "->" : "  ")} HUMIDITY: {weatherData.Humidity}%  \n" +
                   $"                CLOUDINESS: {weatherData.Cloudiness}%                                               \n" +
                   $"                VISIBILITY: {weatherData.Visibility} m                                              \n" +
                   $"                ATMOSPHERIC PRESSURE: {weatherData.AtmosphericPressure} hPA                         \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                    "                                                                                                    \n" +
                   $" LOCATION=\"{location.Name}\" LATITUDE={location.Coordinates.Latitude} LONGITUDE={location.Coordinates.Longitude}\n" +
                    " ---------                                       --                                       --------- \n";
                Console.WriteLine(printable);

                // perform animation logic
                animation[0] = (ushort)((animation[0] + 1) % animation[1]);

                // sleep
                Thread.Sleep(REFRESH_TIME);
            }
            // strict clear screen
            ClearScreen(true);
        }
    }

    public static void ClearScreen(bool strict = false)
    {
        if (strict)
            Console.Clear();
        else
            Console.SetCursorPosition(0, 0);
    }
}
