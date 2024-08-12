using System;
using System.Collections.Immutable;
using coIT.Libraries.Clockodo.BusinessRules;
using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Clockodo.TimeEntriesValidator
{
    public partial class TimeEntriesValidator : Form
    {
        private TimeEntriesService _clockodoTimeEntriesService;
        private ClockodoBusinessRulesEvaluator _clockodoBusinessRulesEvaluator;

        private IImmutableList<TimeEntry> _allTimeEntriesInPeriod;
        private Settings _settings;
        private List<ClockodoFailure> _timeEntryAndUserReportErrors = new();

        public TimeEntriesValidator()
        {
            InitializeComponent();
            PrepareMonthSelectionButtons();
            SetDateTimePickerToMonthsAgo(0);
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = false;
            btnShowEntries.Enabled = false;

            await GetSelectedPeriod()
                .Tap(LoadTimeEntries)
                .Map(_ => DistinctEmployees(_allTimeEntriesInPeriod))
                .OnFailure(error => MessageBox.Show(error, "Fehler beim Abruf der Daten"))
                .Tap(LoadEmployeeList)
                .Tap(_ => InvokeButtonEnabledChange(btnShowEntries, true));

            btnLoad.Enabled = true;
        }

        public void InvokeButtonEnabledChange(Button button, bool enabled)
        {
            if (button.InvokeRequired)
            {
                Action switchState = delegate
                {
                    InvokeButtonEnabledChange(button, enabled);
                };
                button.Invoke(switchState);
            }

            button.Enabled = enabled;
        }

        private void LoadEmployeeList(IEnumerable<string> employees)
        {
            if (clbEmployees.InvokeRequired)
            {
                Action loadEmployeesAction = delegate
                {
                    LoadEmployeeList(employees);
                };
                clbEmployees.Invoke(loadEmployeesAction);
                return;
            }

            clbEmployees.Items.Clear();

            foreach (var employee in employees)
            {
                clbEmployees.Items.Add(employee, true);
            }
        }

        private void PrepareMonthSelectionButtons()
        {
            SetButtonTextToMonthsAgo(btnTimeFrameCurrentMonth, 0);
            SetButtonTextToMonthsAgo(btnTimeframeOneMonthAgo, 1);
            SetButtonTextToMonthsAgo(btnTimeframeTwoMonthsAgo, 2);
            SetButtonTextToMonthsAgo(btnTimeframeThreeMonthsAgo, 3);
        }

        private void SetButtonTextToMonthsAgo(Button button, int months)
        {
            var today = DateTime.Today;
            var monthsAgo = today.AddMonths(months * -1);
            button.Text = monthsAgo.ToString("Y");
        }

        private Result<ClockodoPeriod> GetSelectedPeriod()
        {
            var start = DateOnly.FromDateTime(dtpPeriodStart.Value);
            var end = DateOnly.FromDateTime(dtpPeriodEnd.Value);

            return ClockodoPeriod.Create(start, end);
        }

        private static IEnumerable<string> DistinctEmployees(
            IEnumerable<TimeEntry> allTimeEntriesInPeriod
        )
        {
            return allTimeEntriesInPeriod
                .Select(timeEntry => timeEntry.EmployeeName)
                .Distinct()
                .OrderBy(employeeName => employeeName)
                .ToList();
        }

        private async Task LoadTimeEntries(ClockodoPeriod period)
        {
            _allTimeEntriesInPeriod = await _clockodoTimeEntriesService.GetTimeEntriesAsync(period);
        }

        private async void btnShowEntries_Click(object sender, EventArgs e)
        {
            var selectedEmployees = GetSelectedEmployees();
            //var selectedEmployees = (new List<string> {_settings.Credentials().Name}).ToImmutableHashSet();

            var clockodoHelper = new ClockodoBusinessRulesHelper(_clockodoBusinessRulesEvaluator);
            var dailyUserReports = await _clockodoTimeEntriesService.GetDailyUserreports(
                GetSelectedPeriod().Value
            );
            var changeRequests = await _clockodoTimeEntriesService.GetAllChangeRequestsAsync(
                GetSelectedPeriod().Value
            );

            _timeEntryAndUserReportErrors = clockodoHelper.ListAllFailuresAndViolations(
                selectedEmployees,
                _allTimeEntriesInPeriod,
                dailyUserReports,
                changeRequests
            );

            ShowErrorList();
        }

        private ImmutableHashSet<string> GetSelectedEmployees()
        {
            var checkedEmployees = clbEmployees.CheckedItems.OfType<string>().ToImmutableHashSet();

            return checkedEmployees;
        }

        private void ShowErrorList()
        {
            var filteredErrorList = GetFilteredErrorList();

            var errorBinding = new SortableBindingList<ClockodoFailure>(filteredErrorList.ToList());

            dgvTimeEntriesWIthErrors.DataSource = errorBinding;

            foreach (DataGridViewColumn column in dgvTimeEntriesWIthErrors.Columns)
                column.SortMode = DataGridViewColumnSortMode.Automatic;

            var failureTypeColumn = dgvTimeEntriesWIthErrors.Columns["FailureType"];
            if (failureTypeColumn != null)
                failureTypeColumn.Visible = false;
        }

        private IEnumerable<ClockodoFailure> GetFilteredErrorList()
        {
            IEnumerable<ClockodoFailure> errors = _timeEntryAndUserReportErrors;

            if (cbxIgnoreTimeentriesWithLessThan10Min.Checked)
                errors = errors.Where(e =>
                    e.FailureType != ClockodoFailureType.TimeEntryShorterThan10Minutes
                );

            return errors;
        }

        private void TimeEntriesValidator_Load(object sender, EventArgs e)
        {
            btnShowEntries.Enabled = false;

            if (!Settings.CredentialsExist())
            {
                var settingsResult = new CreateCredentials().ShowDialog();
                if (settingsResult != DialogResult.OK)
                {
                    MessageBox.Show("Fehler beim Anlegen der Einstellungen");
                    Application.Exit();
                }
            }

            var settings = Settings.Load();
            if (settings.IsFailure)
            {
                MessageBox.Show(settings.Error, "Fehler beim Laden der Einstellungen");
                Application.Exit();
            }

            _settings = settings.Value;

            var credentials = _settings.Credentials();
            _clockodoTimeEntriesService = new TimeEntriesService(
                credentials.ToTimeEntriesServiceSettings()
            );

            _clockodoBusinessRulesEvaluator = BusinessRulesLoader.LoadEvaluator();
        }

        private void SetDateTimePickerToMonthsAgo(int months)
        {
            var today = DateTime.Today;
            var lastMonth = today.AddMonths(months * -1 + 1);
            var month = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            dtpPeriodStart.Value = first;
            dtpPeriodEnd.Value = last;
        }

        private void btnTimeframeThreeMonthsAgo_Click(object sender, EventArgs e)
        {
            SetDateTimePickerToMonthsAgo(3);
        }

        private void btnTimeframeTwoMonthsAgo_Click(object sender, EventArgs e)
        {
            SetDateTimePickerToMonthsAgo(2);
        }

        private void btnTimeframeOneMonthAgo_Click(object sender, EventArgs e)
        {
            SetDateTimePickerToMonthsAgo(1);
        }

        private void btnTimeFrameCurrentMonth_Click(object sender, EventArgs e)
        {
            SetDateTimePickerToMonthsAgo(0);
        }

        private void cbxIgnoreTimeentriesWithLessThan10Min_CheckedChanged(
            object sender,
            EventArgs e
        )
        {
            ShowErrorList();
        }
    }
}
