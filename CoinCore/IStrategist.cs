namespace CoinCore
{
    public interface IStrategist
    {
        public string GetKnowledge();

        public void AddData(CurveBundle curveBundle, KLine point);


        public void SetTimeLemit(int num);

        public Prediction MakePrediction();


        public void AddKnowledgeSince(CurveBundle bundle, int num, bool isBuy);

        public void LoadKnowledge(string path);
        public string GetLastKnowledges();

        public void DeleteLastPoint();      
        
        
    }
}
