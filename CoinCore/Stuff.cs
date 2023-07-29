namespace CoinCore
{
    public class Stuff
    {
        public static DateTime BinanceTimeStampToUtcDateTime(double binanceTimeStamp)
        {
            // Binance timestamp is milliseconds past epoch
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return epoch.AddMilliseconds(binanceTimeStamp);
        }
    }
}
