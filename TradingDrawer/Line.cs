namespace BinanceTradingDrawer
{
    public class Line
    {
        public Line(int x1, int y1, int x2, int y2)
        {
            P1 = new Point(x1, y1);
            P2 = new Point(x2, y2);
        }
        public Point P1 { get; set; }
        public Point P2 { get; set; }
    }
}
