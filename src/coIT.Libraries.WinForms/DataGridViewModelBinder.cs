namespace coIT.Libraries.WinForms
{
    public static class DataGridViewModelBinder
    {
        public static T? BindSelected<T>(this DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
                return default;

            if (dataGridView.SelectedRows.Count == 0)
                return default;

            var selectedRow = dataGridView.SelectedRows[0];
            var item = (T)selectedRow.DataBoundItem;

            return item;
        }

        public static void ExecuteWithSelectedItem<T>(
            this DataGridView dataGridView,
            Action<T> onItem
        )
            where T : class
        {
            if (dataGridView.Rows.Count == 0)
                return;

            if (dataGridView.SelectedRows.Count == 0)
                return;

            var selectedRow = dataGridView.SelectedRows[0];

            if (selectedRow.DataBoundItem is not T item)
                return;

            onItem(item);
        }

        public static IReadOnlyList<T> BindList<T>(this DataGridView dataGridView)
        {
            var list = dataGridView.DataSource as IReadOnlyList<T>;
            return list ?? new List<T>();
        }
    }
}
