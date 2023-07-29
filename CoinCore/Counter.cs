namespace CoinCore
{
    public class Counter
    {
        public static List<double> CountEMA(List<double> data, int N)
        {
            var res = new List<double>();
            var a = 2d / (1 + N);
            var pre = data[0];
            for (int i = 0; i < data.Count; i++)
            {
                var ema = data[i] * a + (1 - a) * pre;
                pre = ema;
                res.Add(ema);
            }
            return res;
        }

        public static List<List<double>> CountBoll(List<double> data, int N)
        {
            var ema = CountEMA(data, N);
            var hight = new List<double>();
            var low = new List<double>();
            double sum = 0;
            var listSum = new List<double>();
            for (int i = 0; i < N; i++)
            {
                var q = Math.Pow(data[i] - ema[i], 2);
                sum += q;
                listSum.Add(sum);
                hight.Add(ema[i]);
                low.Add(ema[i]);
            }
            var a = 2;
            for (int i = N; i < data.Count; i++)
            {
                var v2 = sum - Math.Pow(data[i - N] - ema[i - N], 2);
                v2 += Math.Pow(data[i] - ema[i], 2);
                sum = v2;
                listSum.Add(sum);
                var q = Math.Sqrt(sum / N);
                hight.Add(ema[i] + a * q);
                low.Add(ema[i] - a * q);
            }
            var result = new List<List<double>>();
            result.Add(hight);
            result.Add(ema);
            result.Add(low);
            result.Add(listSum);
            return result;
        }
    }
}
