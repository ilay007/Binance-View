namespace CoinCore
{
    public interface IStrategist
    {
        public string GetKnowledge()
        {

            return null;
        }
        public void AddData(CurveBundle curveBundle,KLine point)
        {

        }

        public string GetLastKnowledges()
        {
            return null;
        }

        public Prediction MakePrediction()
        {
            return Prediction.NOTHING;
        }

        public void AddKnowledgeSince(CurveBundle bundle,int num, bool isBuy)
        {

        }

        public void LoadKnowledge()
        {

        }

        public void DelliteLastPoint()
        {
        }
    }
}
