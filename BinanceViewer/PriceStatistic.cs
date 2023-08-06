using CoinCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceAcountViewer
{
    public partial class PriceStatistic : Form
    {
        public PriceStatistic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lines=File.ReadAllLines("D:\\GitHub\\binance\\binance-downloader\\binance_downloader\\downloaded\\2023-08-04_012617_FIROUSDT_1m_klines.csv");
            //var files =Directory.GetFiles("D:\\GitHub\\binance\\binance-downloader\\binance_downloader\\downloaded\\LTC");
            var sums = new List<double>();
            sums.Add(0);
            var price = 0.1;
            var dic = new Dictionary<int, double[]>();
            double totalSum = 0;
            double allcoins = 12665440;
            for(int i=5;i<40;i++)
            {
                if(!dic.ContainsKey(i)) dic.Add(i, new double[1]);
            }
            //foreach (var file in files) 
            //{
                //var lines = File.ReadAllLines(file);
                var kLines = new List<KLine>();                
                int count = 0;
                                     
                foreach (var line in lines)
                {
                    count++;
                    if (count == 1) continue;
                    var kline = new KLine(line.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                    kLines.Add(kline);
                    if (count % 1440 == 0) sums.Add(0);
                    sums[sums.Count - 1] += kline.Volume;
                    var key = (int)(kline.Close / price);
                    // (!dic.ContainsKey(key)) dic.Add(key, new double[1]);
                    dic[key][0] += kline.Volume;
                    totalSum += kline.Volume;

                }
           // }
            double sum = 0;
            foreach(var data in dic)
            {
                var percent= Math.Round(100 * data.Value[0] / totalSum,2);
                var supPercent = Math.Round(100 * data.Value[0] / allcoins,2);
                sum += percent;
                Console.WriteLine("price {0}  procent= {1}  {2}  {3}", Math.Round(data.Key * price,2),percent, sum,supPercent);
            }


        }
    }
}
