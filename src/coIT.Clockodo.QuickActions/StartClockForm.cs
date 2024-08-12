using coIT.Libraries.Clockodo.Clock;
using coIT.Libraries.Clockodo.Clock.Contracts;

namespace coIT.Clockodo.QuickActions;

internal partial class EditTimeEntry : Form
{
    private readonly ClockService _clockService;
    private readonly HistorischerZeiteintrag _existierenderEintrag;

    public EditTimeEntry(ClockService clockService, HistorischerZeiteintrag existierenderEintrag)
    {
        InitializeComponent();

        _clockService = clockService;
        _existierenderEintrag = existierenderEintrag;

        ctrl_Kunde.Text = existierenderEintrag.Kunde;
        ctrl_Beschreibung.Text = existierenderEintrag.Beschreibung;
        ctrl_Leistung.Text = existierenderEintrag.Leistung;
        ctrl_Projekt.Text = existierenderEintrag.Projekt;
        ctrl_Beschreibung.Focus();
        ctrl_Beschreibung.SelectAll();
    }

    private async void StartClock()
    {
        var startClock = new StartClockDto
        {
            CustomerId = _existierenderEintrag._customerId,
            ProjectsId = _existierenderEintrag._projectId,
            ServicesId = _existierenderEintrag._serviceId,
            Description = ctrl_Beschreibung.Text,
        };

        var result = await _clockService.StartClock(startClock);
        if (result.HasValue)
        {
            MessageBox.Show(result.ToString());
        }
        else
        {
            MessageBox.Show("Uhr konnte nicht gestartet werden.");
        }

        Close();
    }

    private void ctrl_StartClock_Click(object sender, EventArgs e)
    {
        StartClock();
    }

    private void ctrl_Beschreibung_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            StartClock();
        }
    }
}
