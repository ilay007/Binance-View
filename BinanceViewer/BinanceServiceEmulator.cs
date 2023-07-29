using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Spot.Models;
using CoinCore;
using System.IO;
using Newtonsoft.Json;

namespace BinanceAcountViewer
{
    public class BinanceServiceEmulator : IService
    {
        private Dictionary<string, Dictionary<string, List<KLine>>> Data;
        private int CountTime = 0;
        private int MaxLenInterval = 500;
        private List<CoinInfo> WalletInfo;
        private int MaxCounterTimer;
        private int count = 0;
        private Cap CurrentCap;

        public void InitService(Dictionary<string, List<KLine>> data, int maxCounterTimer, bool just15min)
        {
            Data = new Dictionary<string, Dictionary<string, List<KLine>>>();
            var start = 500 * 16;
            if (just15min) start = 0;
            foreach (var pair in data)
            {
                var count = pair.Value.Count - (start - start / 16);
                var count4 = pair.Value.Count - (start - start / 4);
                var count16 = pair.Value.Count - (start - start);
                Data.Add(pair.Key, new Dictionary<string, List<KLine>>());
                Data[pair.Key].Add(Interval.FIFTEEN_MINUTE, pair.Value.GetRange(start - start / 16, count).ToList());
                if (!just15min)
                {
                    Data[pair.Key].Add(Interval.ONE_HOUR, GetIntervalFromKLines(4, pair.Value.GetRange(start - start / 4, count4).ToList()));
                    Data[pair.Key].Add(Interval.FOUR_HOUR, GetIntervalFromKLines(16, pair.Value.GetRange(0, count16).ToList()));
                }
            }
            MaxCounterTimer = maxCounterTimer;
            InitCap();
        }

        private void InitCap()
        {
            CurrentCap = new Cap();
            CurrentCap.Bids = new List<double[]>();
            CurrentCap.Bids.Add(new double[] { 10, 10 });
            CurrentCap.Asks = new List<double[]>();
            CurrentCap.Asks.Add(new double[] { 10, 10 });
        }

        public async Task<bool> CancelOpenedOrders(string symbol)
        {
            return true;
        }


        private List<KLine> GetIntervalFromKLines(int n, List<KLine> data)
        {
            var newLines = new List<KLine>();
            for (int i = 0; i < data.Count - n; i += n)
            {
                var range = data.GetRange(i, n);
                newLines.Add(new KLine(range));
            }
            return newLines;
        }

        public async Task<List<Order>> GetCurrentOpenedOrders(string pair)
        {
            return new List<Order>();
        }

        public async void NewOrder(string symbol, Side side, decimal newPrice, decimal quantity)
        {


        }

        public async Task<Cap> GetOrders(string symbol)
        {

            if (count >= MaxCounterTimer)
            {
                count = 0;
            }
            var curKLine = Data[symbol][Interval.FIFTEEN_MINUTE][CountTime + MaxLenInterval + 1];
            double price = 0d;
            switch (count)
            {
                case 0:
                    price = curKLine.Open;
                    break;
                case 1:
                    price = curKLine.Hight;
                    break;
                case 2:
                    price = curKLine.Low;
                    break;
                case 3:
                    price = curKLine.Close;
                    break;

            }
            count++;
            CurrentCap.Asks[0][0] = price;
            CurrentCap.Bids[0][0] = Math.Round(price * 0.999, 2);
            return CurrentCap;
        }

        public async Task<List<CoinInfo>> GetWalletInfo()
        {
            try
            {
                if (WalletInfo == null)
                {
                    var lines = File.ReadAllLines("wallet.txt").ToList();
                    WalletInfo = JsonConvert.DeserializeObject<List<CoinInfo>>(lines[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return WalletInfo;

        }




        public async Task<double> GetCurrentAveragePrice(string coinPair)
        {
            return 0;
        }

        public async Task<List<KLine>> GetKlineCandlestickData(string coinPair, Interval interval)
        {
            if (!Data.ContainsKey(coinPair)) return null;
            var s = interval.ToString();
            count = 0;

            int del = 1;
            switch (s)
            {
                case "1h":
                    del = 4;
                    break;
                case "4h":
                    del = 16;
                    break;
                case "15m":
                    CountTime++;
                    del = 1;
                    break;
                case "1d":
                    del = 96;
                    break;
            }
            try
            {
                return Data[coinPair][interval.ToString()].GetRange(CountTime / del, MaxLenInterval);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Object> GetSymbolOrderBook(string pair)
        {
            return null;
        }

        public async Task<List<HistoryItem>> GetAllOrders(string currency, string opponentCurrency)
        {
            return null;
        }
    }
}
