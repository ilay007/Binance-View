using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using BinanceAcountViewer;
using CoinCore;

namespace AcountViewer
{
    public partial class TradingInfo : BinanceForm
    {
        public TradingInfo(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            InitializeComponent();
            Service = new BinanceService(apiKey, apiSecret,10);
        }

        private void SynkWalletInfoWithList()
        {
            listView1.Items.Clear();
            listView1.SetObjects(walletInfo);
            listView1.Update();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var result=await InitWalletsAsync();
            if (result) SynkWalletInfoWithList();
        }
        private void RecountMidlPrice(List<HistoryItem> history)
        {
            double amountMoney = 0;
            double amount = 0;
            for (int i = 0; i < history.Count; i++)
            {
                var item = history[i];
                if (item.Type.Contains("MARKET"))
                {
                    item.Price = item.MidlePrice;//it is bad to buy in Market
                    continue;
                }

                if (item.Side == "BUY")
                {

                    amountMoney += item.ExecutedQty * item.Price;
                    amount += (1-Fee) * item.ExecutedQty;
                }
                else
                {

                    var curMidl = amount > 0 ? amountMoney / amount : 0;
                    item.Profit = Math.Round((1-Fee)* (item.Price - curMidl) * item.ExecutedQty,2);
                    amountMoney -= item.ExecutedQty * curMidl;
                    amount -= item.ExecutedQty;

                    if (amountMoney < 0 || amount < 0)
                    {
                        amountMoney = 0;
                        amount = 0;
                    }
                }
                if (amount != 0) item.MidlePrice = Math.Round(amountMoney / amount, 6);
            }
        }


        private async Task<List<HistoryItem>> getHistoryCurentCurrency(string currency)
        {
            var history = await Service.GetAllOrders(currency, opponentCurrency);
            var executed = history.Where(s => s.ExecutedQty > 0).ToList();
            RecountMidlPrice(executed);
            return executed;
        }

        private async void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curItem = listView2.SelectedItem;
            if (curItem == null) return;
            var tag=curItem.Group.Tag.ToString();
            var curDate = DateTime.Parse(tag);
            var curencyPair= listView1.SelectedItem;
            if (curItem == null) return;
            try
            {
                var history = await getHistoryCurentCurrency(SelectedPair);
                history=history.Where(s=>s.Date>curDate).ToList();
                RecountMidlPrice(history);
                listView2.SetObjects(history);
                listView2.Update();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\nError Occurred. {ex.Message}");
            }


        }

        private string SelectedPair;
        private  async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curItem = listView1.SelectedItem;
            if(curItem == null) return;
            try
            {
                SelectedPair=curItem.Text;                
                var history = await getHistoryCurentCurrency(curItem.Text);
                listView2.SetObjects(history);
                listView2.Update();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\nError Occurred. {ex.Message}");
            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            var credentials = Credentials.GetCredentials();
            apiKey = credentials[1].Key;
            apiSecret = credentials[1].Value;
            var user1Form = new TradingInfo(credentials[1].Key, credentials[1].Value);
            Service = new BinanceService(apiKey, apiSecret, 10);
            var result = await InitWalletsAsync();
            if (result) SynkWalletInfoWithList();
        }
    }
}
