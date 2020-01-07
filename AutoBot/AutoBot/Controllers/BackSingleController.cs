using AutoBot.Betting.Data;
using AutoBot.Betting.Interfaces;
using AutoBot.Enums;
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
    public class BackSingleController : BaseBackground, IBaseBackground
    {
        public event EventHandler<BetResultData> OnSendResult;
        public event EventHandler<string> OnFinishMoney;
        public event EventHandler<SessionInfo> OnCompletBet;

        private PlayerSettingsModel _settings;
        private IBetting<SingleBetData> _singleBetting;
        private SingleBetData _betData;

        public BackSingleController(IBetting<SingleBetData> singleBetting, PlayerSettingsModel settings) : base()
        {
            _singleBetting = singleBetting;
            _singleBetting.OnFinishMoney += _singleBetting_OnFinishMoney;
            _settings = settings;
            _betData = new SingleBetData();
            _betData.SetData(_settings);
        }        

        public void Start()
        {            
            myWorker.RunWorkerAsync(_betData);
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
            SingleBetData bet = (SingleBetData)e.Argument;
            if (bet == null) return;

            int myBetCount = 0;

            while (bet.MaxBet > myBetCount)
            {
                var rezult = _singleBetting.StartBetting(bet);
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
                OnCompletBet?.Invoke(this, _betData.Session);
            }
        }

        private void ProcessResult(BetResultData data, SingleBetData bet)
        {     
            if(data.Balance>bet.ProfitEdge)
            {
                decimal minProfitToWitdraw=0;
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
            _settings.UserBalance += data.Profit;
            bet.PayIn = bet.SetBaseBet(_settings);
        }        

        private void _singleBetting_OnFinishMoney(object sender, SingleBetData e)
        {
            OnFinishMoney?.Invoke(this, e.Session.Username);
        }        
    }
}
