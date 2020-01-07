using AutoBot.Models;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    public class MultipleBetData : BaseBetData
    {
        public decimal BaseBet { get; set; }
        public decimal StartingPayIn { get; set; }
        public decimal StopMaxBalance { get; set; }
        public decimal StopMinBalance { get; set; }
        public bool ResetOnWin { get; set; }
        public bool ResetOnLose { get; set; }
        public bool ResetOnLoseMaxBet { get; set; }
        public bool StopOnLoseMaxBet { get; set; }
        public decimal IncreaseOnWinPercent { get; set; }
        public decimal IncreaseOnLosePercent { get; set; }
        public decimal MaxAllowedPayIn { get; set; }
        public int MaxBets { get; set; }

        public AutomatedBetsSettings AutoSettings
        {
            get
            {
                return new AutomatedBetsSettings
                {
                    BasePayIn = BaseBet,
                    ClientSeed = ClientSeed,
                    Currency = Currency,
                    GuessHigh = GessHigh,
                    GuessLow = GessLow,
                    StartingPayIn = BaseBet,
                    IncreaseOnLosePercent = IncreaseOnLosePercent,
                    IncreaseOnWinPercent = IncreaseOnWinPercent,
                    MaxAllowedPayIn = MaxAllowedPayIn,
                    MaxBets = MaxBets,
                    ResetOnLose = ResetOnLose,
                    ResetOnLoseMaxBet = ResetOnLoseMaxBet,
                    ResetOnWin = ResetOnWin,
                    StopMaxBalance = StopMaxBalance,
                    StopMinBalance = StopMinBalance,
                    StopOnLoseMaxBet = StopOnLoseMaxBet
                };
            }
        }

        public SingleBetData GetSingleData()
        {
            return new SingleBetData
            {
                Session = this.Session,
                ClientSeed = new Random().Next(),
                Currency = this.Currency,
                Drawdoun = this.Drawdoun,
                PercentMax = this.PercentMax,
                PercentMin = this.PercentMin,
                GessHigh = new Random().Next((int)SetPercent(this.PercentMin), (int)SetPercent(this.PercentMax)),
                GessLow = 0,
                Repit = this.Repit,
                PayIn = this.BaseBet,
                MaxBet = this.MaxBet,
                AccountToSendProfit = this.AccountToSendProfit,
                ProfitEdge = this.ProfitEdge,
            };
        }

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
            MaxBet = settings.BetCapacity;
            AccountToSendProfit = settings.AccountToSendProfit;
            ProfitEdge = settings.ProfitEdge;
            BaseBet = SetBaseBet(settings);
            StartingPayIn = BaseBet;
            StopMaxBalance = 0;
            StopMinBalance = 0;
            ResetOnWin = true;
            ResetOnLose = false;
            ResetOnLoseMaxBet = false;
            StopOnLoseMaxBet = true;
            IncreaseOnLosePercent = CalcPercent(PercentMax);
            IncreaseOnWinPercent = 0;
            MaxAllowedPayIn = 0;
            MaxBets = settings.Drawdoun;            
        }

        private decimal CalcPercent(long gessHigh)
        {
            decimal baseKef = 499999; //соответствует 50% вероятности - удвоение
            return Math.Round(gessHigh / baseKef, 2);
        }
    }
}
