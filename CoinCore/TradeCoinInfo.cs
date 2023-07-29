namespace CoinCore
{
    public class TradeCoinInfo
    {
        public string Name;
        public double Balance;
        public double StartBalanceUSDT;
        public double BalanceUSDT;
        public double LastPriceCoin=25000;
        private int WaitCount;
        private int WaitCountLemit=50;
        
       
       public TradeCoinInfo(string name,double balance,double balanceUSDT) 
       {
            Name = name;
            Balance = balance;
            StartBalanceUSDT = balanceUSDT;
            BalanceUSDT=balanceUSDT;
       }

        public bool HaveWait()
        {
            if(WaitCount>WaitCountLemit)
            {
                WaitCount=0;
                CanselLastOrder();
                return false;
            }
            WaitCount++;
            return true;
        }

        private void CanselLastOrder()
        {
            if(BalanceUSDT==0)
            {
                BalanceUSDT = Balance * LastPriceCoin/0.999;
                Balance = 0;
                return;
            }
            Balance=BalanceUSDT/(LastPriceCoin*0.999);
            BalanceUSDT = 0;

        }
        public double GetProfit()
        {
            return BalanceUSDT - StartBalanceUSDT;
        }
    }
}
