using AutoBot.Betting.Data;
using AutoBot.Interfaces;
using AutoBot.ViewModels;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoBot.Models
{
    public class WorkAccountModel:BaseViewModel
    {
        private IBaseBackground background;
        private PlayerSettingsModel playerSettings;
        private Currencies currency = Currencies.BTC;
        private SessionInfo session;
        private string userName;
        private decimal userBalance;
        private int maxPositive; 
        private double percent;
        private decimal profit;
        private int betNumber;
        private int positiveNuberMax;
        private int negativeNuberMax;
        private bool buttonOn = true;
        private string btnGo = "Старт";
        private SolidColorBrush btnColor = new SolidColorBrush(Colors.GreenYellow);

        public void InitModel()
        {
        }

        public IBaseBackground Background
        {
            get => background;
            set
            {
                background = value;
                OnPropertyChanged("Background");
            }
        }
        
        public PlayerSettingsModel PlayerSettings
        {
            get => playerSettings;
            set
            {
                playerSettings = value;
                OnPropertyChanged("PlayerSettings");
            }
        }
        
        public SessionInfo Session
        {
            get => session;
            set
            {
                session = value;                
                OnPropertyChanged("Session");
            }
        }
       
        public Currencies Currency
        {
            get => currency;
            set
            {
                currency = value;                
                OnPropertyChanged("Currency");
            }
        }               

        public int MaxPositive
        {
            get => maxPositive;
            set
            {
                maxPositive = value;
                OnPropertyChanged("MaxPositive");
            }
        }
        public SolidColorBrush BtnColor
        {
            get => btnColor;
            set
            {
                btnColor = value;
                OnPropertyChanged("BtnColor");
            }
        }
        public bool ButtonOn
        {
            get => buttonOn;
            set
            {
                BtnColor = value ? new SolidColorBrush(Colors.GreenYellow) : new SolidColorBrush(Colors.Red);
                buttonOn = value;
                OnPropertyChanged("ButtonOn");
            }
        }
        public string BtnGo
        {
            get => btnGo;
            set
            {
                btnGo = value;
                OnPropertyChanged("BtnGo");
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
        public decimal UserBalance
        {
            get => userBalance;
            set
            {
                userBalance = value;
                OnPropertyChanged("UserBalance");
            }
        }
        public double Percent
        {
            get => percent;
            set
            {
                percent = value > 100 ? Math.Round(value / 10000, 3) : value;
                OnPropertyChanged("Percent");
            }
        }
        public decimal Profit
        {
            get => profit;
            set
            {
                profit = value;
                OnPropertyChanged("Profit");
            }
        }
        public int BetNumber
        {
            get => betNumber;
            set
            {
                betNumber = value;
                OnPropertyChanged("BetNumber");
            }
        }
        public int PositiveNuberMax
        {
            get => positiveNuberMax;
            set
            {
                positiveNuberMax = value;
                OnPropertyChanged("PositiveNuberMax");
            }
        }
        public int NegativeNuberMax
        {
            get => negativeNuberMax;
            set
            {
                negativeNuberMax = value;
                OnPropertyChanged("NegativeNuberMax");
            }
        }

    }
}
