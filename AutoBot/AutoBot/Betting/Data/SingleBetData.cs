using AutoBot.Enums;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    public class SingleBetData: BaseBetData
    {
        public SingleBetData() : base() { }

        public decimal PayIn { get; set; }

        public override void SetData(PlayerSettingsModel settings)
        {
            Session = settings.Session;
            ClientSeed = new Random().Next();
            Currency = settings.Currency;
            Drawdoun = settings.Drawdoun;
            PercentMax = SetPercent(settings.MaxPercent);
            PercentMin = SetPercent(settings.MinPercent);
            GessHigh = new Random().Next((int)SetPercent(settings.MinPercent), (int)SetPercent(settings.MaxPercent));
            GessLow = 0;
            Repit = settings.Repit;
            PayIn = SetBaseBet(settings);
            MaxBet = settings.BetCapacity;
            AccountToSendProfit = settings.AccountToSendProfit;
            ProfitEdge = settings.ProfitEdge;
        }        
    }
}
