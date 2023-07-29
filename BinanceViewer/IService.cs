using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Spot.Models;
using CoinCore;

namespace BinanceAcountViewer
{
    public interface IService
    {
        
        public async Task<bool> CancelOpenedOrders(string symbol)
        {
            return true;
        }

        public void InitService(Dictionary<string,List<KLine>> data,int maxCounter,bool just15min)
        {

        }

        public async Task<List<Order>> GetCurrentOpenedOrders(string pair)
        {            
            return null;
        }

        public async void NewOrder(string symbol, Side side, decimal newPrice, decimal quantity)
        {

          
        }

        public async Task<Cap> GetOrders(string symbol)
        {
            return null;
        }

        public async Task<List<CoinInfo>> GetWalletInfo()
        {
            return null;
        }

        public async Task<double> GetCurrentAveragePrice(string coinPair)
        {
            return 0;
        }

        public async Task<List<KLine>> GetKlineCandlestickData(string coinPair, Interval interval)
        {
            return null;
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
