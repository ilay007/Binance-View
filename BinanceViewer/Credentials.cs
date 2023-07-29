using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceAcountViewer
{
    public class Credentials
    {
        static List<KeyValuePair<string, string>> keys;
        private Credentials()
        {
        }

        public static List<KeyValuePair<string, string>> GetCredentials()
        {
            return keys;
        }


        public static void InitCredentials(string path)
        {
            keys = new List<KeyValuePair<string, string>>();
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] arr = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length < 2) continue;
                keys.Add(new KeyValuePair<string, string>(arr[0], arr[1]));
            }
        }
    }
}
