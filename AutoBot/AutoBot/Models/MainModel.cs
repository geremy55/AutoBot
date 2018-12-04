using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Models
{
    public class MainModel
    {
        public decimal Balance { get; set; } = 0;
        public long BetCount { get; set; } = 0;
        public decimal BetProfits { get; set; } = 0;
        public long  BetWins { get; set; } = 0;
        public decimal BetWinsPercent { get; set; } = 0;
        public string AccountName { get; set; } = "name";       

        public MainModel()
        {
        }

        public void InitModel(SessionInfo session, Currencies currency)
        {
            if (session == null) return;
            Balance = session[currency].Balance;
            BetCount = session[currency].BetCount;
            BetProfits = session[currency].BetPayIn + session[currency].BetPayOut;
            BetWins = session[currency].BetWinCount;
            BetWinsPercent = session[currency].BetCount == 0
                ? 0
                : session[currency].BetWinCount * 100M / session[currency].BetCount;
            AccountName = session.Username;
        }

    }
}
