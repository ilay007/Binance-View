using CoinCore;
using Newtonsoft.Json;
using Serilog;
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
        private List<TradeCoinInfo> ListTradeCoins=new List<TradeCoinInfo>();
        private string TradingCoinPath;
        public SetupTradingList()
        {
            InitLoger();
            InitializeComponent();            
            InitListTradeCoins();
            UpdateListView();


        }


        private void InitLoger()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.RollingFile(
              pathFormat: "logs\\log-{Date}.txt", // {Date} will be replaced with the date
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        }

        private void InitListTradeCoins()
        {
            try
            {
                TradingCoinPath = Properties.Settings.Default.PathToTradingStateReal;
                ListTradeCoins = JsonConvert.DeserializeObject<List<TradeCoinInfo>>(File.ReadAllLines(TradingCoinPath)[0]);
            }
            catch (FileNotFoundException ex)
            {
                Log.Error("File not found: " + ex.Message);            
                throw new Exception("File not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred: " + ex.Message);
                Console.WriteLine("An error occurred: " + ex.Message);
            }
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
                listView1.SetObjects(ListTradeCoins);
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
            if (newLines.Count == ListTradeCoins.Count) return;
            UpdateListView();
        }

        private void buttonAddCoin_Click(object sender, EventArgs e)
        {
            var user1Form = new NewBotForm(ListTradeCoins, TradingCoinPath);
            user1Form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var curItem = listView1.SelectedItem;
            if(curItem == null) return;
            TradeCoinInfo selected=ListTradeCoins.First(s => s.Name == curItem.Text);
            ListTradeCoins.Remove(selected);
            UpdateListView();
            SaveListTradesCoins();
        }

        private void SaveListTradesCoins()
        {
            var listTradesCoins = JsonConvert.SerializeObject(ListTradeCoins);
            using (StreamWriter writer = new StreamWriter(TradingCoinPath))
            {
                writer.WriteLine(listTradesCoins);
                writer.Close();
            }
        }
    }
}
