namespace SampleFins
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnFinsUdp_Click(object sender, EventArgs e)
        {
            frmFinsUdp f1 = new frmFinsUdp();
            f1.ShowDialog();
        }

        private void btnFinsUdpASync_Click(object sender, EventArgs e)
        {
            frmFinsUdpASync f2 = new frmFinsUdpASync();
            f2.ShowDialog();
        }

        private void btnFinsTcp_Click(object sender, EventArgs e)
        {
            frmFinsTcp f3 = new frmFinsTcp();
            f3.ShowDialog();
        }
    }
}