using AutoBot.Betting.Data;
using AutoBot.Betting.Interfaces;
using AutoBot.Interfaces;
using AutoBot.Models;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Controllers
{
    public class BackMultyController : BaseBackground, IBaseBackground
    {
        public event EventHandler<BetResultData> OnSendResult;
        public event EventHandler<string> OnFinishMoney;
        public event EventHandler<SessionInfo> OnCompletBet;

        private IBetting<MultipleBetData> multyBetting;
        private PlayerSettingsModel settings;
        private MultipleBetData betData;

        public BackMultyController(IBetting<MultipleBetData> multyBetting, PlayerSettingsModel settings) : base()
        {
            this.multyBetting = multyBetting;
            this.multyBetting.OnFinishMoney += MultyBetting_OnFinishMoney;
            this.settings = settings;
            betData = new MultipleBetData();
            betData.SetData(settings);
        }   
                
        public void Start()
        {
            myWorker.RunWorkerAsync(betData);
        }

        public void Stop()
        {
            if (myWorker.IsBusy)
            {
                myWorker.CancelAsync();
            }
        }

        protected override void MyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MultipleBetData bet = (MultipleBetData)e.Argument;
            if (bet == null) return;

            int myBetCount = 0;

            while (bet.MaxBet > myBetCount)
            {
                var rezult = multyBetting.StartBetting(bet);
                ProcessResult(rezult, bet);

                OnSendResult?.Invoke(this, rezult);

                if (myWorker.CancellationPending)
                {
                    e.Cancel = true;

                    return;
                }
                myBetCount++;
            }
            e.Result = bet;
        }

        protected override void MyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

            }
            if (e.Error != null)
            {

            }
            else
            {
                OnCompletBet?.Invoke(this, betData.Session);
            }
        }

        private void ProcessResult(BetResultData data, MultipleBetData bet)
        {
            if (data.Balance > bet.ProfitEdge)
            {
                decimal minProfitToWitdraw = 0;
                switch (bet.Currency)
                {
                    case Currencies.BTC:
                        {

                            minProfitToWitdraw = Math.Max(data.Balance - bet.ProfitEdge, 0.0005m);
                        }
                        break;
                    case Currencies.Doge:
                        {
                            minProfitToWitdraw = Math.Max(data.Balance - bet.ProfitEdge, 2m);
                        }
                        break;
                    case Currencies.LTC:
                        {
                            minProfitToWitdraw = Math.Max(data.Balance - bet.ProfitEdge, 0.002m);
                        }
                        break;
                    case Currencies.ETH:
                        {
                            minProfitToWitdraw = Math.Max(data.Balance - bet.ProfitEdge, 0.002m);
                        }
                        break;
                }

                DiceWebAPI.Withdraw(bet.Session, minProfitToWitdraw, bet.AccountToSendProfit, bet.Currency);
            }
            settings.UserBalance += data.Profit;
            bet.BaseBet = bet.SetBaseBet(settings);
        }


        private void MultyBetting_OnFinishMoney(object sender, MultipleBetData e)
        {
            OnFinishMoney?.Invoke(this, e.Session.Username);
        }
    }
}
