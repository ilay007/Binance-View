using AcountViewer;
using Binance.Spot.Models;
using Castle.Core;
using Strateges;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CoinCore;
using Strateges;
using BinanceTradingDrawer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BinanceAcountViewer
{
    public partial class KnowledgeViewer : Form
    {
        int count = 0;
        Dictionary<string, StatisticStrategist> strategists;


        public KnowledgeViewer()
        {
            InitializeComponent();
            strategists = new Dictionary<string, StatisticStrategist>();
            strategists.Add("1m", new StatisticStrategist());
            strategists.Add("15m", new StatisticStrategist());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var PathToKnowledge = "D:\\mGitHub\\MyGit\\LTCUSDT_1m.txt";
            var PathToKnowledge2 = "D:\\mGitHub\\MyGit\\LTCUSDT_15m.txt";
            strategists["1m"].LoadKnowledge(PathToKnowledge);
            strategists["15m"].LoadKnowledge(PathToKnowledge2);
            ReDrawPicture(strategists["1m"], pictureBox1);
            ReDrawPicture(strategists["15m"], pictureBox3);


        }


        private void ReDrawPicture(StatisticStrategist strategist, PictureBox pictureBox)
        {
            var data = strategist.Knowledge.BuyKnowledges[this.count];
            while (data == null)
            {
                this.count++;
                data = strategist.Knowledge.BuyKnowledges[this.count];
            }
            var curImage = new Bitmap(pictureBox.Width, pictureBox1.Height);
            var image = Drawer.DrawKLines(curImage, data.KLines);
            var numPoints = data.KLines.Count;
            var max = data.KLines.Select(s => s.Hight).ToList().GetRange(0, numPoints).Max();
            var min = data.KLines.Select(s => s.Low).ToList().GetRange(0, numPoints).Min();
            Drawer.DrawGrapth(curImage, data.Boll.CurveHight.GetRange(0, numPoints), Color.Violet, max, min);
            Drawer.DrawGrapth(curImage, data.Boll.Ema.EmaPoints.GetRange(0, numPoints), Color.Brown, max, min);
            Drawer.DrawGrapth(curImage, data.Boll.CurveLow.GetRange(0, numPoints), Color.Orange, max, min);
            pictureBox.Image = image;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            count++;
            if (count > strategists["1m"].Knowledge.BuyKnowledges.Count() - 1) count = 0;
            ReDrawPicture(strategists["1m"], pictureBox1);
            ReDrawPicture(strategists["15m"], pictureBox3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count--;
            if (count < 0) count = 0;
            ReDrawPicture(strategists["1m"], pictureBox1);
            ReDrawPicture(strategists["15m"], pictureBox3);

        }


    }
}
