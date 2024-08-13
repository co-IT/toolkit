namespace coIT.Clockodo.QuickActions.Einstellungen
{
    internal class EinstellungenGeladenEventArgs : EventArgs
    {
        public ClockodoEinstellungen ClockodoEinstellungen { get; set; }
        public LexofficeKonfiguration LexofficeEinstellungen { get; set; }
    }
}
