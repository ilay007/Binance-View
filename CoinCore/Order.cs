namespace CoinCore
{
    public class Order
    {
        public Order(string symbol,decimal price,decimal executedQty,string side)
        {
            Symbol=symbol;
            Price=price;
            ExecutedQty=executedQty;
            Side=side;
        }
        public string Symbol;
        public long OrderId;
        public decimal Price;
        public decimal OrigQty;
        public decimal ExecutedQty;
        public string ClientOrderId;
        public string Type;
        public bool IsWorking;
        public string Side;
        public string TimeInForce;
        public long WorkingTime;
    }
}
