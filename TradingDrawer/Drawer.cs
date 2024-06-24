using System.Drawing;
using CoinCore;

namespace BinanceTradingDrawer
{
    public class Drawer
    {
        public static int k=8;

        public static int d=10;

        public static void SignImage(Bitmap image,string sing)
        {
            Graphics g = Graphics.FromImage(image);
            SolidBrush curBrush = new SolidBrush(Color.Green);
            g.DrawString(sing,new Font("Arial", 11), curBrush,new PointF(10,10));

        }

        public static List<double> FindDifferent(List<double> list1, List<double> list2)
        {
            var res = new List<double>();
            if (list1 == null || list2 == null) return res;
            if (list1.Count != list2.Count) return res;
            for(int i = 0; i < list1.Count; i++) res.Add(list1[i] - list2[i]);
            return res;
            
        }

        public static void DrawAsHistogram(Bitmap image,List<double> points)
        {
            Graphics g = Graphics.FromImage(image);
            var height = image.Height;
            var width = image.Width;
            var dely = (points.Max() - points.Min());
            var stepX = width / points.Count;
            var stepY =  height / (2*dely);
            Pen greenPen = new Pen(Color.Green, 2);
            Pen redPen = new Pen(Color.Red, 2);
            for (int i = 0; i < points.Count; i++)
            {
                var curPen = new Pen(Color.Green, 2);
                var cheight = (int)(points[i]* stepY);
                var startY = (int)(height / 2 - cheight);
                if (cheight < 0)
                {
                    curPen = redPen;
                    startY = (int)height / 2;

                }                                  
                Rectangle rect = new Rectangle(i * stepX, startY, stepX, (int)Math.Abs(cheight));
                g.DrawRectangle(curPen, rect);
                SolidBrush curBrush = new SolidBrush(curPen.Color);
                g.FillRectangle(curBrush, rect);
            }          

        }

        public static Bitmap DrawGrapth(Bitmap image,List<double> points,Color color,double max,double min)
        {
            Graphics g = Graphics.FromImage(image);            
            var height=image.Height;
            var width=image.Width;           
            var dely =(max - min);
            var stepX=width/points.Count;
            var stepY=k*height/(d*dely);
            var listPoints=new List<Point>();
            for(int i=0;i<points.Count;i++)
            {
                listPoints.Add(new Point(i * stepX, (int)((points[i] - min)*stepY)));
            }
            for(int i=1;i<points.Count;i++)
            {
                Pen blackPen = new Pen(color, 2);
                g.DrawLine(blackPen, listPoints[i-1].X, height-listPoints[i-1].Y,listPoints[i].X,height-listPoints[i].Y);
            }
            return image;
        }

     

           

        double koef = 0.9;

        public static int CountStepWidth(int width, int numSteps)
        {
            return width/numSteps;
        }

        public static Bitmap DrawKLines(Bitmap image,List<KLine> klines)
        {
            Graphics g = Graphics.FromImage(image);
            var height = image.Height;
            var width = image.Width;
            var listClose=klines.Select(s => s.Close).ToList();
            var listOpen=klines.Select(s => s.Open).ToList();
            var listHight=klines.Select(s => s.Hight).ToList();
            var listLow=klines.Select(s => s.Low).ToList();
            var max = listHight.Max();
            var min = listLow.Min();
            var dely = (max - min);
            var stepX = CountStepWidth(width,klines.Count);// width / klines.Count;
            //var stepY =dely>1?(k*height/ (d*dely)):k*height*dely/d;
            var stepY = (k * height / (d * dely));
            Pen redPen = new Pen(Color.Red, 2);
            Pen greenPen = new Pen(Color.Green, 2);
            for(int i=0;i<klines.Count;i++)
            {
                var curPen = new Pen(Color.Green, 2);
                var cheight = (int)(Math.Abs(listClose[i] - listOpen[i]) * stepY);
                var startRY = height - (int)((listClose[i] - min) * stepY);
                Rectangle rect = new Rectangle(i * stepX, startRY, stepX-2,cheight);
                if (listClose[i] < listOpen[i])
                {
                    curPen = redPen;
                    startRY = height - (int)((listOpen[i] - min) * stepY);
                    rect = new Rectangle(i * stepX,startRY, stepX-2, cheight);
                }
                var maxHeight = (int)(Math.Abs(listHight[i] - listLow[i]) * stepY);
                g.DrawLine(curPen, i *stepX+stepX/2, height - (int)((listLow[i] - min) * stepY), i *stepX+stepX/2, height - (int)((listHight[i] - min) * stepY));
                g.DrawRectangle(curPen, rect);
                SolidBrush curBrush = new SolidBrush(curPen.Color);                
                g.FillRectangle(curBrush, rect);
            }
            return image;
        }

    }
}