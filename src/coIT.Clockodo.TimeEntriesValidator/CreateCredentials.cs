namespace coIT.Clockodo.TimeEntriesValidator
{
    public partial class CreateCredentials : Form
    {
        public CreateCredentials()
        {
            InitializeComponent();
        }

        private void btnCreateConfig_Click(object sender, EventArgs e)
        {
            var name = tbxEmployeename.Text;
            var mail = tbxEmployeemail.Text;
            var token = tbxEmployeetoken.Text;

            Settings.CreateCredentials(mail, token, name);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
