using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.ConfigurationManager;
using coIT.Libraries.LexOffice;

namespace coIT.Clockodo.QuickActions.Lexoffice
{
    public partial class LexofficeTabControl : UserControl
    {
        private readonly FileSystemManager _fileSystemManager;
        private readonly EnvironmentManager _environmentManager;

        public LexofficeTabControl(
            FileSystemManager fileSystemManager,
            EnvironmentManager environmentManager
        )
        {
            InitializeComponent();
            _fileSystemManager = fileSystemManager;
            _environmentManager = environmentManager;
        }

        private void LexofficeTabControl_Load(object sender, EventArgs e)
        {
            var rechnungsKontrolle = new LexofficeRechnungskontrolle(
                _fileSystemManager,
                _environmentManager
            );

            tbpRechnungSelbstkontrolle.Controls.Add(rechnungsKontrolle);
            rechnungsKontrolle.Dock = DockStyle.Fill;
        }
    }
}
