namespace CoinCore
{
    public class CoinInfo
    {
        public string Coin;
        public bool DepositAllEnable;
        public bool WithdrawAllEnable;
        public string Name;
        public string Description;
        public double Free;
        public double Locked;
        public double Freeze;
        public double Withdrawing;
        public string Inpoing;
        public string Inpoable;
        public double Storage;
        public bool IsLegalMoney;
        public bool Trading;
        public double Price;
        public double Balance;
        public void SetPrice(double price)
        {
            this.Price = price;
            this.Balance = Free * price;
        }
    }
}
