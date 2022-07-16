namespace WeatherApp;
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Location location = Location.NULL_ISLAND;
        //return;

        bool running = true;
        while (running)
        {
            switch (ConsoleDisplay.MainMenu.Display(location))
            {
                case ConsoleDisplay.MainMenu.ExitCode.QUIT:
                {
                    running = false;
                    break;
                }
                case ConsoleDisplay.MainMenu.ExitCode.SEE_CURRENT_WEATHER:
                {
                    ConsoleDisplay.SeeCurrentWeather.Display(location);
                    break;
                }
                case ConsoleDisplay.MainMenu.ExitCode.SEE_WEATHER_FORECAST:
                {
                    ConsoleDisplay.SeeWeatherForecast.Display(location);
                    break;
                }
                case ConsoleDisplay.MainMenu.ExitCode.CHANGE_LOCATIONS:
                {
                    location = ConsoleDisplay.ChangeLocations.Display(location);
                    break;
                }
                default: break;
            }
        }
    }
}