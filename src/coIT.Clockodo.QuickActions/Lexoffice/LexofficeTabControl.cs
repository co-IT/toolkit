using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.ConfigurationManager;
using coIT.Libraries.LexOffice;

namespace coIT.Clockodo.QuickActions.Lexoffice
{
    public partial class LexofficeTabControl : UserControl
    {
        private readonly LexofficeService _lexofficeService;
        private readonly TimeEntriesService _clockodoService;
        private readonly FileSystemManager _fileSystemManager;

        public LexofficeTabControl(
            LexofficeService lexofficeService,
            TimeEntriesService clockodoService,
            FileSystemManager fileSystemManager
        )
        {
            InitializeComponent();
            _lexofficeService = lexofficeService;
            _clockodoService = clockodoService;
            _fileSystemManager = fileSystemManager;
        }

        private void LexofficeTabControl_Load(object sender, EventArgs e)
        {
            var rechnungsKontrolle = new LexofficeRechnungskontrolle(
                _lexofficeService,
                _clockodoService,
                _fileSystemManager
            );

            tbpRechnungSelbstkontrolle.Controls.Add(rechnungsKontrolle);
            rechnungsKontrolle.Dock = DockStyle.Fill;
        }
    }
}
