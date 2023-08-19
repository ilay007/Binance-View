namespace CoinCore
{
    public class EmptyStrategist:IStrategist
    {
        public string GetKnowledge()
        {

            return null;
        }

        public void AddData(CurveBundle curveBundle, KLine point)
        {

        }

        public void SetTimeLemit(int num)
        {

        }
        public string GetLastKnowledges()
        {
            return null;
        }

        public Prediction MakePrediction(double point)
        {
            return Prediction.NOTHING;
        }

        public void DeleteLastPoint()
        {
        }
        public void AddKnowledgeSince(CurveBundle bundle,int num, bool isBuy)
        {

        }

        public void LoadKnowledge(string path)
        {

        }
    }
}
