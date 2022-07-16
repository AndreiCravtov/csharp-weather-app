namespace WeatherApp;
class KeyCapture
{
    private char? _latest_key;
    private static KeyCapture? _instance;

    private KeyCapture()
    {
        // start background input thread
        Thread input = new Thread(CaptureKeys);
        input.IsBackground = true;
        input.Start();
    }

    public static KeyCapture Instance()
    {
        if (_instance == null)
            _instance = new KeyCapture();
        return _instance;
    }

    public char? GetLatestKey()
    {
        char? return_key = _latest_key;
        _latest_key = null;
        return return_key;
    }

    private void CaptureKeys()
    {
        while (true)
        {
            _latest_key = Console.ReadKey(true).KeyChar;
        }
    }
}