// Gracias a github.com/jdamador

public static class ArrayExtensions
{
    public static void Fill(this int[] arrayToFill, int withValue)
    {
        for (int i = 0; i < arrayToFill.Length; i++)
        {
            arrayToFill[i] = withValue;
        }
    }
}