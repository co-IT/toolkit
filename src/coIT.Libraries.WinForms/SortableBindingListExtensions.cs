namespace coIT.Libraries.WinForms;

public static class SortableBindingListExtensions
{
    public static SortableBindingList<T> AsSortableBindingList<T>(this IEnumerable<T>? originList)
        where T : class
    {
        return originList == null
            ? new SortableBindingList<T>()
            : new SortableBindingList<T>(originList.ToList());
    }
}
