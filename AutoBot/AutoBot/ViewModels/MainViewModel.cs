using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private SessionInfo _session;
        private ILoginDialog _loginDialog;
        ISettingsDiallog _settingsDiallog;
        private MainModel model;
        public MainModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        private Currencies selectedCurrency = Currencies.BTC;
        public Currencies SelectedCurrency
        {
            get => selectedCurrency;
            set
            {
                selectedCurrency = value;
                Model.InitModel(_session, selectedCurrency);
                OnPropertyChanged("SelectedCurrency");
            }
        }

        public MainViewModel(MainModel _model, ILoginDialog loginDialog, ISettingsDiallog settingsDiallog)
        {
            if(_model!=null)
            {
                Model = _model;
            }
            _loginDialog = loginDialog;
            _settingsDiallog = settingsDiallog;
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(obj =>
                  {
                      _loginDialog.Login();
                  }));
            }
        }

        private RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new RelayCommand(obj =>
                  {
                      _settingsDiallog.Start();
                  }));
            }
        }
    }
}
