using CoinCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace BinanceAcountViewer
{
    public partial class SetupTradingList : Form
    {
        private List<TradeCoinInfo> ListTradesCoins;
        private string TradingCoinPath;
        public SetupTradingList()
        {
            InitializeComponent();
            TradingCoinPath = Properties.Settings.Default.PathToTradingStateReal;
            var lines = File.ReadAllLines(TradingCoinPath);
            ListTradesCoins = JsonConvert.DeserializeObject<List<TradeCoinInfo>>(lines[0]);
            listView1.SetObjects(ListTradesCoins);
            listView1.Update();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
