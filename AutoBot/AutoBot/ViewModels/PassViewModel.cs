using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoBot.ViewModels
{
    public class PassViewModel:BaseViewModel
    {
        ILoginService _loginService;
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

        public PassViewModel(LoginModel model, ILoginService loginService)
        {
            Model = model;
            _loginService = loginService;
            _loginService.OnLogin += _loginService_OnLogin;
        }

        private void _loginService_OnLogin(object sender, ResultLoginModel e)
        {
            
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
                      _loginService.Login();

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
                      new NewAccountDialog().Login();
                  }));
            }
        }
    }
}
