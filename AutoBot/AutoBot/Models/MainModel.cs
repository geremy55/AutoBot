using AutoBot.Interfaces;
using AutoBot.ViewModels;
using Dice.Client.Web;

namespace AutoBot.Models
{
    public class MainModel: BaseModel
    {
        private readonly IGetAccountDataService _getAccountDataService;

        private SessionInfo session;
        private Currencies currency = Currencies.BTC;
        private decimal balance;
        private long betCount;
        private decimal betProfits;
        private long betWins;
        private decimal betWinsPercent;
        private string accountName;        

        public MainModel(IGetAccountDataService getAccountDataService)
        {
            _getAccountDataService = getAccountDataService;            
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
        
        public decimal Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        
        public long BetCount
        {
            get=>betCount;
            set
            {
                betCount = value;
                OnPropertyChanged("BetCount");
            }
        }
        
        public decimal BetProfits
        {
            get=> betProfits;
            set
            {
                betProfits = value;
                OnPropertyChanged("BetProfits");
            }
        }
        
        public long  BetWins
        {
            get=> betWins;
            set
            {
                betWins = value;
                OnPropertyChanged("BetWins");
            }
        }
        
        public decimal BetWinsPercent
        {
            get=> betWinsPercent;
            set
            {
                betWinsPercent = value;
                OnPropertyChanged("BetWinsPercent");
            }
        }
        
        public string AccountName
        {
            get => accountName;
            set
            {
                accountName = value;
                OnPropertyChanged("AccountName");
            }
        } 

        public override void InitModel()
        {
            if (Session == null) return;
            _getAccountDataService.GetAccountData(this);
            Balance = Session[Currency].Balance;
            BetCount = Session[Currency].BetCount;
            BetProfits = Session[Currency].BetPayIn + Session[Currency].BetPayOut;
            BetWins = Session[Currency].BetWinCount;
            BetWinsPercent = Session[Currency].BetCount == 0
                ? 0
                : System.Math.Round(Session[Currency].BetWinCount * 100M / Session[Currency].BetCount, 6);
            AccountName = Session.Username;
        }


    }
}
