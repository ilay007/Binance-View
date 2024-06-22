using System;
using System.Linq;
using System.Windows.Forms;
using BinanceAcountViewer;
using BinanceAcountViewer.Properties;

namespace AcountViewer
{
    public partial class StartFrom : Form
    {

        public StartFrom()
        {
            InitializeComponent();
            Credentials.InitCredentials(Settings.Default.PathToFileWithKeys);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var credentials = Credentials.GetCredentials();
            var user1Form = new TradingInfo(credentials[0].Key, credentials[0].Value);
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var credentials = Credentials.GetCredentials();
            var user1Form = new BotViewer(credentials[0].Key, credentials[0].Value);
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var user1Form = new KnowledgeViewer();
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var user1Form = new SetupTradingList();
            user1Form.IsMdiContainer = true;
            user1Form.Show();

        }
    }
}
