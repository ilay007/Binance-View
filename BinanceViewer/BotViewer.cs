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
using Newtonsoft.Json;
using Strateges;
using System.Threading;
using Serilog;
using System.Diagnostics;

namespace BinanceAcountViewer
{
    public partial class BotViewer : BinanceForm
    {

        CoinsStore CoinsStore;
        private List<Interval> Intervals = new List<Interval>();
        private string PathToKnowledge;
        string opponentCurrency = "USDT";
        List<TradeCoinInfo> ListTradesCoins;
        private int MaxCounterTimer = 7;
        private bool just15min = false;
        private int WindowSize = 60;
        private Dictionary<string, Dictionary<string, KLine[]>> LastKLines = new Dictionary<string, Dictionary<string, KLine[]>>();
        private bool CanselDrawing = false;
        private bool RealMode = false;

        private string TradingCoinPath = "";
        private List<Order> OpenedOrders = new List<Order>();
        private List<Order> NotExecutedOrders = new List<Order>();
        private Object synckObj = new object();
        private Object coinsObj = new object();
        private bool Update = false;



        public BotViewer(string apiKey, string apiSecret) : base(apiKey, apiSecret)
        {
            /* Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log.txt")
            .CreateLogger();*/
            InitLoger();
            Log.Information("BotViewer is started");
            InitializeComponent();
            textBox1.Text = WindowSize.ToString();
            textBox2.Text = selectedCurrency;
            Init();
            
        }

       

        private void InitLoger()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.RollingFile(
              pathFormat: "logs\\log-{Date}.txt", // {Date} will be replaced with the date
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        }

        private void Init()
        {
            Intervals.Add(Interval.ONE_MINUTE);
            Intervals.Add(Interval.FIFTEEN_MINUTE);
            Intervals.Add(Interval.ONE_HOUR);
            Intervals.Add(Interval.FOUR_HOUR);
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            TradingCoinPath = Properties.Settings.Default.PathToTradingStateReal;
            FillTradingBotsData();
            InitStrategists();


            //Thead to control opende orders
            var th = new Thread(() => ControlOpenedOrders());
            th.Start();

            using (ApplicationContext db = new ApplicationContext())
            {
                var btcs = db.Sessions.Where(s => s.Pair == "BTCUSDT").ToList();
                var sessions = btcs.OrderBy(s => s.Date).ToList();
                int k = 1;
            }

        }

        private void FillTradingBotsData()
        {
            LoadListTradesCoins();
            CoinsStore = new CoinsStore(Intervals, 18, 9, 24);
            foreach (var interval in Intervals)
            {
                LastKLines.Add(interval, new Dictionary<string, KLine[]>());
                foreach (var coin in ListTradesCoins)
                {
                    LastKLines[interval].Add(coin.Name + opponentCurrency, new KLine[1]);
                }
            }
        }


        private void InitCriptoPairs()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                /* Pair pair1 = new Pair { Name = "ICP", ApponenName = "USDT" };
                 Pair pair2 = new Pair { Name = "FIRO", ApponenName = "USDT" };
                 Pair pair3 = new Pair { Name = "RVN", ApponenName = "USDT" };
                 Pair pair4 = new Pair { Name = "DOGE", ApponenName = "USDT" };
                 Pair pair5 = new Pair { Name = "MINA", ApponenName = "USDT" };
                 db.Pairs.AddRange(pair1, pair2, pair3, pair4, pair5);
                 db.SaveChanges();*/
            }

        }







        private async void ControlOpenedOrders()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                if (OpenedOrders.Count == 0 || Service == null) continue;
                try
                {

                    var coins = new List<string>();
                    lock (synckObj)
                    {
                        foreach (var order in OpenedOrders)
                        {
                            coins.Add(order.Symbol);
                        }
                    }
                    var waitingOrders = new List<Order>();
                    foreach (var coin in coins)
                    {
                        var curOpendOrders = Service.GetCurrentOpenedOrders(coin).Result.ToList();
                        if (curOpendOrders.Count == 0) continue;
                        waitingOrders.AddRange(curOpendOrders);
                    }
                    var executed = new List<Order>();
                    lock (synckObj)
                    {
                        foreach (var opOrder in OpenedOrders)
                        {
                            var finded = TryFindOrders(opOrder, waitingOrders);
                            if (finded == null)
                            {
                                CorrectListCoinInfo(opOrder);
                                executed.Add(opOrder);
                                continue;
                            }
                            if (finded.OrigQty != opOrder.ExecutedQty) //means that order was frequensy executed
                            {
                                CorrectListCoinInfo(opOrder);
                            }
                        }
                    }

                    foreach (var exOrder in executed)
                    {
                        LogInfo(exOrder.Side + "price=" + exOrder.Price + " balanceUSDT=" + exOrder.ExecutedQty * exOrder.Price + "CoinBalnce=" + exOrder.Price);
                        OpenedOrders.Remove(exOrder);
                    }
                    if (executed.Count > 0 && RealMode) Update = true;

                }
                catch (Exception ex)
                {
                    LogInfo(ex.Message);
                }

            }
        }

        private async void SynckAll()
        {
            Update = false;
            await SynckWithWallets();
            SaveListTradesCoins();
            SynkWalletInfoWithList();

        }


        public void CorrectListCoinInfo(Order order)
        {
            lock (coinsObj)
            {
                var curCoin = ListTradesCoins.FirstOrDefault(s => order.Symbol.Contains(s.Name));
                if (curCoin == null) return;
                if (order.Side == "BUY")
                {
                    var balanceBefore = curCoin.Balance;

                    curCoin.Balance += (1 - Fee) * (double)order.ExecutedQty;
                    curCoin.BalanceUSDT -= (double)(order.ExecutedQty * order.Price);
                    curCoin.LastPriceCoin = (curCoin.LastPriceCoin * balanceBefore + (double)(order.ExecutedQty * order.Price)) / curCoin.Balance;
                }
                else
                {
                    curCoin.Balance -= (double)order.ExecutedQty;
                    curCoin.BalanceUSDT += (1 - Fee) * (double)(order.ExecutedQty * order.Price);

                }
            }
        }


        private Order TryFindOrders(Order order, List<Order> orders)
        {
            var openedList = orders.Where(s => s.OrigQty == order.OrigQty && s.Symbol == order.Symbol && s.Side == order.Side).ToList();
            if (openedList.Count == 0) return null;
            return openedList[0];
        }

        private void InitStrategists()
        {
            /*var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
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
            }*/

            foreach (var interval in Intervals)
            {
                foreach (var coin in ListTradesCoins)
                {

                    CoinsStore.AddStrategist(interval, coin.Name + opponentCurrency, new Strategist());
                    if (interval == Interval.ONE_HOUR) CoinsStore.Strategists[interval][coin.Name + opponentCurrency].SetTimeLemit(6);
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
            listView2.Items.Clear();
            listView2.SetObjects(ListTradesCoins);
            listView2.Update();

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
                    /*if (coins[0].Balance < 20)
                    {
                        coin.BalanceUSDT = Math.Min(coin.BalanceUSDT, USDT.Free);
                        coin.LastPriceCoin = coins[0].Balance / coins[0].Free;
                    }
                    else
                    {                        
                        coin.LastPriceCoin = coins[0].Balance / coins[0].Free;                     

                    }*/
                    LogInfo("Sync " + coin.Name);
                }
            }
            return true;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Service = new BinanceService(apiKey, apiSecret, MaxCounterTimer);
            TradingCoinPath = Properties.Settings.Default.PathToTradingStateReal;
            label3.Text = "Real time mode";
            RealMode = true;
            var res = false;
            foreach (var interval in Intervals)
            {
                foreach (var coin in ListTradesCoins)
                {
                    var curPair = coin.Name + opponentCurrency;
                    res = await UpdateKLines(interval, curPair);
                }
            }
            foreach (var coin in ListTradesCoins)
            {
                Service.CancelOpenedOrders(coin.Name);
            }
            LoadListTradesCoins();
            foreach (var coin in ListTradesCoins)
            {
                Log.Information(coin.ToString());
            }
            foreach (var coin in ListTradesCoins)
            {
                var history = await getHistoryCurentCurrency(coin.Name);
                coin.LastSellPrice = CountLastPrice(history, "SELL");
                coin.LastBuyPrice = CountLastPrice(history, "BUY");
            }
            await SynckWithWallets();
            SaveListTradesCoins();
            if (!res) return;
            ReDraw();
            StartTimer(5000);
        }

        private double CountLastPrice(List<HistoryItem> history, string patern)
        {
            if (history.Count == 1) return history[0].Price;
            int count = history.Count - 1;
            while (history[count].Side != patern) count--;
            double balanceUsdt = 0;
            double balanceCoin = 0;
            for (int i = count; i > 0; i--)
            {
                if (history[i].Side == patern)
                {
                    balanceUsdt += history[i].ExecutedQty * history[i].Price;
                    balanceCoin += history[i].ExecutedQty;

                    continue;
                }
                break;
            }
            return balanceUsdt / balanceCoin;

        }

        private bool ReDraw()
        {
            if (Service == null) return false;
            var point = pictureBox1.PointToClient(Cursor.Position);
            DrawKLines(pictureBox7, null, Interval.ONE_MINUTE);
            DrawKLines(pictureBox1, pictureBox2, Interval.FIFTEEN_MINUTE);

            if (pictureBox1.Image == null) return false;
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
            richTextBox1.Text = "";
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


        public async Task<bool> UpdateKLines(Interval interval, string currentPair)
        {
            if (Service == null) return false;
            //if (just15min && interval != Interval.FIFTEEN_MINUTE) return false;
            var data = await Service.GetKlineCandlestickData(currentPair, interval);
            if (!RealMode)
            {
                var last = data.Last();
                data.RemoveAt(data.Count - 1);
                LastKLines[interval][currentPair][0] = last;
                CoinsStore.TryAddData(data, interval.ToString(), currentPair);
                return true;
            }
            LastKLines[interval][currentPair][0] = new KLine(data.Last().OpenTime, data.Last().Close);
            CoinsStore.TryAddData(data, interval.ToString(), currentPair);
            return true;
        }


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
            var start = data.KLines.Count - numPoints - curPoint;
            Drawer.DrawKLines(curImage, data.KLines.GetRange(start, numPoints));
            var max = data.KLines.Select(s => s.Hight).ToList().GetRange(start, numPoints).Max();
            var min = data.KLines.Select(s => s.Low).ToList().GetRange(start, numPoints).Min();
            Drawer.DrawGrapth(curImage, data.Boll.CurveHight.GetRange(start, numPoints), Color.Violet, max, min);
            Drawer.DrawGrapth(curImage, data.Boll.Ema.EmaPoints.GetRange(start, numPoints), Color.Brown, max, min);
            Drawer.DrawGrapth(curImage, data.Boll.CurveLow.GetRange(start, numPoints), Color.Orange, max, min);
            picture1.Image = curImage;
            if (picture2 == null) return;
            var image2 = new Bitmap(picture2.Width, picture2.Height);
            Graphics g2 = Graphics.FromImage(image2);
            g2.Clear(Color.White);
            var curRange12 = data.FastEma.EmaPoints.GetRange(start, numPoints);
            var curRange26 = data.SlowEma.EmaPoints.GetRange(start, numPoints);
            Drawer.DrawAsHistogram(image2, data.DifEma.GetRange(start, numPoints));
            Drawer.DrawGrapth(image2, curRange12, Color.Violet, curRange12.Max(), curRange12.Min());
            Drawer.DrawGrapth(image2, curRange26, Color.Pink, curRange12.Max(), curRange12.Min());
            Drawer.SignImage(curImage, currentPair + "/" + interval.ToString());
            picture2.Image = image2;
        }

        private async void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curItem = listView2.SelectedItem;
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


        private void LoadListTradesCoins()
        {
            ListTradesCoins = new List<TradeCoinInfo>();
            try
            {
                var lines = File.ReadAllLines(TradingCoinPath);
                ListTradesCoins = JsonConvert.DeserializeObject<List<TradeCoinInfo>>(lines[0]);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
            }

        }



        private void SaveWalletInfo()
        {
            var walletInf = JsonConvert.SerializeObject(walletInfo);
            using (StreamWriter writer = new StreamWriter(Properties.Settings.Default.PathToWalletFile))
            {
                writer.WriteLine(walletInf);
                writer.Close();
            }

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


        private void BuyCoin(TradeCoinInfo coin, double priceToBuy, bool inProfit)
        {

            if (coin == null || coin.BalanceUSDT < 20) return;
            var curPair = coin.Name + opponentCurrency;
            //TO DO
            if (inProfit && priceToBuy > (1 - 2 * Fee) * coin.LastBuyPrice) return;
            var q = ((coin.BalanceUSDT > 40 ? 0.5 * coin.BalanceUSDT : coin.BalanceUSDT) / priceToBuy);
            var iq = (int)(10 * q);
            q = ((double)iq / 10);
            if (priceToBuy < 1) q = Math.Round(q - 1, 0);
            Service.NewOrder(curPair, Side.BUY, (decimal)priceToBuy, (decimal)q);
            Log.Information(curPair + " num_coins=" + q + " buy price=" + priceToBuy + " balanceUSDT=" + coin.BalanceUSDT);
            var curOrder = new Order(curPair, (decimal)priceToBuy, (decimal)q, Side.BUY);
            lock (synckObj)
            {
                OpenedOrders.Add(curOrder);
            }
            Console.WriteLine(LastKLines[Interval.FIFTEEN_MINUTE][curPair][0].GetOpenDate());
        }


        private void SellCoin(TradeCoinInfo coin, double priceToSell, bool inProfit)
        {

            if (coin == null || coin.Balance * priceToSell < 20) return;
            var curPair = coin.Name + opponentCurrency;
            if (coin.Name == "BTC") return;
            if (inProfit && priceToSell < (1 + 2 * Fee + 0.07) * coin.LastBuyPrice) return;
            var balance = coin.Balance * priceToSell > 40 ? 0.5 * coin.Balance : coin.Balance;
            double p = priceToSell;
            int count = 1;
            while (p > 1)
            {
                p /= 10;
                count++;
            }

            int mn = (int)Math.Pow(10, (double)count);
            var ibalance = (int)(mn * balance);
            balance = ((double)ibalance) / mn;
            if (priceToSell < 1) balance = Math.Round(balance - 1, 0);
            try
            {
                Service.NewOrder(curPair, Side.SELL, (decimal)priceToSell, (decimal)balance);
            }
            catch (Exception ex)
            {
                Log.Error("Trying to sell " + curPair + " " + ex.Message.ToString());
                return;
            }
            Log.Information(curPair + " num_coins=" + balance + " sell price=" + priceToSell + " balanceUSDT=" + coin.BalanceUSDT);
            var curOrder = new Order(curPair, (decimal)priceToSell, (decimal)balance, Side.SELL);
            lock (synckObj)
            {
                OpenedOrders.Add(curOrder);
            }
            Console.WriteLine(LastKLines[Interval.FIFTEEN_MINUTE][curPair][0].GetOpenDate());
        }


        private bool enableDateSavingInDb = true;

        private void SavingBidsInDb(Cap cap, string curPair)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Sessions.Add(new Session { Date = ulong.Parse(cap.LastUpdateId), Pair = curPair });
                db.SaveChanges();

            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var session = db.Sessions.First(s => s.Date == ulong.Parse(cap.LastUpdateId));
                for (int i = 0; i < cap.Asks.Count; i++)
                {
                    db.Orders.Add(new OrderH { IdSession = session.Id, Num = i, Price = cap.Asks[i][0], Quantity = cap.Asks[i][1], Type = "Ask" });

                }
                for (int i = 0; i < cap.Bids.Count; i++)
                {
                    db.Orders.Add(new OrderH { IdSession = session.Id, Num = i, Price = cap.Bids[i][0], Quantity = cap.Bids[i][1], Type = "Bid" });
                }
                db.SaveChanges();

            }
        }



        private async void timer1_Tick(object sender, EventArgs e)
        {
            countTimer++;
            richTextBox1.Clear();

            var Waps = new List<string>();
            StringBuilder builderOrders = new StringBuilder();
            try
            {
                var pair = getCurrentPair();
                foreach (var coin in ListTradesCoins)
                {
                    var curPair = coin.Name + opponentCurrency;
                    String dataCap = await Service.GetOrders(curPair);
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    Cap cap = JsonConvert.DeserializeObject<Cap>(dataCap);
                    var priceToBuy = cap.Asks[0][0];//want to sell
                    var priceToSell = cap.Bids[0][0];//want to buy
                    var sell0 = 0d;
                    var sellCount = 0d;
                    var buy0 = 0d;
                    var buyCount = 0d;
                    if (enableDateSavingInDb) SavingBidsInDb(cap, curPair);


                    for (int i = 0; i < 40; i++)
                    {
                        sell0 += cap.Asks[i][0] * cap.Asks[i][1];
                        sellCount += cap.Asks[i][1];
                        buy0 += cap.Bids[i][0] * cap.Bids[i][1];
                        buyCount += cap.Bids[i][1];
                    }
                    var midlSell = sell0 / sellCount;
                    var midlBuy = buy0 / buyCount;
                    var wap = (sell0 + buy0) / (sellCount + buyCount);
                    Waps.Add(curPair + " " + priceToSell + " " + Math.Round(wap, 5));
                    // cap.Bids[0][0];                
                    // Log.Information(curPair + dataCap);
                    //Console.WriteLine(stopWatch.ElapsedMilliseconds);
                    var del = Fee * cap.Bids[0][0];
                    var openedOrders = await Service.GetCurrentOpenedOrders(curPair);
                    AddOpenOrders(builderOrders, openedOrders);
                    foreach (var interval in Intervals)
                    {
                        DateTime currentDateTime = DateTime.Now;
                        if (!RealMode || (currentDateTime.Minute % (interval.GetNum())) == 0)
                        {
                            var res1 = await UpdateKLines(interval, curPair);
                            if (!res1) return;
                        }
                        var point = cap.Bids[0][0];
                        if (RealMode)
                        {
                            CoinsStore.LinesHistory[interval][curPair].ChangeLastPoint(cap.Bids[0][0]);
                        }
                        else
                        {
                            LastKLines[interval][curPair][0].Insert(cap.Bids[0][0]);
                            CoinsStore.AddPoint(interval, curPair, LastKLines[interval][curPair][0]);
                        }

                        lock (synckObj)
                        {
                            if (OpenedOrders.FirstOrDefault(s => s.Symbol == curPair) != null) continue;
                        }
                        if (interval != Interval.ONE_MINUTE) continue;

                        var prediction = CoinsStore.MakePrediction(curPair, point);


                        if (prediction != "Nothing" && coin.Name != "BTC")
                        {
                            Log.Information(curPair + " prediction=  <<" + prediction + ">> price=" + priceToBuy);
                        }


                        //Service.CancelOpenedOrders(curPair);
                        //if (priceToBuy > (1.017) * coin.LastPriceCoin) prediction = Prediction.SELL;
                        // if (prediction != Prediction.NOTHING) LogInfo(prediction.ToString() + "price is " + cap.Bids[0][0].ToString());
                        if ((prediction.Value == Prediction.BUY))
                        {
                            BuyCoin(coin, priceToBuy, true);
                        }
                        else if ((prediction.Value == Prediction.SELL))
                        {
                            SellCoin(coin, priceToSell, true);
                        }

                    }
                    if (pair == curPair && !CanselDrawing)
                    {
                        var res = ReDraw();
                        FillRichBook(cap);
                    }

                    foreach (var interval in Intervals)
                    {
                        if (cap != null && !RealMode) CoinsStore.RemoveLastPoint(interval, curPair);
                    }


                }
                if (Update) SynckAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Service = new BinanceService(apiKey, apiSecret, MaxCounterTimer);
                // SynckWithWallets();
            }
            if (countTimer % 10 != 0) return;
            richTextBox2.Clear();
            var builder = new StringBuilder();
            foreach (var w in Waps)
            {

                builder.Append("\n");
                builder.Append(w);
            }
            richTextBox2.AppendText(builder.ToString());
            var curOrders = builderOrders.ToString();
            if (LastBuildOrders != curOrders)
            {
                richTextBox3.Clear();
                richTextBox3.AppendText(curOrders);
                LastBuildOrders = curOrders;
            }

        }


        private void AddOpenOrders(StringBuilder builderOrders, List<Order> openedOrders)
        {
            foreach (var order in openedOrders)
            {
                builderOrders.Append(order.Symbol);
                builderOrders.Append(" ");
                builderOrders.Append(order.Side);
                builderOrders.Append(" ");
                builderOrders.Append(order.Type);
                builderOrders.Append(" ");
                builderOrders.Append(Math.Round(order.Price, 3));
                builderOrders.Append(" ");
                builderOrders.Append(Math.Round(order.OrigQty, 3));
                builderOrders.Append(" ");
                builderOrders.Append("\n");
            }
        }

        String LastBuildOrders = "";

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
            StartTimer(20);
            foreach (var interval in Intervals)
            {
                UpdateKLines(interval, GetCurrentPair());
            }
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


        private async void button6_Click(object sender, EventArgs e)
        {
            label3.Text = "Test mode";
            selectedCurrency = textBox2.Text.ToString();
            Service = new BinanceServiceEmulator();
            TradingCoinPath = Properties.Settings.Default.PathToTradingStateTrain;
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
                    //break;
                }
            }
            Service.InitService(dic, MaxCounterTimer, just15min);
            LoadListTradesCoins();
            await SynckWithWallets();
            SaveListTradesCoins();
            StartTimer(100);
            foreach (var interval in Intervals)
            {
                UpdateKLines(interval, GetCurrentPair());
            }

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

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            SynckAll();
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            var priceToBuy = Convert.ToDouble(textBoxPrice.Text);
            foreach (var coin in ListTradesCoins)
            {
                if (coin.Name == selectedCurrency)
                {
                    BuyCoin(coin, priceToBuy, false);
                }
            }
        }

        private void buttonSell_Click(object sender, EventArgs e)
        {
            var priceToSell = Convert.ToDouble(textBoxPrice.Text);
            foreach (var coin in ListTradesCoins)
            {
                if (coin.Name == selectedCurrency) SellCoin(coin, priceToSell, false);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Service.CancelOpenedOrders(getCurrentPair());
        }

        private void tableLayoutPanel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}




