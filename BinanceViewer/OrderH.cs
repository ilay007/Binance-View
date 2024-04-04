using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceAcountViewer
{
    
    public class OrderH
    {
        public Guid Id { get; set; }
        public Guid IdSession { get; set; }
        public Session Subject { get; set; }
        public int Num { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public String Type { get; set; }
    }
}
