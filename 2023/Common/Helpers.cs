namespace Common;

public static class CharExtensions
{
    public static bool IsNumber(this char c)
    {
        return c >= '0' && c <= '9';
    }
}