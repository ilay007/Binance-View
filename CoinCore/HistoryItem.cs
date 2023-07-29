using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCore
{
    public class HistoryItem
    {
        public string Symbol;
        public string orderId;
        public double Price;
        public double OrigQty;
        public string Status;
        public double ExecutedQty;
        public string timeInForce;
        public string Type;
        public string Side;
        public double  Time;
        public string workingTime;
        public double MidlePrice;
        public double Profit;
        public DateTime Date;
    }
}
