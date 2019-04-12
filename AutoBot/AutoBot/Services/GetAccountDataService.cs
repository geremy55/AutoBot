using AutoBot.Interfaces;
using AutoBot.Models;
using Dice.Client.Web;
using System;
using System.Threading;
using System.Windows.Threading;

namespace AutoBot.Services
{
    public class GetAccountDataService: IGetAccountDataService
    {        
        MainModel _model;
        private Timer BalanceTimer;

        public GetAccountDataService()
        {            
        }
        
        async void BalanceTimer_Tick(object sender)
        {  
           if(_model.Session!=null)
            {
                GetBalanceResponse response = await DiceWebAPI.GetBalanceAsync(_model.Session, _model.Currency);
                if (response.Success)
                {
                    _model.InitModel();
                }
            }           
        }

        public void GetAccountData(MainModel model)
        {
            _model = model;
            BalanceTimer = new Timer(new TimerCallback(BalanceTimer_Tick), null, 10, 1000 * 60);
        }
    }
}
