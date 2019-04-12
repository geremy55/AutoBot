using AutoBot.Betting.Data;
using AutoBot.Betting.Interfaces;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoBot.Betting.Services
{
    public class SingleBettingService : ISingleBetting<SingleBetData>
    {  
        public event EventHandler<SingleBetData> OnFinishMoney;
        public SingleBettingService() { }

        public BetResultData StartBetting(SingleBetData bet)
        {
            int betNr = 0;
            var rezult = new BetResultData();
            while (true)
            {
                PlaceBetResponse rezBet = DiceWebAPI.PlaceBet(bet.Session, bet.PayIn, bet.GessLow, bet.GessHigh, bet.ClientSeed, bet.Currency);

                if (rezBet.Success)
                {
                    betNr++;                    
                    if (rezBet.PayOut > 0)
                    {
                        rezult.Profit += rezBet.PayOut - bet.PayIn;
                        rezult.BetCount = betNr;
                        rezult.Percent = bet.GessHigh;
                        rezult.Balance = bet.Session[bet.Currency].Balance;
                        rezult.Session = bet.Session;
                        break;
                    }
                    else
                    {
                        rezult.Profit -= bet.PayIn;
                        CalcPercent(betNr, bet);
                        bet.ClientSeed = new Random().Next();
                        if (betNr % bet.Repit == 0 && betNr != 0) bet.PayIn = bet.PayIn * 2;
                    }
                }
                else
                {
                    if (rezBet.InsufficientFunds)
                    {
                        OnFinishMoney?.Invoke(this, bet);
                    }
                    else
                    {
                        Thread.Sleep(30000);
                    }
                }
            }
            return rezult;
        }

        private void CalcPercent(int betNr, SingleBetData bet)
        {
            int col = (bet.Drawdoun / 2) / 3;
            long perCol = (bet.PercentMax - bet.PercentMin) / 3;

            if (betNr <= col)
            {
                bet.GessHigh = new Random().Next((int)bet.PercentMin, (int)(bet.PercentMin + perCol));
            }
            else if (betNr > col && betNr <= 2 * col)
            {
                bet.GessHigh = new Random().Next((int)(bet.PercentMin + perCol), (int)(bet.PercentMin + perCol * 2));
            }
            else if (betNr > 2 * col)
            {
                bet.GessHigh = new Random().Next((int)(bet.PercentMin + perCol * 2), (int)bet.PercentMax);
            }
        }
    }
}
