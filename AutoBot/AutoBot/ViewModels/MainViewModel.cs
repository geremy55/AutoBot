using AutoBot.Dialogs;
using AutoBot.Dialogs.LoginDialog;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.Services;
using Dice.Client.Web;
using System.Windows;

namespace AutoBot.ViewModels
{
    public class MainViewModel : BaseViewModel
    {        
        private readonly IBaseDialogService<ResultLoginModel> _loginDialogService;
        private readonly IBaseDialogService<ProgrammSettingsModel> _settingsDiallogService;
        private ProgrammSettingsModel _settingsModel;

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
        
        public MainViewModel(MainModel _model, IBaseDialogService<ResultLoginModel> loginDialogService, 
            IBaseDialogService<ProgrammSettingsModel> settingsDiallogService)
        {
            if(_model!=null)
            {
                Model = _model;
            }
            _settingsModel = new ProgrammSettingsModel();
            _loginDialogService = loginDialogService;
            _settingsDiallogService = settingsDiallogService;                        
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
                          Model.Session = e.Session;
                          _settingsModel.WithdrawAdress = e.Session.AccountId.ToString();
                      }
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
                      if (_settingsDiallogService.ShowDialog(_settingsModel, "Настройки"))
                      {
                          _settingsModel = _settingsDiallogService.ReturnedModel;
                      }
                  }));
            }
        }        
        
    }
}
