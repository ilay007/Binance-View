using System.Drawing;

namespace CoinCore
{
    public class Ema
    {
        public List<double> CrudePoints;
        public List<double> EmaPoints;
        private int N;

        public int Getn() { return N; }

        public Ema GetRange(int start, int end)
        {
            var nEma = new Ema();
            nEma.CrudePoints=CrudePoints.GetRange(start, end);
            nEma.EmaPoints=EmaPoints.GetRange(start, end);
            nEma.N = N;
            return nEma;
        }

        public Ema()
        {

        }

        public Ema(List<double> ema,int n) 
        { 
            CrudePoints=new List<double>();
            CrudePoints.AddRange(ema);
            N = n;
            EmaPoints = Counter.CountEMA(ema, n);           
        }

        public void DellLastPoint()
        {
            if(CrudePoints.Count == 0) return;
            if(EmaPoints.Count == 0) return;
            CrudePoints.RemoveAt(CrudePoints.Count - 1);
            EmaPoints.RemoveAt(EmaPoints.Count - 1);
        }

        public void ChangeLastPoint(double point)
        {
            CrudePoints[CrudePoints.Count-1]=point;
            var a = 2d / (1 + N);
            var pre = EmaPoints[CrudePoints.Count-2];
            EmaPoints.Add(CrudePoints.Last() * a + (1 - a) * pre);
        }

        public void AddCrudePoint(double point)
        {
            CrudePoints.Add(point);
            var a = 2d / (1 + N);
            var pre = EmaPoints.Last();
            EmaPoints.Add(CrudePoints.Last() * a + (1 - a) * pre);            
        }
    }
}
