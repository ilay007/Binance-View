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
            strategists.Add("1h", new StatisticStrategist());
            strategists.Add("4h", new StatisticStrategist());
            LoadKnowledges();
            ReDraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadKnowledges();
            ReDraw();
        }

        private void LoadKnowledges()
        {
            var PathToKnowledge = "D:\\mGitHub\\MyGit\\LTCUSDT_1m.txt";
            var PathToKnowledge2 = "D:\\mGitHub\\MyGit\\LTCUSDT_15m.txt";
            var PathToKnowledge3 = "D:\\mGitHub\\MyGit\\LTCUSDT_1h.txt";
            var PathToKnowledge4 = "D:\\mGitHub\\MyGit\\LTCUSDT_4h.txt";
            strategists["1m"].LoadKnowledge(PathToKnowledge);
            strategists["15m"].LoadKnowledge(PathToKnowledge2);
            strategists["1h"].LoadKnowledge(PathToKnowledge3);
            strategists["4h"].LoadKnowledge(PathToKnowledge4);
            ReDraw();

        }


        private void ReDraw()
        {
            ReDrawPicture(strategists["1m"], pictureBox5, pictureBox8);
            ReDrawPicture(strategists["15m"], pictureBox1, pictureBox2);
            ReDrawPicture(strategists["1h"], pictureBox3, pictureBox6);
            ReDrawPicture(strategists["4h"], pictureBox4, pictureBox7);

        }


        private void ReDrawPicture(StatisticStrategist strategist, PictureBox pictureBox, PictureBox pictureBox2)
        {

            var data = !checkBox1.Checked ? strategist.Knowledge.BuyKnowledges[this.count] : strategist.Knowledge.SellKnowledges[this.count];
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
            var curRange12 = data.FastEma.EmaPoints.GetRange(0, numPoints);
            var curRange26 = data.SlowEma.EmaPoints.GetRange(0, numPoints);
            if (pictureBox2 == null) return;
            var secondImage = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Drawer.DrawAsHistogram(secondImage, data.DifEma.GetRange(0, numPoints));
            Drawer.DrawGrapth(secondImage, curRange12, Color.Violet, curRange12.Max(), curRange12.Min());
            pictureBox2.Image = secondImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            count++;
            if (count >= strategists["1m"].Knowledge.BuyKnowledges.Count() - 1) count = 0;
            ReDraw();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            count--;
            if (count < 0) count = 0;
            ReDraw();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }
    }
}
