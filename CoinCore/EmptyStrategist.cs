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


        public double GetDifInPercent(int stepsAgo)
        {
            return 0.1;
        }



            public Prediction MakePrediction(double point)
        {
            return Prediction.NOTHING;
        }

        public void DeleteLastPoint()
        {
        }
        public void AddKnowledgeSince(CurveBundle bundle,int num, Recommendations isBuy)
        {

        }

        public void LoadKnowledge(string path)
        {

        }
    }
}
