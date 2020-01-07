using AutoBot.Enums;
using AutoBot.Models;
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

        public virtual void SetData(PlayerSettingsModel settings) { }
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

        protected long SetPercent(double percent)
        {
            //нормализация до 3-х знаков после запятой
            double norm = Math.Round(percent, 3);
            //перевод в лонг
            long normPerc = (long)(norm * 10000) - 1;
            return normPerc;
        }

    }


}
