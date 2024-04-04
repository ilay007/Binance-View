using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Binance.Spot;
using Binance.Spot.Models;
using BinanceAcountViewer;
using Newtonsoft.Json;
using CoinCore;

namespace AcountViewer
{
    public class BinanceService:IService
    {
        private SpotAccountTrade spotAccountTrade;
        private HttpClient httpClient;
        private string apiKey = "";
        private string apiSecret = "";
        private Wallet wallet;
        private Market Market;

        public BinanceService(string apiKey, string apiSecret,int maxCounter)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            httpClient = new HttpClient();
            spotAccountTrade = new SpotAccountTrade(
              httpClient,
              apiKey: apiKey,
              apiSecret: apiSecret);
            Market = new Market(
              httpClient,
              apiKey: this.apiKey,
              apiSecret: this.apiSecret);
            wallet = new Wallet(
               httpClient,
               apiKey: this.apiKey,
               apiSecret: this.apiSecret);
        }

        public async Task<List<Order>> GetCurrentOpenedOrders(string pair)
        {
            try
            {
                var str = await spotAccountTrade.CurrentOpenOrders(pair);
                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(str);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<bool> CancelOpenedOrders(string symbol)
        {
            try
            {
                var res = spotAccountTrade.CancelAllOpenOrdersOnASymbol(symbol);
                return true;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public async void NewOrder(string symbol, Side side, decimal newPrice, decimal quantity)
        {
            try
            {
                await spotAccountTrade.NewOrder(symbol: symbol, side: side, type: OrderType.LIMIT, timeInForce: TimeInForce.GTC, quantity: quantity, quoteOrderQty: null, price: newPrice, recvWindow: 40000);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
         
        }
           
        
        
        public async Task<String> GetOrders(string symbol)
        {
           try
           {
                var res = await Market.OrderBook(symbol, 450);
                return res;
               

            }
            catch (Exception ex) 
            { 
                throw ex;
            }
          
        }

        public async Task<List<CoinInfo>> GetWalletInfo()
        {
            try
            {
                var result0 = await wallet.AllCoinsInformation();
                List<CoinInfo> walletInfo = JsonConvert.DeserializeObject<List<CoinInfo>>(result0);
                return walletInfo;
            }
            catch (Exception ex) 
            { 
                throw ex;
            }
            
        }

       
        

        public async Task<double> GetCurrentAveragePrice(string coinPair)
        {
            try
            {
                var res = await Market.CurrentAveragePrice(coinPair);
                var price = JsonConvert.DeserializeObject<AveragePrice>(res);
                return price.Price;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<KLine>> GetKlineCandlestickData(string coinPair,Interval interval)
        {
            try
            {
                var res2 = await Market.KlineCandlestickData(coinPair, interval);
                var data = JsonConvert.DeserializeObject<List<List<string>>>(res2);
                List<KLine> Candles = new List<KLine>();
                foreach (var line in data)
                {
                    Candles.Add(new KLine(line));
                }
                return Candles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }


        public async Task<Object> GetSymbolOrderBook(string pair)
        {
            try
            {
                var result = await Market.SymbolOrderBookTicker(pair);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<HistoryItem>> GetAllOrders(string currency, string opponentCurrency)
        {
            try
            {
                var ctxcOrders = await spotAccountTrade.AllOrders(currency + opponentCurrency);
                List<HistoryItem> history = JsonConvert.DeserializeObject<List<HistoryItem>>(ctxcOrders);
                var executed = history.Where(s => s.ExecutedQty > 0).ToList();
                foreach (var purchase in executed)
                {
                    purchase.Date = Stuff.BinanceTimeStampToUtcDateTime(purchase.Time);
                }
                return executed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
