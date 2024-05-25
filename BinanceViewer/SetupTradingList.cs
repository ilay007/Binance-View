using CoinCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            ListTradesCoins = JsonConvert.DeserializeObject<List<TradeCoinInfo>>(File.ReadAllLines(TradingCoinPath)[0]);
            UpdateListView();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void UpdateListView()
        {
            try
            {                                         
                listView1.SetObjects(ListTradesCoins);
                listView1.Update();

            }
            catch(Exception ex)
            {
                throw ex;
            }
           

        }

        private async void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newLines = JsonConvert.DeserializeObject<List<TradeCoinInfo>>(File.ReadAllLines(TradingCoinPath)[0]);
            if (newLines.Count == ListTradesCoins.Count) return;
            UpdateListView();
        }

        private void buttonAddCoin_Click(object sender, EventArgs e)
        {
            var user1Form = new NewBotForm(ListTradesCoins, TradingCoinPath);
            user1Form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var curItem = listView1.SelectedItem;
            TradeCoinInfo selected=ListTradesCoins.First(s => s.Name == curItem.Text);
            ListTradesCoins.Remove(selected);
            UpdateListView();


        }
    }
}
