using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    public class MultipleBetData:BaseBetData
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
    }
}
