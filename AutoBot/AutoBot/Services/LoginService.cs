using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.Properties;
using Dice.Client.Web;
using System;
using System.Threading;

namespace AutoBot.Services
{
    class LoginService: ILoginService<ResultLoginModel, LoginModel>
    {
        public event EventHandler<ResultLoginModel> OnLogin;
        private LoginModel _loginModel;    

        public void Start(LoginModel model)
        {
            _loginModel = model;
            LoginGo();            
        }

        private async void LoginGo()
        {
            BeginSessionResponse result = null;
            try
            {
                result = await DiceWebAPI.BeginSessionAsync(Settings.Default.ApiSettings, _loginModel.Login,
               _loginModel.Password, int.Parse(_loginModel.GoogleCode==""?"0": _loginModel.GoogleCode));
            }
            catch (Exception ex)
            {
            }

            if(result!=null)
            {
                OnLogin?.Invoke(this, new ResultLoginModel { Session = result.Session, ErrorResult = ErrorProcessing(result) });
            } 
        }

        private string ErrorProcessing(BeginSessionResponse result)
        {
            string loginError = "Успешно";
            if (result.RateLimited)
            {
                Thread.Sleep(3000);
                LoginGo();
            }
            if (result.LoginRequired||result.WrongUsernameOrPassword)
            {
                loginError = "Неверный логин или пароль";
            }
            return loginError;
        }
        
    }
}
