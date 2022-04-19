namespace pratica2PDI.Codigos.UI
{
    public partial class ChooseOption : Form
    {
        int choice = 0;

        public ChooseOption(string title, string[] options)
        {
            InitializeComponent();
            this.title.Text = title;
            defineOptions(options);
        }

        public int getChoice()
        {
            return choice;
        }

        private void defineOptions(string[] options)
        {
            channelsOptions.Items.AddRange(options);
            channelsOptions.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            choice = channelsOptions.SelectedIndex;
            Close();
        }
    }
}
