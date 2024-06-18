using CoinCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BinanceAcountViewer
{
    public partial class NewBotForm : Form
    {
        private List<TradeCoinInfo> ListTradesCoins;
        private string TradingCoinPath;


        public NewBotForm(List<TradeCoinInfo> listTradesCoins, string tradingCoinPath)
        {
            ListTradesCoins = listTradesCoins;
            TradingCoinPath = tradingCoinPath;
            InitializeComponent();
            textBox4.Text = "0";
            textBox3.Text = "0";
        }



        private void NewBotForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var newCoin = new TradeCoinInfo(textBox1.Text, Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text));
            ListTradesCoins.Add(newCoin);
            SaveListTradesCoins();


        }

        private void SaveListTradesCoins()
        {
            var listTradesCoins = JsonConvert.SerializeObject(ListTradesCoins);
            using (StreamWriter writer = new StreamWriter(TradingCoinPath))
            {
                writer.WriteLine(listTradesCoins);
                writer.Close();
            }
        }

        private void NewBotForm_Load(object sender, EventArgs e)
        {

        }
    }
}
