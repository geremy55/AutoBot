using AutoBot.Dialogs;
using AutoBot.Dialogs.Interfaces;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoBot.ViewModels
{
    public class PlayerSettingsViewModel : BaseViewModel, IDialogEvent<PlayerSettingsModel>
    {
        public event EventHandler<PlayerSettingsModel> OnPressOK;

        private IBaseDialogService<ResultLoginModel> _loginDialogService;
        private IFileService<PlayerSettingsModel> _fileService;
        private IDialogService _dialogService;

        private PlayerSettingsModel playerModel;
        public PlayerSettingsModel PlayerModel
        {
            get => playerModel;
            set
            {
                playerModel = value;
                OnPropertyChanged("PlayerModel");
            }
        }

        public PlayerSettingsViewModel( IBaseDialogService<ResultLoginModel> loginDialogService, 
            IDialogService dialogService, IFileService<PlayerSettingsModel> fileService)
        {
            PlayerModel = new PlayerSettingsModel();
            _loginDialogService = loginDialogService;            
            _fileService = fileService;
            _dialogService = dialogService;
        }

        public void SetModel(PlayerSettingsModel model)
        {
            PlayerModel = model;
        }
           
        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(obj =>
                  {
                      if (_loginDialogService.ShowDialog(null, "Авторизация"))
                      {
                          var e = _loginDialogService.ReturnedModel;
                          if (e.Session == null)
                          {
                              MessageBox.Show(e.ErrorResult);
                              return;
                          }
                          PlayerModel.Session = e.Session;
                          PlayerModel.UserBalance = e.Session[PlayerModel.Currency].Balance;
                          PlayerModel.UserName = e.Session.Username;
                      }
                  }));
            }
        }

        private RelayCommand loadUsersCommand;
        public RelayCommand LoadUsersCommand
        {
            get
            {
                return loadUsersCommand ??
                  (loadUsersCommand = new RelayCommand(obj =>
                  {
                      
                  }));
            }
        }       


        private RelayCommand addAccountCommand;
        public RelayCommand AddAccountCommand
        {
            get
            {
                return addAccountCommand ??
                  (addAccountCommand = new RelayCommand(obj =>
                  {
                      if (TestingInputData())
                      {
                          OnPressOK?.Invoke(this, PlayerModel);
                      }                      
                  }));
            }
        }
        private bool TestingInputData()
        {
            return true;
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new RelayCommand(obj =>
                  {
                      OnPressOK?.Invoke(this, null);
                  }));
            }
        }

        // команда сохранения настроек
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.SaveFileDialog() == true)
                          {
                              _fileService.Save(_dialogService.FilePath, PlayerModel);
                              _dialogService.ShowMessage("Настройки сохранены");
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда загрузки настроек
        private RelayCommand loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                  (loadCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.OpenFileDialog() == true)
                          {
                              var loadSet = _fileService.Open(_dialogService.FilePath);
                              PlayerModel.MaxPercent = loadSet.MaxPercent;
                              PlayerModel.MinPercent = loadSet.MinPercent;
                              PlayerModel.BetCapacity = loadSet.BetCapacity;
                              PlayerModel.BetType = loadSet.BetType;
                              PlayerModel.BetUps = loadSet.BetUps;
                              PlayerModel.Currency = loadSet.Currency;
                              PlayerModel.Drawdoun = loadSet.Drawdoun;
                              PlayerModel.MManagerOn = loadSet.MManagerOn;
                              PlayerModel.MoneyManager = loadSet.MoneyManager;                              
                              PlayerModel.ProfitEdge = loadSet.ProfitEdge;
                              PlayerModel.Repit = loadSet.Repit;
                              PlayerModel.WorkBalance = loadSet.WorkBalance;
                              PlayerModel.AccountToSendProfit = loadSet.AccountToSendProfit;
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }

                  }));
            }
        }
    }
}
