namespace CoinCore
{  
    public class StoryItem
    {
        public long Id { get; set; }
        public double Amount { get; set; }

        public string Coin { get; set; }

        public string Network { get; set; }

        public int Status { get; set; }

        public string Address { get; set; }
        public string AddressTag { get; set; }
        public string TxId { get; set; }
        public string InsertTime { get; set; }
        public string TranseferType { get; set; }
        public string ConfirmTimes { get; set; }
        public int WalletType { get; set; }

        
        public StoryItem(Object data)
        {
          
        }




    }
}
