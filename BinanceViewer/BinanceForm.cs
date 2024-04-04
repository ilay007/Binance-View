// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcountViewer;
using Castle.Core.Logging;
using log4net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CoinCore;
using System.Xml.Linq;

namespace BinanceAcountViewer
{
    public partial class BinanceForm : Form
    {

        protected IService Service;
        public string opponentCurrency = "USDT";
        public string apiKey = "";
        public string apiSecret = "";
        public List<CoinInfo> walletInfo;
        public string selectedCurrency= Properties.Settings.Default.SelectedCurrency;
        public ILog Ilogger;
        private double fee = Properties.Settings.Default.Fee;

        public double Fee
        {
            get { return fee; }
        }


        public string getCurrentPair()
        {
            return selectedCurrency+opponentCurrency;
        }

        public BinanceForm() :base()
        {
        }

        public BinanceForm(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            Ilogger = LogManager.GetLogger("mylog");


            InitializeComponent();

        }

        public void LogInfo(string message)
        {
            //Ilogger.Info(message);
            Console.WriteLine(message);
        }

        public async Task<List<HistoryItem>> getHistoryCurentCurrency(string currency)
        {
            var history = await Service.GetAllOrders(currency, opponentCurrency);
            var executed = history.Where(s => s.ExecutedQty > 0).ToList();
            RecountMidlPrice(executed);
            return executed;
        }

        public void RecountMidlPrice(List<HistoryItem> history)
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
                    amount += (1 - Fee) * item.ExecutedQty;
                }
                else
                {

                    var curMidl = amount > 0 ? amountMoney / amount : 0;
                    item.Profit = Math.Round((1 - Fee) * (item.Price - curMidl) * item.ExecutedQty, 2);
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

        public async Task<bool> InitWalletsAsync()
        {
            LogInfo("Start InitWalletAsync");
            walletInfo = await Service.GetWalletInfo();
            walletInfo = walletInfo.Where(s => s.Free > 0).ToList();            
            foreach (var coinInfo in walletInfo)
            {
                try
                {
                    LogInfo(coinInfo.Name);
                    if (coinInfo.IsLegalMoney || !coinInfo.Trading) continue;
                    if (coinInfo.Coin == opponentCurrency) continue;
                    var price = await Service.GetCurrentAveragePrice(coinInfo.Coin + opponentCurrency);
                    coinInfo.SetPrice(Math.Round(price, 2));

                }
                catch (Exception ex)
                {
                    return false;
                    Console.WriteLine($"{ex.Message}");
                }
            }
            return true;
        }

        private void BinanceForm_Load(object sender, EventArgs e)
        {

        }
    }
}
