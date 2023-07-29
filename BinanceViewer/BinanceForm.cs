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
