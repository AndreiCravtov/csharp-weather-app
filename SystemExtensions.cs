namespace System;

public static class StringExtensions
{
    public static string PadBoth(this string str, int length)
    {
        int spaces = length - str.Length;
        int padLeft = spaces / 2 + str.Length;
        return str.PadLeft(padLeft).PadRight(length);
    }
}

public static class IntegerExtension
{
    public static int GetAbs(this int num)
    {
        return Math.Abs(num);
    }
}