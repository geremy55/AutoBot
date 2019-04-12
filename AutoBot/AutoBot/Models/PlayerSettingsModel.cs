using AutoBot.Betting.Data;
using AutoBot.Enums;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Models
{
    [Serializable]
    public class PlayerSettingsModel:BaseModel
    {
        [NonSerialized]
        private SessionInfo session;       
        private Currencies currency = Currencies.Doge;
        [NonSerialized]
        private string userName;
        [NonSerialized]
        private decimal userBalance;

        private double minPercent=49.5;
        private double maxPercent=49.95;
        private int repit=1;
        private int betUps=2;
        private int drawdoun=18;
        private BetTypeEnum betType = BetTypeEnum.SingleBet;
        private int betCapacity=1000;
        private MoneyManagerEnum moneyManager = MoneyManagerEnum.IndependentBets;
        private decimal workBalance;
        private decimal profitEdge;        
        private string accountToSendProfit; 
        private bool strategyOn = true;
        private bool mManagerOn = false;

        public bool StrategyOn
        {
            get => strategyOn;
            set
            {
                strategyOn = value;
                OnPropertyChanged("StrategyOn");
            }
        }
        public bool MManagerOn
        {
            get => mManagerOn;
            set
            {
                mManagerOn = value;
                OnPropertyChanged("MManagerOn");
            }
        }

        public SessionInfo Session
        {
            get => session;
            set
            {
                session = value;
                InitModel();
                OnPropertyChanged("Session");
            }
        }
       
        public Currencies Currency
        {
            get => currency;
            set
            {
                currency = value;
                InitModel();
                OnPropertyChanged("Currency");
            }
        }

        public override void InitModel()
        {
            base.InitModel();
            if (Session != null)
            {
                UserBalance = Session[Currency].Balance;
                WorkBalance = UserBalance;
                decimal minProfitToWitdraw = 0m;
                switch(Currency)
                {
                    case Currencies.BTC:
                        {
                            minProfitToWitdraw = 0.0005m;
                        }
                        break;
                    case Currencies.Doge:
                        {
                            minProfitToWitdraw = 2m;
                        }
                        break;
                    case Currencies.LTC:
                        {
                            minProfitToWitdraw = 0.002m;
                        }
                        break;
                    case Currencies.ETH:
                        {
                            minProfitToWitdraw = 0.002m;
                        }
                        break;
                }
                ProfitEdge = WorkBalance + minProfitToWitdraw;
            }
        }

        public double MinPercent
        {
            get => minPercent;
            set
            {
                minPercent = value;
                OnPropertyChanged("MinPercent");
            }
        }
        public double MaxPercent
        {
            get => maxPercent;
            set
            {                
                var InitData = new InitialBetData().GetInitial(value);
                maxPercent = InitData.WinPercent;
                MinPercent = maxPercent * 0.9;
                Repit = InitData.Repit;
                BetUps = InitData.BetUps;
                Drawdoun = InitData.Drawdoun;
                OnPropertyChanged("MaxPercent");
            }
        }
        public int Repit
        {
            get => repit;
            set
            {
                repit = value;
                OnPropertyChanged("Repit");
            }
        }
        public int BetUps
        {
            get => betUps;
            set
            {
                betUps = value;
                OnPropertyChanged("BetUps");
            }
        }
        public int Drawdoun
        {
            get => drawdoun;
            set
            {
                drawdoun = value;               
                OnPropertyChanged("Drawdoun");
            }
        }
        public BetTypeEnum BetType
        {
            get => betType;
            set
            {
                betType = value;
                OnPropertyChanged("BetType");
            }
        }
        public int BetCapacity
        {
            get => betCapacity;
            set
            {
                betCapacity = value;
                OnPropertyChanged("BetCapacity");
            }
        }
        public MoneyManagerEnum MoneyManager
        {
            get
            {              
               return moneyManager;                
            }            
            set
            {
                moneyManager = value;
                OnPropertyChanged("MoneyManager");
            }
        }
        public decimal WorkBalance
        {
            get => workBalance;
            set
            {
                workBalance = value;
                OnPropertyChanged("WorkBalance");
            }
        }
        public decimal ProfitEdge
        {
            get => profitEdge;
            set
            {
                profitEdge = value;
                OnPropertyChanged("ProfitEdge");
            }
        }
        public decimal UserBalance
        {
            get => userBalance;
            set
            {
                userBalance = value;
                OnPropertyChanged("UserBalance");
            }
        }
        public string AccountToSendProfit
        {
            get => accountToSendProfit;
            set
            {
                accountToSendProfit = value;
                OnPropertyChanged("AccountToSendProfit");
            }
        }
       
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
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
