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

        public string GetLastKnowledges()
        {
            return null;
        }

        public Prediction MakePrediction()
        {
            return Prediction.NOTHING;
        }

        public void DelliteLastPoint()
        {
        }
        public void AddKnowledgeSince(CurveBundle bundle,int num, bool isBuy)
        {

        }

        public void LoadKnowledge()
        {

        }



    }
}
