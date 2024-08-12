namespace coIT.Database;

internal static class BusinessLogic
{
    public static void PrintList<T>(List<T> list, Func<T, string> printSelector)
    {
        foreach (var item in list)
        {
            var print = printSelector(item);
            Console.WriteLine(print);
        }
    }
}
