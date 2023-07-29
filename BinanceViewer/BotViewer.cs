// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcountViewer;
using Binance.Spot.Models;
using BinanceTradingDrawer;
using System.Net.Http;
using CoinCore;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace BinanceAcountViewer
{
    public partial class BotViewer : BinanceForm
    {

        CoinsStore CoinsStore = new CoinsStore(18, 9, 24);
        private List<Interval> Intervals = new List<Interval>();
        private string PathToKnowledge = "Knowledges.txt";
        string opponentCurrency = "USDT";
        List<TradeCoinInfo> ListTradesCoins;
        private int MaxCounterTimer = 4;
        private bool just15min = false;
        private int WindowSize = 60;
        private Dictionary<string, KLine[]> LastKLines = new Dictionary<string, KLine[]>();


        public BotViewer(string apiKey, string apiSecret) : base(apiKey, apiSecret)
        {
            InitializeComponent();
            textBox1.Text = WindowSize.ToString();
            textBox2.Text = selectedCurrency;
            Intervals.Add(Interval.FIFTEEN_MINUTE);
            Intervals.Add(Interval.ONE_HOUR);
            Intervals.Add(Interval.FOUR_HOUR);
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            ListTradesCoins = new List<TradeCoinInfo>();
            ListTradesCoins.Add(new TradeCoinInfo(selectedCurrency, 0, 40d));          
            foreach (var coin in ListTradesCoins)
            {
                LastKLines.Add(coin.Name + opponentCurrency, new KLine[1]);
            }
            InitStrategists();
        }

        private void InitStrategists()
        {           
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach (var f in dir.GetFiles("*.dll"))
            {
                var types = Assembly.LoadFile(f.FullName).GetTypes();
                foreach (var t in types)
                {
                    if (t.GetInterfaces().Contains(typeof(IStrategist)))
                    {
                        var attr = t.GetCustomAttribute(typeof(CoinStrategistAttribute)) as CoinStrategistAttribute;
                        var name = attr.Name;
                        foreach (var interval in Intervals)
                        {
                            foreach (var coin in ListTradesCoins)
                            {
                                CoinsStore.AddStrategist(interval, coin.Name + opponentCurrency, Activator.CreateInstance(t) as IStrategist);
                            }
                        }
                    }
                }
            }
            if(CoinsStore.Strategists.Count==0)
            {
                foreach (var interval in Intervals)
                {
                    foreach (var coin in ListTradesCoins)
                    {
                        CoinsStore.AddStrategist(interval, coin.Name + opponentCurrency, new EmptyStrategist());
                    }
                }

            }
        }



        private void StartTimer(int timeMs)
        {
            this.timer1.Interval = timeMs;
            timer1.Tick += timer1_Tick;
            timer1.Start();

        }

        public BotViewer() : base() { }

        private int curPoint = 0;
        private int numPoints = 50;

        private string GetCurrentPair()
        {
            return selectedCurrency + opponentCurrency;
        }


        private void SynkWalletInfoWithList()
        {
            listView1.Items.Clear();
            listView1.SetObjects(walletInfo);
            listView1.Update();
        }

        private async Task<bool> SynckWithWallets()
        {
            var ok = await InitWalletsAsync();
            if (ok) SynkWalletInfoWithList();
            LogInfo("Start wallets synck");
            if (walletInfo == null && !ok) return false;
            LogInfo("wallet info is not null");
            foreach (var coin in ListTradesCoins)
            {
                var coins = walletInfo.Where(s => s.Coin == coin.Name).ToList();
                var USDT = walletInfo.Where(s => s.Coin == "USDT").ToList()[0];
                if (coins.Count > 0)
                {
                    coin.Balance = coins[0].Free;
                    if (coins[0].Balance < 20)
                    {
                        coin.BalanceUSDT = Math.Min(coin.BalanceUSDT, USDT.Free);
                        coin.LastPriceCoin = coins[0].Balance / coins[0].Free;
                    }
                    else
                    {
                        coin.BalanceUSDT = 0;
                        coin.LastPriceCoin = coins[0].Balance / coins[0].Free;

                    }
                    LogInfo("Sync " + coin.Name);
                }
            }
            return true;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Service = new BinanceService(apiKey, apiSecret, MaxCounterTimer);
            label3.Text = "Real time mode";
            var res = false;
            foreach (var pair in LastKLines)
            {
                res = await UpdateKLines(pair.Key);
            }
            SynckWithWallets();
            if (!res) return;
            ReDraw();
            StartTimer(2000);
        }

        private bool ReDraw()
        {
            if (Service == null) return false;
            var point = pictureBox1.PointToClient(Cursor.Position);
            DrawKLines(pictureBox1, pictureBox2, Interval.FIFTEEN_MINUTE);
            if (pictureBox1.Image == null) return false;
            var width1 = pictureBox1.Width;
            var width3 = pictureBox3.Width;
            var width5 = pictureBox5.Width;
            Graphics g = Graphics.FromImage((Image)pictureBox1.Image);
            g.DrawLine(new Pen(Color.Black), point.X, 0, point.X, pictureBox1.Image.Height);
            if (!just15min)
            {
                DrawKLines(pictureBox3, pictureBox4, Interval.ONE_HOUR);
                DrawKLines(pictureBox5, pictureBox6, Interval.FOUR_HOUR);
                if (pictureBox3.Image == null) return false;
                var x1 = (point.X + 3 * width3) / 4;
                Graphics g1 = Graphics.FromImage((Image)pictureBox3.Image);
                g1.DrawLine(new Pen(Color.Black), x1, 0, x1, pictureBox3.Image.Height);

                if (pictureBox5.Image == null) return false;
                var x2 = (x1 + 3 * width5) / 4;
                Graphics g2 = Graphics.FromImage((Image)pictureBox5.Image);
                g2.DrawLine(new Pen(Color.Black), x2, 0, x2, pictureBox5.Image.Height);
            }
            return true;

        }

        private async void FillRichBook(Cap cap)
        {
            if (cap == null) return;
            var builder = new StringBuilder();
            cap.Asks.Reverse();
            richTextBox1.SelectionFont = new Font("Arial", 12);
            richTextBox1.SelectionColor = Color.Red;
            var len = Math.Min(3, cap.Asks.Count);
            var asks = cap.Asks.GetRange(0, len);
            foreach (var item in asks)
            {
                builder.Append("\n");
                builder.Append(item[0]);
                builder.Append("    ");
                builder.Append(item[1]);
                richTextBox1.AppendText(builder.ToString());
                builder.Clear();
            }
            builder.Clear();
            builder.Append("\n");
            richTextBox1.SelectionColor = Color.Green;
            var bids = cap.Bids.GetRange(0, len);
            foreach (var item in bids)
            {
                builder.Append("\n");
                builder.Append(item[0]);
                builder.Append("    ");
                builder.Append(item[1]);
                richTextBox1.AppendText(builder.ToString());
                builder.Clear();
            }

        }


        public async Task<bool> UpdateKLines(string currentPair)
        {
            if (Service == null) return false;
            foreach (var interval in Intervals)
            {
                if (just15min && interval != Interval.FIFTEEN_MINUTE) continue;
                var data = await Service.GetKlineCandlestickData(currentPair, interval);
                if (interval == Interval.FIFTEEN_MINUTE)
                {
                    LastKLines[currentPair][0] = data.Last();
                    data.RemoveAt(data.Count - 1);
                }
                var lastData = data.Last();
                CoinsStore.TryAddData(data, interval.ToString(), currentPair);
            }
            return true;
        }

        private Order obj = new Order();
        public void DrawKLines(PictureBox picture1, PictureBox picture2, Interval interval)
        {
            numPoints = Convert.ToInt16(textBox1.Text);
            var currentPair = GetCurrentPair();
            if (!CoinsStore.LinesHistory.ContainsKey(interval.ToString())) return;
            if (!CoinsStore.LinesHistory[interval.ToString()].ContainsKey(currentPair)) return;
            var data = CoinsStore.LinesHistory[interval.ToString()][currentPair];
            var curImage = new Bitmap(picture1.Width, picture1.Height);
            Graphics g = Graphics.FromImage(curImage);
            g.Clear(Color.White);
            var count = data.KLines.Count;
            var lastrange = data.KLines.GetRange(data.KLines.Count() - 5, 4);
            var start = data.KLines.Count - numPoints - curPoint - 1;
            Drawer.DrawKLines(curImage, data.KLines.GetRange(start, numPoints));
            var max = data.KLines.Select(s => s.Hight).ToList().GetRange(start, numPoints).Max();
            var min = data.KLines.Select(s => s.Low).ToList().GetRange(start, numPoints).Min();
            Drawer.DrawGrapth(curImage, data.Boll.CurveHight.GetRange(start, numPoints), Color.Violet, max, min);
            Drawer.DrawGrapth(curImage, data.Boll.Ema.EmaPoints.GetRange(start, numPoints), Color.Brown, max, min);
            Drawer.DrawGrapth(curImage, data.Boll.CurveLow.GetRange(start, numPoints), Color.Orange, max, min);
            var image2 = new Bitmap(picture2.Width, picture2.Height);
            Graphics g2 = Graphics.FromImage(image2);
            g2.Clear(Color.White);
            var curRange12 = data.FastEma.EmaPoints.GetRange(start, numPoints);
            var curRange26 = data.SlowEma.EmaPoints.GetRange(start, numPoints);
            Drawer.DrawAsHistogram(image2, data.DifEma.GetRange(start, numPoints));
            Drawer.DrawGrapth(image2, curRange12, Color.Violet, curRange12.Max(), curRange12.Min());
            Drawer.DrawGrapth(image2, curRange26, Color.Pink, curRange12.Max(), curRange12.Min());
            Drawer.SignImage(curImage, currentPair + "/" + interval.ToString());
            picture1.Image = curImage;
            picture2.Image = image2;
        }

        private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curItem = listView1.SelectedItem;
            if (curItem == null) return;
            try
            {
                selectedCurrency = curItem.Text;
                //UpdateKLines();
                var currentPair = GetCurrentPair();
                foreach (var interval in Intervals)
                {
                    if (just15min && interval != Interval.FIFTEEN_MINUTE) continue;
                    var data = await Service.GetKlineCandlestickData(currentPair, interval);
                    CoinsStore.TryAddData(data, interval.ToString(), currentPair);
                }
                ReDraw();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\nError Occurred. {ex.Message}");
            }
        }

        private int countTimer = 6;
        
        private async void timer1_Tick(object sender, EventArgs e)
        {
            countTimer++;
            richTextBox1.Clear();
            try
            {
                var pair = getCurrentPair();
                foreach (var coin in ListTradesCoins)
                {
                    var curPair = coin.Name + opponentCurrency;
                    //DateTime currentDateTime = DateTime.Now;
                    //if (countTimer > 65&&currentDateTime.Minute%15==0)
                    if (countTimer > 65)
                    {                        
                        countTimer = 0;                       
                        var res1 = await UpdateKLines(curPair);                     
                        if (!res1) return;
                    }
                    Cap cap = await Service.GetOrders(curPair);
                    var priceToBuy = cap.Asks[0][0];
                    var priceToSell = cap.Bids[0][0];
                    var del = Fee * cap.Bids[0][0];
                    LastKLines[curPair][0].Insert(cap.Bids[0][0]);
                    CoinsStore.AddPoint(Interval.FIFTEEN_MINUTE, curPair, LastKLines[curPair][0]);
                    var timer = new Stopwatch();
                    timer.Start();
                    if (pair == curPair)
                    {
                        var res = ReDraw();
                    }
                    var time = timer.ElapsedMilliseconds;
                    if (cap != null) CoinsStore.RemoveLastPoint(Interval.FIFTEEN_MINUTE, curPair);
                    FillRichBook(cap);
                    var openOrders = await Service.GetCurrentOpenedOrders(curPair);
                    if (openOrders.Count > 0)
                    {
                        var isCanceled = Service.CancelOpenedOrders(curPair);
                        continue;
                    }
                    var prediction = CoinsStore.MakePrediction(Interval.FIFTEEN_MINUTE, curPair);                    
                    if (prediction != Prediction.NOTHING) LogInfo(prediction.ToString() + "price is " + cap.Bids[0][0].ToString());
                    if (prediction.Value == Prediction.BUY && coin.BalanceUSDT > 20)
                    {
                        if (priceToBuy > (1 - Fee) * coin.LastPriceCoin) continue;
                        var q = coin.BalanceUSDT / priceToBuy;
                        var iq = (int)(10 * q);
                        q = ((double)iq / 10);
                        Service.NewOrder(curPair, Side.BUY, (decimal)priceToBuy, (decimal)q);
                        coin.Balance = (1 - Fee) * coin.BalanceUSDT / priceToBuy;
                        coin.LastPriceCoin = priceToBuy;
                        coin.BalanceUSDT = 0;
                        LogInfo("buy price=" + priceToBuy + " balanceUSDT=" + coin.BalanceUSDT);
                        SynckWithWallets();
                    }
                    else if (prediction.Value == Prediction.SELL && coin.Balance * priceToSell > 20)
                    {
                        if (priceToSell < (1 + 2 * Fee) * coin.LastPriceCoin) continue;
                        var balance = coin.Balance;
                        var ibalance = (int)(10 * balance);
                        balance = ((double)ibalance) / 10;
                        Service.NewOrder(curPair, Side.SELL, (decimal)priceToSell, (decimal)balance);
                        coin.LastPriceCoin = priceToSell;
                        coin.BalanceUSDT = (1 - Fee) * coin.Balance * priceToSell;
                        coin.Balance = 0;
                        LogInfo("sell price=" + priceToSell + " balanceUSDT=" + coin.BalanceUSDT);
                        SynckWithWallets();
                    }

                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.ToString());
                Service = new BinanceService(apiKey, apiSecret, MaxCounterTimer);
                // SynckWithWallets();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Service = new BinanceServiceEmulator();
            InitWalletsAsync();
            var dirs = Directory.GetDirectories(Properties.Settings.Default.TestData).ToList();
            var dic = new Dictionary<string, List<KLine>>();
            foreach (var dir in dirs)
            {
                var folders = Directory.GetDirectories(dir);
                var param = folders[0].ToString().Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var pairName = param[param.Count - 2];
                if (!dic.ContainsKey(pairName)) dic.Add(pairName, new List<KLine>());
                foreach (var folder in folders)
                {
                    var files = Directory.GetFiles(folder).ToList();
                    var kLines = new List<KLine>();
                    foreach (var file in files)
                    {
                        var lines = File.ReadAllLines(file);
                        foreach (var line in lines)
                        {
                            var parts = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var kLine = new KLine(parts);
                            kLines.Add(kLine);
                        }
                    }
                    dic[pairName].AddRange(kLines);
                }
            }
            Service.InitService(dic, MaxCounterTimer, just15min);
            StartTimer(100);
            UpdateKLines(GetCurrentPair());
            ReDraw();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var point = pictureBox1.PointToClient(Cursor.Position);
            var currentPair = GetCurrentPair();
            var widhStep = Drawer.CountStepWidth(pictureBox1.Width, numPoints);
            int numberStep = point.X / widhStep + 1;
            var numLastSteps = 4;
            var start = numberStep + numLastSteps;
            if (BuyMode) CoinsStore.AddKnowledges(currentPair, start, true);
            if (SellMode) CoinsStore.AddKnowledges(currentPair, start, false);

        }


        private string toString(List<int[]> list)
        {
            var builder = new StringBuilder();
            for (int k = 0; k < list.Count; k++)
            {
                for (int i = 0; i < list[k].Length; i++)
                {
                    builder.Append(list[k][i].ToString());
                }
            }
            return builder.ToString();
        }

        private bool SellMode;
        private bool BuyMode;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SellMode = !SellMode;
            BuyMode = false;
            checkBox2.Enabled = !SellMode;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            BuyMode = !BuyMode;
            SellMode = false;
            checkBox1.Enabled = !BuyMode;
        }

        private void checkBox1_MouseMove(object sender, MouseEventArgs e)
        {
            ReDraw();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CoinsStore.SaveKnowledges(PathToKnowledge);

        }


        private void button5_Click(object sender, EventArgs e)
        {
            selectedCurrency = textBox2.Text.ToString();
            CoinsStore.TeachStrategists(PathToKnowledge);

        }


        private void button6_Click(object sender, EventArgs e)
        {
            label3.Text = "Test mode";
            selectedCurrency = textBox2.Text.ToString();
            Service = new BinanceServiceEmulator();
            SynckWithWallets();
            var files = Directory.GetFiles(Properties.Settings.Default.DataBase);
            var dic = new Dictionary<string, List<KLine>>();
            var pair = getCurrentPair();
            if (!dic.ContainsKey(pair)) dic.Add(pair, new List<KLine>());
            foreach (var file in files)
            {
                if (file.Contains(selectedCurrency))
                {
                    var lines = File.ReadAllLines(file).ToList();
                    var kLines = new List<KLine>();
                    for (int i = 1; i < lines.Count - 1; i++)
                    {
                        var line = lines[i];
                        var parts = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var kLine = new KLine(parts);
                        kLines.Add(kLine);
                    }
                    dic[pair].AddRange(kLines);
                    break;
                }
            }
            Service.InitService(dic, MaxCounterTimer, just15min);
            StartTimer(100);
            UpdateKLines(GetCurrentPair());
            ReDraw();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

    }
}




