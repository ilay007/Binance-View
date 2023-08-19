using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Binance.Spot.Models;
using CoinCore;

namespace AcountViewer
{
    public class CoinsStore
    {
        public Dictionary<string, Dictionary<string, CurveBundle>> LinesHistory = new Dictionary<string, Dictionary<string, CurveBundle>>();
        public Dictionary<string, Dictionary<string, IStrategist>> Strategists = new Dictionary<string, Dictionary<string, IStrategist>>();

        public int BollN { get; set; }
        public int FastN { get; set; }
        public int SlowN { get; set; }

        public int Boll { get; set; }

        private List<Interval> Intervals = new List<Interval>();

        public CoinsStore(List<Interval> intervals,int bollN, int fastN, int slowN)
        {
            BollN = bollN;
            FastN = fastN;
            SlowN = slowN;
            Intervals.AddRange(intervals);            
            foreach(var interval in Intervals)
            {
                Strategists.Add(interval, new Dictionary<string, IStrategist>());
            }

        }

        private void AddTimeInterval(string interval)
        {
            if (!this.LinesHistory.ContainsKey(interval))
            {
                this.LinesHistory.Add(interval, new Dictionary<string, CurveBundle>());
            }
        }


        public void DelleteLastPoint(string pair)
        {
            foreach(var interval in Intervals)
            {
                LinesHistory[interval][pair].DelliteLastPoint();
                Strategists[interval][pair].DeleteLastPoint();
            }
        }

        public void AddStrategist(string interval, string pair, IStrategist strategist)
        {
            if (!Strategists[interval].ContainsKey(pair)) this.Strategists[interval].Add(pair, strategist);
        }


        public Prediction MakePrediction(string pair)
        {
            foreach(var interval in Intervals) 
            {
                if (!Strategists.ContainsKey(interval)) return Prediction.NOTHING;
                if (!Strategists[interval].ContainsKey(pair)) return Prediction.NOTHING;            
            }
            var pred15 = Strategists["15m"][pair].MakePrediction();
            var predOne = Strategists["1h"][pair].MakePrediction();
             if(pred15 == Prediction.BUY&&predOne==Prediction.BUY)return Prediction.BUY;
            if(pred15 == Prediction.SELL&&predOne==Prediction.SELL)return Prediction.SELL;
            //if(pred15 == Prediction.BUY)return Prediction.BUY;
           // if(pred15 == Prediction.SELL)return Prediction.SELL;
            return Prediction.NOTHING;
        }



        public void RemoveLastPoint(string interval, string currentPair)
        {
            this.LinesHistory[interval][currentPair].DelliteLastPoint();
        }
               
        

        public void AddPoint(string interval, string currentPair, KLine kline)
        {
            if (!this.LinesHistory.ContainsKey(interval)) return;
            if (!this.LinesHistory[interval].ContainsKey(currentPair)) return;
            LinesHistory[interval][currentPair].AddPoint(kline);
            var lastData = LinesHistory[interval][currentPair].GetLastData();
            Strategists[interval][currentPair].AddData(lastData, kline);
        }


        public bool TryAddData(List<KLine> data, string invKey, string currentPair)
        {
            if (!this.LinesHistory.ContainsKey(invKey)) AddTimeInterval(invKey);
            if (!this.LinesHistory[invKey].ContainsKey(currentPair))
            {
                this.LinesHistory[invKey].Add(currentPair, new CurveBundle(data, BollN, FastN, SlowN, currentPair, invKey));
            }
            else
            {
                var bundle = this.LinesHistory[invKey][currentPair];
                var lastLine = bundle.KLines.Last();
                int k = data.Count - 1;
                for (k = data.Count - 1; k >= 0; k--) if (data[k].OpenTime == lastLine.OpenTime) break;
                if (k < data.Count - 1)
                {
                    this.LinesHistory[invKey][currentPair].AddPoint(data[k + 1]);

                }
            }
            return true;
        }

        public void AddKnowledges(string pair, int start, bool isBuy)
        {
            var crude = LinesHistory[Interval.FIFTEEN_MINUTE][pair].GetLastKnowledgesSinceNum(start);
            Strategists[Interval.FIFTEEN_MINUTE][pair].AddKnowledgeSince(crude, start, true);

        }

        public void SaveKnowledges(string path)
        {
            foreach (var interval in Strategists)
            {
                foreach (var strategist in interval.Value)
                {
                    var serialized = strategist.Value.GetKnowledge();
                    var pathToFile = interval.Key + path + "";
                    using (StreamWriter writer = new StreamWriter(pathToFile))
                    {
                        writer.WriteLine(serialized);
                        writer.Close();
                    }
                }
            }
        }

        public void TeachStrategists(string pathToKnowledge)
        {
            //TODO
            try
            {
                foreach (var intervalStrat in Strategists)
                {
                    foreach (var strategist in intervalStrat.Value)
                    {
                        strategist.Value.LoadKnowledge(pathToKnowledge);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }       
    
}

