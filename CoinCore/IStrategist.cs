namespace CoinCore
{
    public interface IStrategist
    {
        public string GetKnowledge();

        public void AddData(CurveBundle curveBundle, KLine point);


        public void SetTimeLemit(int num);

        public Prediction MakePrediction(double point);

        public double GetDifInPercent(int stepsAgo);

        public void AddKnowledgeSince(CurveBundle bundle, int num, Recommendations isBuy);

        public void LoadKnowledge(string path);

        public string GetLastKnowledges();

        public void DeleteLastPoint();      
        
        
    }
}
