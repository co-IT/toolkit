namespace coIT.Libraries.WinForms
{
    public static class DataGridViewSettings
    {
        public static void ConfigureWithDefaultBehaviour(this DataGridView datagrid)
        {
            datagrid.AllowUserToAddRows = false;
            datagrid.AllowUserToDeleteRows = false;
            datagrid.AllowUserToOrderColumns = true;
            datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            datagrid.MultiSelect = false;
            datagrid.ReadOnly = true;
            datagrid.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            datagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
