namespace CoinCore
{
    public class CurveBundle
    {
        public Boll Boll;
        public Ema FastEma;
        public Ema SlowEma;
        public List<KLine> KLines;
        public List<double> DifEma;

        public String Pair;
        public String Interval;
        public static int NumLastCodes = 4;
        public CurveBundle(List<KLine> kLines, int bollN, int fastN, int slowN, String pair, String interval)
        {
            KLines = kLines;
            Pair = pair;
            Interval = interval;
            var lines = kLines.Select(s => s.Close).ToList();
            Boll = new Boll(lines, bollN);
            FastEma = new Ema(lines, fastN);
            SlowEma = new Ema(lines, slowN);
            DifEma = new List<double>();
            for(int i=0;i<FastEma.EmaPoints.Count;i++)
            {
                DifEma.Add(FastEma.EmaPoints[i] - SlowEma.EmaPoints[i]);
            }
        }

        public CurveBundle()
        {

        }


        public CurveBundle GetRange(int start, int len)
        {
            var nBundle = new CurveBundle();
            nBundle.KLines = KLines.GetRange(start, len);
            nBundle.KLines = KLines.GetRange(start, len);
            nBundle.Boll = Boll.GetRange(start, len);
            nBundle.FastEma = FastEma.GetRange(start, len);
            nBundle.SlowEma = SlowEma.GetRange(start, len);
            nBundle.DifEma = DifEma.GetRange(start, len);
            return nBundle;
        }

        public CurveBundle GetLastKnowledgesSinceNum(int num)
        {
            return GetRange(num - NumLastCodes, NumLastCodes);
        }


        public CurveBundle GetLastData()
        {
            return GetRange(KLines.Count - 2, 1);
        }




        public void DelliteLastPoint()
        {
            KLines.RemoveAt(KLines.Count - 1);
            Boll.DeliteLastPoint();
            FastEma.DellLastPoint();
            SlowEma.DellLastPoint();
            DifEma.RemoveAt(DifEma.Count - 1);
        }


        public void AddPoint(KLine point)
        {
            KLines.Add(point);
            Boll.AddCrudePoint(point.Close);
            FastEma.AddCrudePoint(point.Close);
            SlowEma.AddCrudePoint(point.Close);
            DifEma.Add(FastEma.EmaPoints.Last()-SlowEma.EmaPoints.Last());
        }
    }
}
