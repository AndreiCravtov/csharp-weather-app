namespace WeatherApp;

public class Input
{
    // create enum instances
    public static readonly Input UP = new Input(new char[] { 'w', 'W'}, "UP");
    public static readonly Input DOWN = new Input(new char[] { 's', 'S' }, "DOWN");
    public static readonly Input SELECT = new Input(new char[] { ' ' }, "SELECT"); // space key
    public static readonly Input BACK = new Input(new char[] { '\b' }, "BACK"); // backspace key
    public static readonly Input EXIT = new Input(new char[] { '\u001b' }, "EXIT"); // escape key
    public static readonly Input NULL = new Input(new char[0], "NULL");

    // assign values to the enums
    public char[] Charecters { get; private set; }
    public string Name { get; private set; }
    private Input(char[] charecters, string name) => (Charecters, Name) = (charecters, name);

    // make enum iterable
    public static IEnumerable<Input> Values
    {
        get
        {
            yield return UP;
            yield return DOWN;
            yield return SELECT;
            yield return BACK;
            yield return EXIT;
            yield return NULL;
        }
    }

    // add input fetch functionality functionality
    public static Input GetLatestInput()
    {
        char? inputChar = KeyCapture.Instance().GetLatestKey();
        if (inputChar == null) return NULL;

        Input returnInput = NULL;
        foreach (Input input in Values)
        {
            if (input.Charecters.Contains((char)inputChar))
            {
                returnInput = input;
                break;
            }
        }

        return returnInput;
    }
}