using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class AbonentDetails
    {
        public DateTime DateTime { get; set; }
        public int AbonentId { get; set; }
        public string? Reporter { get; set; }
        public string? Service { get; set; }
        public string? Duration { get; set; }
        public decimal Cost { get; set; }
        public string? Rouming { get; set; }
    }
}
