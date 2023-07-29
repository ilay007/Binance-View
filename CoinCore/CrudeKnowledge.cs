using System.Text;

namespace CoinCore
{
    public class CrudeKnowledge
    {
        public double Ema;
        public double CurveHight;
        public double CurveLow;
        public double DeltEma;
        public KLine Line;

        public CrudeKnowledge()
        {

        }


        public CrudeKnowledge(Boll boll,KLine line,double delEma,int num) 
        {
            Ema=boll.Ema.EmaPoints[num];
            CurveHight = boll.CurveHight[num];
            CurveLow = boll.CurveLow[num];
            Line = line;
            DeltEma=delEma;
        }


        public string ToStringEma()
        {
            StringBuilder sb=new StringBuilder();
            if (DeltEma > 0) sb.Append(2);
            else if(DeltEma==0) sb.Append(1);
            else
            {
                sb.Append(2);
            }
            return sb.ToString();

        }


    }
}
