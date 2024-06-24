using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoinCore
{
    public class CurveBundle
    {
        public Boll Boll;
        public Ema FastEma;
        public Ema SlowEma;
        public List<KLine> KLines;
        public List<double> DifEma;
        public List<int> GainVolumeProc;
        public List<int> GainVolume;
        public string Pair;
        public string Interval;
        public static int NumLastCodes = 4;
        public CurveBundle(List<KLine> kLines, int bollN, int fastN, int slowN, string pair, string interval)
        {
            KLines = kLines;
            Pair = pair;
            Interval = interval;
            var lines = kLines.Select(s => s.Close).ToList();
            Boll = new Boll(lines, bollN);
            FastEma = new Ema(lines, fastN);
            SlowEma = new Ema(lines, slowN);
            DifEma = new List<double>();
            GainVolume=new List<int>();
            GainVolumeProc=new List<int>();
            for (int i=0;i<FastEma.EmaPoints.Count;i++)
            {
                DifEma.Add(FastEma.EmaPoints[i] - SlowEma.EmaPoints[i]);
            }
            GainVolume.Add(0);
            GainVolumeProc.Add(0);
            for (int k=0;k<kLines.Count-1;k++)
            {
                var lastKline = kLines;
                var lastPrice = lastKline[k+1].Close;
                double lesValueCount = 1;
                double moreValueCount = 1;
                for (int i = k; i > 0; i--)
                {
                    if (kLines[i].Low > lastPrice)
                    {
                        moreValueCount += kLines[i].Volume;
                        continue;
                    }
                    lesValueCount += kLines[i].Volume;
                }
                int proc = 100 * (int)(moreValueCount - lesValueCount) / (int)lesValueCount;
                GainVolumeProc.Add(proc);
                int delCount = (int)Math.Round(moreValueCount - lesValueCount, 2);
                GainVolume.Add(delCount);
            }            
        }

        public CurveBundle(Boll boll, Ema fastEma, Ema slowEma, List<KLine> kLines, List<double> difEma, List<int> gainVolumeProc, List<int> gainVolume)
        {
            Boll Boll = boll;
            Ema FastEma = fastEma;
            Ema SlowEma = slowEma;
            List<KLine> KLines = kLines;
            List<double> DifEma = difEma;
            List<int> GainVolumeProc = gainVolumeProc;
            List<int> GainVolume = gainVolume;
        }

    

        public CurveBundle()
        {

        }


        public CurveBundle GetRange(int start, int len)
        {
            var nBundle = new CurveBundle();
            if(start+len>KLines.Count)
            {
                len=KLines.Count-start-1;
            }
            nBundle.KLines = KLines.GetRange(start, len);
            nBundle.KLines = KLines.GetRange(start, len);
            nBundle.Boll = Boll.GetRange(start, len);
            nBundle.FastEma = FastEma.GetRange(start, len);
            nBundle.SlowEma = SlowEma.GetRange(start, len);
            nBundle.DifEma = DifEma.GetRange(start, len);
            nBundle.GainVolumeProc= GainVolumeProc.GetRange(start,len);
            nBundle.GainVolume = GainVolume.GetRange(start, len);
            return nBundle;
        }

        public CurveBundle GetLastKnowledgesSinceNum(int num)
        {
            return GetRange(num - NumLastCodes, NumLastCodes);
        }
        public CurveBundle GetLastData(int num)
        {
            return GetRange(KLines.Count - num-1, num);
        }
        public void DelliteLastPoint()
        {
            KLines.RemoveAt(KLines.Count - 1);
            GainVolume.RemoveAt(KLines.Count - 1);
            GainVolumeProc.RemoveAt(KLines.Count - 1);
            Boll.DeliteLastPoint();
            FastEma.DellLastPoint();
            SlowEma.DellLastPoint();
            DifEma.RemoveAt(DifEma.Count - 1);
        }

        public void ChangeLastPoint(double point)
        {
            KLines[KLines.Count - 1].Insert(point);
            /*Boll.ChangeLastPoint(point);
            FastEma.ChangeLastPoint(point);
            SlowEma.ChangeLastPoint(point);
            DifEma[DifEma.Count - 1] = FastEma.EmaPoints.Last() - SlowEma.EmaPoints.Last();*/
        }
        public void AddPoint(KLine point)
        {
            KLines.Add(point);
            Boll.AddCrudePoint(point.Close);
            FastEma.AddCrudePoint(point.Close);
            SlowEma.AddCrudePoint(point.Close);
            DifEma.Add(FastEma.EmaPoints.Last()-SlowEma.EmaPoints.Last());
            var lastKline = KLines[KLines.Count - 1];
            var lastPrice = lastKline.Close;
            double lesValueCount = 1;
            double moreValueCount = 1;
            for (int i = KLines.Count - 1; i > 0; i--)
            {
                if (KLines[i].Low > lastPrice)
                {
                    moreValueCount += KLines[i].Volume;
                    continue;
                }
                lesValueCount += KLines[i].Volume;
            }
            int proc = 100 * (int)(moreValueCount - lesValueCount) / (int)lesValueCount;
            int delCount = (int)Math.Round(moreValueCount - lesValueCount, 2);
            GainVolumeProc.Add(proc);
            GainVolume.Add(delCount);
        }
    }
}
