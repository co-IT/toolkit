namespace coIT.Libraries.WinForms
{
    public static class DataGridViewHeaders
    {
        public static void SetHeadersTo(this DataGridView datagrid, List<string> headers)
        {
            for (var i = 0; i < datagrid.Columns.Count && i < headers.Count; i++)
                datagrid.Columns[i].HeaderText = headers[i];
        }
    }
}
