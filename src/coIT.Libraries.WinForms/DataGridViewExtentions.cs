namespace coIT.Libraries.WinForms
{
    public static class DataGridViewExtentions
    {
        public static void SpalteVerstecken(this DataGridView dgv, string spaltenname)
        {
            var failureTypeColumn = dgv.Columns[spaltenname];
            if (failureTypeColumn != null)
                failureTypeColumn.Visible = false;
        }

        public static void SpalteUmbenennen(
            this DataGridView dgv,
            string alterName,
            string neuerName
        )
        {
            var failureTypeColumn = dgv.Columns[alterName];
            if (failureTypeColumn != null)
                failureTypeColumn.HeaderCell.Value = neuerName;
        }

        public static void SpaltenGrößeFestlegen(
            this DataGridView dgv,
            string spaltenname,
            int minimaleBreite,
            int prozentualerAnteil
        )
        {
            var existingColumn = dgv.Columns[spaltenname];
            if (existingColumn is null)
                return;

            existingColumn.MinimumWidth = minimaleBreite;
            existingColumn.FillWeight = prozentualerAnteil;
        }

        public static void SpalteAlsHyperlink(this DataGridView dgv, string spaltenname)
        {
            var existingColumn = dgv.Columns[spaltenname];
            if (existingColumn != null)
            {
                var linkColumn = new DataGridViewLinkColumn
                {
                    Name = existingColumn.Name,
                    HeaderText = existingColumn.HeaderText,
                    DataPropertyName = existingColumn.DataPropertyName,
                    DisplayIndex = existingColumn.DisplayIndex,
                    Width = existingColumn.Width,
                    ReadOnly = existingColumn.ReadOnly,
                    SortMode = existingColumn.SortMode,
                    Visible = existingColumn.Visible,
                    DefaultCellStyle = existingColumn.DefaultCellStyle
                };

                // Add the new link column at the same position
                dgv.Columns.Insert(existingColumn.Index, linkColumn);

                // Remove the old column
                dgv.Columns.Remove(existingColumn);
            }
        }
    }
}
