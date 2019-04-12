using AutoBot.Dialogs.Interfaces;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoBot.ViewModels
{
    public class NewAccountViewModel :BaseViewModel, IDialogEvent<string>
    {
        public event EventHandler<string> OnPressOK;

        private ILoginService<string, NewAccountModel> _newAccountService;
        private NewAccountModel _model;
        public NewAccountModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        public NewAccountViewModel(ILoginService<string, NewAccountModel> newAccountService)
        {
            _model = new NewAccountModel();
            _newAccountService = newAccountService;
            _newAccountService.OnLogin += _newAccountService_OnLogin;
        }        

        private RelayCommand createNewAccountCommand;
        public RelayCommand CreateNewAccountCommand
        {
            get
            {
                return createNewAccountCommand ??
                  (createNewAccountCommand = new RelayCommand(obj =>
                  {
                      var passwordBox = obj as PasswordBox;
                      if (string.IsNullOrEmpty(passwordBox.Password)) return;
                      Model.PasswordOne = passwordBox.Password;
                      _newAccountService.Start(Model);
                  }));
            }
        }
        private void _newAccountService_OnLogin(object sender, string e)
        {            
            if (!string.IsNullOrEmpty(e))
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
    }
}
