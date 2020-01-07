using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    public class BetResultData
    {
        public int BetCount { get; set; } = 0;
        public decimal Profit { get; set; } = 0;
        public double Percent { get; set; } = 0;        
        public decimal Balance { get; set; } = 0;
        public SessionInfo Session { get; set; } 
    }
}
