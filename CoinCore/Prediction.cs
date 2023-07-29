namespace CoinCore
{
    public struct Prediction
    {
        private Prediction(string value)
        {
            this.Value = value;
        }

        public static Prediction BUY { get => new Prediction("Buy"); }
        public static Prediction SELL { get => new Prediction("Sell"); }
        public static Prediction NOTHING { get => new Prediction("Nothing"); }

        public string Value { get; private set; }

        public static implicit operator string(Prediction enm) => enm.Value;

        public override string ToString() => this.Value.ToString();
    }
}
