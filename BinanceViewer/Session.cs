using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceAcountViewer
{
    public class Session
    {
        public Guid Id { get; set; }
        public ulong Date { get; set; }
        public string Pair { get; set; }

    }
}
