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
    class MultyBettingService : IBetting<MultipleBetData>
    {        
        public event EventHandler<MultipleBetData> OnFinishMoney;
        private IBetting<SingleBetData> singleBetting;

        public MultyBettingService(IBetting<SingleBetData> singleBetting)
        {
            this.singleBetting = singleBetting;
            this.singleBetting.OnFinishMoney += SingleBetting_OnFinishMoney;
        }

        private void SingleBetting_OnFinishMoney(object sender, SingleBetData e)
        {
            
        }

        public BetResultData StartBetting(MultipleBetData data)
        {
            if (data == null) return null; 

            PlaceAutomatedBetsResponse rezBet = DiceWebAPI.PlaceAutomatedBets(data.Session, data.AutoSettings);
            var rezult = new BetResultData {Session=data.Session};
            if (rezBet.Success)
            {
                if (rezBet.TotalPayOut == 0) //запуск одиночных ставок
                {
                    decimal baseBet;
                    if (rezBet.BetCount > 1)
                    {
                        baseBet = rezBet.PayIns[rezBet.BetCount - 1] <= rezBet.PayIns[rezBet.BetCount - 2] ?
                                        2 * rezBet.PayIns[rezBet.BetCount - 1] : 2 * rezBet.PayIns[rezBet.BetCount - 2];
                    }
                    else
                    {
                        baseBet = 2 * rezBet.PayIns[rezBet.BetCount - 1];
                    }
                    //сформировать исходные данные для одиночных ставок
                    SingleBetData singlData = data.GetSingleData();
                    singlData.PayIn = baseBet;
                    BetResultData singleResult = singleBetting.StartBetting(singlData);

                    rezult.BetCount = rezBet.BetCount + singleResult.BetCount;
                    rezult.Percent = singleResult.Percent;
                    rezult.Profit = rezBet.TotalPayOut + singleResult.Profit;
                    rezult.Balance = data.Session[data.Currency].Balance;
                }
                else //получен профит
                {
                    rezult.BetCount = rezBet.BetCount;
                    rezult.Percent = 0;                    
                    rezult.Profit = rezBet.TotalPayOut + rezBet.TotalPayIn;
                    rezult.Balance= data.Session[data.Currency].Balance;
                }
            }
            else
            {
                if (rezBet.InsufficientFunds)
                {
                    OnFinishMoney?.Invoke(this, data);
                }
                else
                {
                    Thread.Sleep(30000);
                }
            }

            return rezult;
        }        
    }
}
