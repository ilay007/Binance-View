namespace CoinCore
{
    public class Boll
    {
        public Ema Ema;
        public List<double> CurveHight;
        public List<double> CurveLow;
        private List<double> CrudeData;
        private List<double> sum;
        private int N;
        private int a = 2;

        public Boll()
        {

        }

        public Boll(List<double> crudeData, int n)
        {
            N = n;
            var boll = Counter.CountBoll(crudeData, n);
            sum = boll[3];
            CurveHight = boll[0];
            Ema = new Ema(crudeData, n);
            CurveLow = boll[2];
            CrudeData = crudeData;
        }


        public Boll GetRange(int start, int len)
        {
            var nBoll = new Boll();
            nBoll.Ema = this.Ema.GetRange(start, len);
            nBoll.CurveHight = this.CurveHight.GetRange(start, len);
            nBoll.CurveLow = this.CurveLow.GetRange(start, len);
            nBoll.CrudeData = this.CrudeData.GetRange(start, len);
            nBoll.sum = this.sum.GetRange(start, len);
            nBoll.N = N;
            return nBoll;
        }

        public void AddCrudePoint(double point)
        {
            Ema.AddCrudePoint(point);
            CrudeData.Add(point);
            var k = CrudeData.Count;
            var v2 = sum.Last() - Math.Pow(CrudeData[k - N] - Ema.EmaPoints[k - N], 2);
            v2 += Math.Pow(CrudeData.Last() - Ema.EmaPoints.Last(), 2);
            sum.Add(v2);
            var q = Math.Sqrt(sum.Last() / N);
            CurveHight.Add(Ema.EmaPoints.Last() + a * q);
            CurveLow.Add(Ema.EmaPoints.Last() - a * q);

        }

        public void DeliteLastPoint()
        {
            Ema.DellLastPoint();
            if (CurveHight.Count == 0) return;
            CurveHight.RemoveAt(CurveHight.Count - 1);
            CurveLow.RemoveAt(CurveLow.Count - 1);
            CrudeData.RemoveAt(CrudeData.Count - 1);
            sum.RemoveAt(sum.Count - 1);
        }
    }
}
