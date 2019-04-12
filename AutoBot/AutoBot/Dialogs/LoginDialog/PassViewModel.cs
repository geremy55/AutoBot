using AutoBot.Dialogs;
using AutoBot.Dialogs.Interfaces;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Windows.Controls;

namespace AutoBot.ViewModels
{
    public class PassViewModel:BaseViewModel, IDialogEvent<ResultLoginModel>
    {
        public event EventHandler<ResultLoginModel> OnPressOK;

        private IBaseDialogService<string> _newAccService;
        private ILoginService<ResultLoginModel, LoginModel> _loginService;

        private LoginModel _model;
        public LoginModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }       

        public PassViewModel(IBaseDialogService<string> newAccService, ILoginService<ResultLoginModel, LoginModel> loginService)
        {
            Model = new LoginModel();
            _newAccService = newAccService;
            _loginService = loginService;
            _loginService.OnLogin += _loginService_OnLogin;
        }       

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(obj =>
                  {
                      var passwordBox = obj as PasswordBox;
                      if (passwordBox == null) return;
                      Model.Password = passwordBox.Password;
                      _loginService.Start(Model);                      
                  }));
            }
        }
        private void _loginService_OnLogin(object sender, ResultLoginModel e)
        {            
            if(e.Session!=null)
            {
                OnPressOK?.Invoke(this, e);
            }
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
                     
        private RelayCommand newAccountCommand;  
        public RelayCommand NewAccountCommand
        {
            get
            {
                return newAccountCommand ??
                  (newAccountCommand = new RelayCommand(obj =>
                  {  
                      if (_newAccService.ShowDialog(null, "Новый аккаунт"))
                      {
                          Model.Login = _newAccService.ReturnedModel;
                      }                    
                  }));
            }
        }
       
    }
}
