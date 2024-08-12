using System.Timers;
using Timer = System.Timers.Timer;

namespace coIT.Clockodo.QuickActions
{
    public partial class LadeForm : Form
    {
        public string LadeText { get; }
        public Func<Task> Funktion { get; }

        private Timer _timer;
        private Task _laufendeAktion;

        public int Timeout { get; }

        public LadeForm(string ladeText, Func<Task> funktion, TimeSpan timeout)
        {
            InitializeComponent();
            LadeText = ladeText;
            Funktion = funktion;
            _timer = new Timer(timeout);
            _timer.Elapsed += OnTimeout;
            _timer.Enabled = true;
        }

        private void OnTimeout(object sender, EventArgs e)
        {
            try
            {
                if (_laufendeAktion is not null && !_laufendeAktion.IsCompleted)
                    _laufendeAktion.Dispose();
            }
            finally
            {
                _timer.Dispose();
                DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private async void LadeForm_Load(object sender, EventArgs e)
        {
            lblStatus.Text = Text;
            await FunktionAusführen();
            Close();
        }

        public async Task FunktionAusführen()
        {
            try
            {
                _laufendeAktion = Funktion();
                await _laufendeAktion;
                DialogResult = DialogResult.OK;
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show(
                    "Die Abfrage hat die maximal zulässige Dauer überschritten. Kontaktiere Jannes oder versuche es erneut.",
                    "Abfrage fehlgeschlagen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                DialogResult = DialogResult.Abort;
            }
            catch (Exception ex)
            {
                //TODO: Workaround => Solange es kein CancellationToken gibt, läuft die Aktion weiter
                if (this.IsDisposed)
                    return;

                MessageBox.Show(
                    $"Kontaktiere Jannes bezüglich '{ex.Message}'",
                    "Abfrage fehlgeschlagen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
