using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    abstract public class BaseBetData
    {
        public SessionInfo Session { get; set; }
        public long GessLow { get; set; } = 0;
        public long GessHigh { get; set; }
        public int ClientSeed { get; set; } = new Random().Next();
        public Currencies Currency { get; set; }
        public int Repit { get; set; }
        public long PercentMin { get; set; }
        public long PercentMax { get; set; }
        public int Drawdoun { get; set; }
        public int MaxBet { get; set; }
        public string AccountToSendProfit { get; set; }
        public decimal ProfitEdge { get; set; }
    }
}
