namespace CoinCore
{
    public class KLine
    {
        public DateTime OpenTime;
        public DateTime CloseTime;
        public Double Open;
        public Double Hight;
        public Double Low;
        public Double Close;
    
        public KLine()
        {

        }
        public KLine(DateTime openTime, DateTime closeTime, double open, double hight, double low, double close)
        {
            OpenTime = openTime;
            CloseTime = closeTime;
            Open = open;
            Hight = hight;
            Low = low;
            Close = close;
        }

        public void Insert(Double point)
        {
            Close = point;
            Low=Math.Min(point,Low);
            Hight=Math.Max(point,Hight);
        }

        //OpenTime,Open,High,Low,Close,Volume,CloseTime,qav,numTrades,tbbav,tbqav,ignore
        public KLine(List<string> data)
        {
            if (data.Count < 7) return;
            OpenTime = ConvertToDate(data[0]);            
            try
            {
                this.Open = Convert.ToDouble(data[1].Replace(".", ","));
                Hight = Convert.ToDouble(data[2].Replace(".", ","));
                Low = Convert.ToDouble(data[3].Replace(".", ","));
                Close = Convert.ToDouble(data[4].Replace(".", ","));
                CloseTime = ConvertToDate(data[6]);               
            }
            catch (Exception ex)
            {

            }

        }

        private DateTime ConvertToDate(string date)
        {
            if(IsDoubleRealNumber(date)) return Stuff.BinanceTimeStampToUtcDateTime(Convert.ToDouble(date));
            return DateTime.Parse(date);
        }

        public static bool IsDoubleRealNumber(string valueToTest)
        {
            if (double.TryParse(valueToTest, out double d) && !Double.IsNaN(d) && !Double.IsInfinity(d))
            {
                return true;
            }

            return false;
        }


        public KLine(List<KLine> lines)
        {
            OpenTime = lines[0].OpenTime;
            CloseTime = lines.Last().CloseTime;
            Open = lines[0].Open;
            Hight = lines.Select(s=>s.Hight).ToList().Max();
            Low = lines.Select(s=>s.Low).ToList().Min();
            Close = lines.Last().Close;

        }



    }
}