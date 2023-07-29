// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcountViewer;
using Binance.Spot.Models;
using Binance.Spot;
using CoinCore;
using Newtonsoft.Json;

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
            //return new Task<List>;
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
