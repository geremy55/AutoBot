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

        public void SetData(PlayerSettingsModel settings)
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

        public decimal SetBaseBet(PlayerSettingsModel settings)
        {
            int full = settings.Drawdoun / settings.Repit;
            int left = settings.Drawdoun % settings.Repit;

            int bigSum = 0;

            for (int j = 0; j <= full; j++)
            {
                bigSum += (int)Math.Pow(settings.BetUps, j);
            }
            bigSum *= settings.Repit;

            if (left > 0)
            {
                bigSum += left * (int)Math.Pow(settings.BetUps, ++full);
            }
            return Math.Round(Math.Max(settings.MoneyManager == MoneyManagerEnum.IndependentBets ?
                settings.UserBalance / bigSum : settings.WorkBalance / bigSum, 0.00000001m), 8);
        }

        private long SetPercent(double percent)
        {
            //нормализация до 3-х знаков после запятой
            double norm = Math.Round(percent, 3);
            //перевод в лонг
            long normPerc = (long)(norm * 10000) - 1;
            return normPerc;
        }
    }
}
