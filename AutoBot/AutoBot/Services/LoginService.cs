using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.Properties;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Services
{
    class LoginService: ILoginService
    {
        public event EventHandler<ResultLoginModel> OnLogin;
        private LoginModel _loginModel;
        public LoginService(LoginModel loginModel)
        {
            _loginModel = loginModel;
        }      

        public void Login()
        {
            LoginGo();
        }

        private async void LoginGo()
        {
            BeginSessionResponse result = null;
            try
            {
                result = await DiceWebAPI.BeginSessionAsync(Settings.Default.SettingsKey, _loginModel.Login,
                _loginModel.Password, _loginModel.GoogleCode);
            }
            catch (Exception ex)
            {
            }

            if(result.Success)
            {
                OnLogin?.Invoke(this, new ResultLoginModel { Session = result.Session});
            } 
        }
       
    }
}
