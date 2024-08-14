using System.Collections.Immutable;
using System.Diagnostics;
using coIT.Clockodo.QuickActions.Einstellungen;
using coIT.Clockodo.QuickActions.Lexoffice;
using coIT.Libraries.Clockodo.Account;
using coIT.Libraries.Clockodo.Account.Contracts;
using coIT.Libraries.Clockodo.BusinessRules;
using coIT.Libraries.Clockodo.Clock;
using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.ConfigurationManager;
using coIT.Libraries.ConfigurationManager.Cryptography;
using coIT.Libraries.ConfigurationManager.Serialization;
using coIT.Libraries.LexOffice;
using coIT.Libraries.WinForms;
using coIT.Libraries.WinForms.DateTimeButtons;

namespace coIT.Clockodo.QuickActions;

public partial class FormMain : Form
{
    private AccountInformation _accountInformation;
    private ClockodoEinstellungen _clockodoSettings;
    private LexofficeKonfiguration _lexofficeKonfiguration;
    private EnvironmentManager _environmentManager;
    private FileSystemManager _fileSystemManager;

    public FormMain()
    {
        InitializeComponent();

        dgvClockodoFehler.CellFormatting += dgvClockodoFehler_CellFormatting;
    }

    private void EinstellungenEingebenErzwingen(object? sender, EventArgs e)
    {
        TabStatusSetzen(false);
        tbcForms.SelectedTab = tbpEinstellungen;
    }

    private void EinstellungenSetzen(object sender, EinstellungenGeladenEventArgs einstellungen)
    {
        _clockodoSettings = einstellungen.ClockodoEinstellungen;
        _lexofficeKonfiguration = einstellungen.LexofficeEinstellungen;
        DatenNeuladen();
        UhrAktualisierungsTimerStarten();
        TabStatusSetzen(true, true);
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
        this.Visible = false;
        KonfigurationManagerLaden();
        EinstellungenLaden();
        LexofficeTabLaden();

        // Wird derzeit nicht benötigt und würde Anwender verwirren
        tbcForms.Controls.Remove(tbpErfassen);

        this.Visible = true;

        ZeitraumSchnellauswahlButtonTexteSetzen();
        StandardZeitraumSetzen();
    }

    private void LexofficeTabLaden()
    {
        var lexofficeService = new LexofficeService(_lexofficeKonfiguration.LexofficeKey);
        var timeEntriesServices = new TimeEntriesService(_clockodoSettings.ClockodoCredentials);
        var lexofficeTab = new LexofficeTabControl(
            lexofficeService,
            timeEntriesServices,
            _fileSystemManager
        );
        tbpLexoffice.Controls.Add(lexofficeTab);
        lexofficeTab.Dock = DockStyle.Fill;
    }

    private void EinstellungenLaden()
    {
        var einstellungenControl = new EinstellungenControl(
            _environmentManager,
            _fileSystemManager
        );

        tbpEinstellungen.Controls.Add(einstellungenControl);
        einstellungenControl.Dock = DockStyle.Fill;
        einstellungenControl.EinstellungenErfolreichGeladen += EinstellungenSetzen;
        einstellungenControl.EinstellungenKonntenNichtGeladenWerden +=
            EinstellungenEingebenErzwingen;
        einstellungenControl.EinstellungenAktualisierungStart += (_, _) =>
            TabStatusSetzen(false, true);
        einstellungenControl.EinstellungenAktualisierungEnde += (_, _) =>
            TabStatusSetzen(true, true);
        einstellungenControl.Laden();
    }

    private void KonfigurationManagerLaden()
    {
        var key =
            "eyJJdGVtMSI6IlZSZG1iaEJEVnF6U0swbTBHYjBEUFREdWU5c01sSmNNeURwOE1qb1VKTjg9IiwiSXRlbTIiOiJubE00WEJsTkZGTWFDVFd3Si9EdEZRPT0ifQ==";
        var aesCryptography = AesCryptographyService.FromKey(key).Value;
        var jsonSerializer = new NewtonsoftJsonSerializer();

        _environmentManager = new EnvironmentManager(aesCryptography, jsonSerializer);
        _fileSystemManager = new FileSystemManager();
    }

    private void UhrAktualisierungsTimerStarten()
    {
        var timer = new System.Windows.Forms.Timer();
        timer.Tick += new EventHandler(async (_, _) => await LaufendeUhrAktualisieren());
        timer.Interval = 60 * 1000;
        timer.Start();
    }

    private void StandardZeitraumSetzen()
    {
        var jetzt = DateTime.Now.AddDays(-1);
        var vor14Tagen = jetzt.AddDays(-15);

        dtpZeitraumStart.Value = vor14Tagen;
        dtpZeitraumEnde.Value = jetzt;
    }

    private void TabStatusSetzen(bool status, bool settingsAuchBlockieren = false)
    {
        tbpErfassen.Enabled = status;
        tbpClockodo.Enabled = status;

        if (settingsAuchBlockieren)
        {
            tbpEinstellungen.Enabled = status;
            tbpEinstellungen.Enabled = status;
        }
    }

    private async Task LoadUserAccountInformation()
    {
        try
        {
            var accountService = new AccountService(_clockodoSettings.CreateApiConnectionSettings);
            _accountInformation = await accountService.GetMyAccount();
            ctrl_ZeigeSelektiertenMitarbeiter.Text = $"Angemeldet als: {_accountInformation.Name}";
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    private async void ctrl_LadeDaten_Click(object sender, EventArgs e)
    {
        await DatenNeuAbrufen();
    }

    private async Task DatenNeuAbrufen()
    {
        try
        {
            var jetzt = DateOnly.FromDateTime(DateTime.Now);
            var vor14Tagen = jetzt.AddDays(-14);

            var period = ClockodoPeriod.Create(vor14Tagen, jetzt);
            var clockodoService = new TimeEntriesService(_clockodoSettings.ClockodoCredentials);

            var alleEinträgeDerMitarbeiter = (
                await clockodoService.GetTimeEntriesAsync(period.Value, _accountInformation.Id)
            );

            var historischeZeiteinträge = alleEinträgeDerMitarbeiter
                .OrderByDescending(zeiteintrag => zeiteintrag.End)
                .Select(HistorischerZeiteintrag.ErzeugeAus)
                .Distinct()
                .ToList()
                .AsSortableBindingList();

            ctrl_Zeiteintraege.ConfigureWithDefaultBehaviour();
            ctrl_Zeiteintraege.DataSource = historischeZeiteinträge;
            // ctrl_Zeiteintraege.Columns["serviceID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            // ctrl_Zeiteintraege.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            if (ctrl_Zeiteintraege.Columns.Count > 0)
            {
                ctrl_Zeiteintraege.Columns[0].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.DisplayedCells;
                ctrl_Zeiteintraege.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.DisplayedCells;
                ctrl_Zeiteintraege.Columns[2].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.DisplayedCells;
                ctrl_Zeiteintraege.Columns[Index.FromEnd(1)].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            await LaufendeUhrAktualisieren();
        }
        catch (Exception e)
        {
            MessageBox.Show(
                e.Message,
                "Fehler beim Abrufen der Daten.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private async void DatenNeuladen()
    {
        await LoadUserAccountInformation();
        await DatenNeuAbrufen();
        ctrl_Zeiteintraege.Focus();
        ctrl_Zeiteintraege.SelectAll();
        ctrl_LadeDaten.Enabled = true;
    }

    private void OpenTimeEntry()
    {
        var zeiteintrag = ctrl_Zeiteintraege.BindSelected<HistorischerZeiteintrag>();

        if (zeiteintrag == null)
            return;

        var clockService = new ClockService(_clockodoSettings.CreateApiConnectionSettings);
        var form = new EditTimeEntry(clockService, zeiteintrag);

        form.ShowDialog();
    }

    private async void ctrl_Zeiteintraege_CellDoubleClick(
        object sender,
        DataGridViewCellEventArgs e
    )
    {
        OpenTimeEntry();
        await DatenNeuAbrufen();
    }

    private void ctrl_Zeiteintraege_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            OpenTimeEntry();
    }

    private async void ctrl_laufendeUhrAbfrage_Click(object sender, EventArgs e)
    {
        await LaufendeUhrAktualisieren();
    }

    private async Task LaufendeUhrAktualisieren()
    {
        var clockService = new ClockService(_clockodoSettings.CreateApiConnectionSettings);
        var result = await clockService.GetRunningClockEntry();
        if (result == null)
            return;

        //ctrl_textboxLaufenderEintrag.Text = $"{result.CustomerName}:{result.ProjectName}{Environment.NewLine}{result.Description}";
        ctrl_textboxLaufenderEintrag.Text =
            $"Eintrag läuft seit: {result?.TimeSince.LocalDateTime}{Environment.NewLine} Beschreibung: {result.Description}";
    }

    private async void btnFehlerAktualisiere_click(object sender, EventArgs e)
    {
        Aktualisieren();
    }

    private async Task Aktualisieren()
    {
        var zeitraumStart = dtpZeitraumStart.Value;

        var zeitraumEnde = dtpZeitraumEnde.Value.GetEndOfDay();

        if (zeitraumEnde.Date >= DateTime.Now.Date)
        {
            MessageBox.Show(
                "Für den heutigen Tag oder die Zukunft, kann keine Selbstkontrolle durchgeführt werden",
                "Fehler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            return;
        }

        var period = ClockodoPeriod.Create(zeitraumStart, zeitraumEnde);

        if (period.IsFailure)
        {
            MessageBox.Show(period.Error, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var clockodoService = new TimeEntriesService(_clockodoSettings.ClockodoCredentials);

        var abfragenUndAktualisierenFunc = async () =>
            await ZeiteinträgeAbfragenUndEvaluieren(period.Value, clockodoService);

        var standardTimeout = 30; //besprochen mit UAR am 02.08.24
        using (
            var warteDialog = new LadeForm(
                "Zeiteinträge werden abgerufen und geprüft",
                abfragenUndAktualisierenFunc,
                TimeSpan.FromSeconds(standardTimeout)
            )
        )
        {
            var ergebnis = warteDialog.ShowDialog();

            switch (ergebnis)
            {
                case DialogResult.Abort:
                    MessageBox.Show(
                        "Die Aktion dauerte zu lange",
                        "Zeitüberschreitung",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk
                    );
                    break;
                case DialogResult.OK:
                    if (dgvClockodoFehler.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            "Applaus, Applaus, es wurden keine Fehler gefunden.",
                            "Hinweis",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    break;
            }
        }
    }

    private async Task ZeiteinträgeAbfragenUndEvaluieren(
        ClockodoPeriod period,
        TimeEntriesService clockodoService
    )
    {
        var alleEinträgeDesMitarbeiters = await clockodoService.GetTimeEntriesAsync(
            period,
            _accountInformation.Id
        );

        var evalator =
            ClockodoBusinessRulesEvaluator.CreateWithDefaultCustomerToBillingAccountsAsssociationes();
        var ruleHelper = new ClockodoBusinessRulesHelper(evalator);
        var employee = new List<string> { _accountInformation.Name };

        var dailyUserReports = await clockodoService.GetDailyUserreports(period);
        var änderungsAnträge = await clockodoService.GetAllChangeRequestsAsync(
            period,
            _accountInformation.Id
        );

        var reportsFürDerzeitigenNutzer = dailyUserReports.Where(report =>
            report.Id == _accountInformation.Id
        );

        var failures = ruleHelper.ListAllFailuresAndViolations(
            employee.ToImmutableHashSet(),
            alleEinträgeDesMitarbeiters,
            reportsFürDerzeitigenNutzer,
            änderungsAnträge
        );

        ShowErrorList(failures);
    }

    private void ShowErrorList(List<ClockodoFailure> listeGefundenerFehler)
    {
        var errorBinding = new SortableBindingList<ClockodoFailure>(listeGefundenerFehler.ToList());

        dgvClockodoFehler.DataSource = errorBinding;

        foreach (DataGridViewColumn column in dgvClockodoFehler.Columns)
            column.SortMode = DataGridViewColumnSortMode.Automatic;

        dgvClockodoFehler.SpalteVerstecken(nameof(ClockodoFailure.FailureType));
        dgvClockodoFehler.SpalteVerstecken(nameof(ClockodoFailure.EmployeeName));
        dgvClockodoFehler.SpalteVerstecken(nameof(ClockodoFailure.Context));
        dgvClockodoFehler.SpalteVerstecken(nameof(ClockodoFailure.CanBeEdited));

        dgvClockodoFehler.SpalteUmbenennen(nameof(ClockodoFailure.Message), "Bemerkungen");
        dgvClockodoFehler.SpalteUmbenennen(nameof(ClockodoFailure.Date), "Datum");
        dgvClockodoFehler.SpalteUmbenennen(nameof(ClockodoFailure.DirectLinkToTimeEntry), "Link");

        dgvClockodoFehler.SpaltenGrößeFestlegen(nameof(ClockodoFailure.Message), 100, 150);
        dgvClockodoFehler.SpaltenGrößeFestlegen(nameof(ClockodoFailure.Date), 150, 1);
        dgvClockodoFehler.SpaltenGrößeFestlegen(
            nameof(ClockodoFailure.DirectLinkToTimeEntry),
            100,
            15
        );

        dgvClockodoFehler.SpalteAlsHyperlink(nameof(ClockodoFailure.DirectLinkToTimeEntry));
        dgvClockodoFehler.Sort(
            dgvClockodoFehler.Columns[nameof(ClockodoFailure.Message)],
            System.ComponentModel.ListSortDirection.Ascending
        );
    }

    private void dgvClockodoFehler_CellFormatting(
        object sender,
        DataGridViewCellFormattingEventArgs e
    )
    {
        foreach (DataGridViewRow Myrow in dgvClockodoFehler.Rows)
        {
            if (!(bool)Myrow.Cells[nameof(ClockodoFailure.CanBeEdited)].Value)
                Myrow.DefaultCellStyle.BackColor = Color.DarkGray;
        }
    }

    private void dgvClockodoFehler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (
            e.RowIndex < 0
            || e.RowIndex > dgvClockodoFehler.Rows.Count
            || e.ColumnIndex < 0
            || e.ColumnIndex > dgvClockodoFehler.Columns.Count
        )
            return;

        var clickedCell = dgvClockodoFehler.Rows[e.RowIndex].Cells[e.ColumnIndex];
        if (clickedCell is not DataGridViewLinkCell || clickedCell.Value is null)
            return;

        LinkInBrowserÖffnen(clickedCell.Value.ToString());
    }

    private void LinkInBrowserÖffnen(string link)
    {
        Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
    }

    private void ZeitraumSetzen(DateTime start, DateTime ende)
    {
        dtpZeitraumStart.Value = start;
        dtpZeitraumEnde.Value = ende;
    }

    private void ZeitraumSchnellauswahlButtonTexteSetzen()
    {
        BtnTextErsteZweiWochenAktuellerMonatSetzen();
        BtnTextLetzteZweiWochenVorherigerMonatSetzen();
        BtnTextLetzterMonatSetzen();
    }

    private (DateTime Start, DateTime Ende) ZeitraumErsteZweiWochenAktuellerMonat()
    {
        var monatsAnfang = DateTime.Now.GetFirstDayInMonth();
        var zweiWochenNachMonatsAnfang = monatsAnfang.AddDays(7 * 2);
        var gestern = DateTime.Now.AddDays(-1);

        if (gestern < zweiWochenNachMonatsAnfang)
            return (monatsAnfang, gestern);

        return (monatsAnfang, zweiWochenNachMonatsAnfang);
    }

    private void BtnTextErsteZweiWochenAktuellerMonatSetzen()
    {
        var (monatsAnfang, zweiWochenNachMonatsAnfang) = ZeitraumErsteZweiWochenAktuellerMonat();
        var buttonText = $"{monatsAnfang:dd.}-{zweiWochenNachMonatsAnfang:dd. MMM}";
        btnErsteZweiWochenAktuellerMonat.Text = buttonText;
    }

    private void btnErsteZweiWochenAktuellerMonat_Click(object sender, EventArgs e)
    {
        var (monatsAnfang, zweiWochenNachMonatsAnfang) = ZeitraumErsteZweiWochenAktuellerMonat();

        ZeitraumSetzen(monatsAnfang, zweiWochenNachMonatsAnfang);
    }

    private (DateTime Start, DateTime Ende) ZeitraumLetzteZweiWochenVorherigerMonat()
    {
        var letzterMonatEnde = DateTime.Now.AddMonths(-1).GetLastDayInMonth();
        var zweiWochenVorLetzterMonatEnde = letzterMonatEnde.AddDays(-7 * 2);

        return (zweiWochenVorLetzterMonatEnde, letzterMonatEnde);
    }

    private void BtnTextLetzteZweiWochenVorherigerMonatSetzen()
    {
        var (monatsAnfang, zweiWochenNachMonatsAnfang) = ZeitraumLetzteZweiWochenVorherigerMonat();
        var buttonText = $"{monatsAnfang:dd.}-{zweiWochenNachMonatsAnfang:dd. MMM}";
        btnLetzteZweiWochenVormonat.Text = buttonText;
    }

    private void btnLetzteZweiWochenVormonat_Click(object sender, EventArgs e)
    {
        var (zweiWochenVorLetzterMonatEnde, letzterMonatEnde) =
            ZeitraumLetzteZweiWochenVorherigerMonat();

        ZeitraumSetzen(zweiWochenVorLetzterMonatEnde, letzterMonatEnde);
    }

    private void BtnTextLetzterMonatSetzen()
    {
        var (startLetzterMonat, endeLetzterMonat) = ZeitraumLetzterMonat();
        var buttonText = $"{startLetzterMonat:dd.}-{endeLetzterMonat:dd. MMM}";

        btnLetzterMonat.Text = buttonText;
    }

    private (DateTime Start, DateTime Ende) ZeitraumLetzterMonat()
    {
        var heute = DateTime.Now;
        var irgendwannLetzterMonat = heute.AddMonths(-1);

        var startLetzterMonat = irgendwannLetzterMonat.GetFirstDayInMonth();
        var letzerTagImMonat = irgendwannLetzterMonat.GetLastDayInMonth();

        var endeLetzterTagImMonat = letzerTagImMonat.GetEndOfDay();

        return (startLetzterMonat, endeLetzterTagImMonat);
    }

    private void btnLetzterMonat_Click(object sender, EventArgs e)
    {
        var (startLetzterMonat, endeLetzterMonat) = ZeitraumLetzterMonat();

        ZeitraumSetzen(startLetzterMonat, endeLetzterMonat);
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        LinkInBrowserÖffnen(((Control)sender).Text);
    }

    private void dtpZeitraumEnde_ValueChanged(object sender, EventArgs e)
    {
        DateTimePickerDarfNichtHeuteOderSpäterSein((DateTimePicker)sender);
    }

    private void dtpZeitraumStart_ValueChanged(object sender, EventArgs e)
    {
        DateTimePickerDarfNichtHeuteOderSpäterSein((DateTimePicker)sender);
    }

    private void DateTimePickerDarfNichtHeuteOderSpäterSein(DateTimePicker picker)
    {
        if (picker.Value >= DateTime.Now.Date)
            picker.Value = DateTime.Now.AddDays(-1).GetEndOfDay();
    }
}
