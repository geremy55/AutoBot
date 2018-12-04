using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.Services;
using AutoBot.ViewModels;
using AutoBot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Helpers
{
    public class LoginDialog : ILoginDialog
    {
        public void Login()
        {
            var loginModel = new LoginModel();
            var passView = new PassView();
            var loginservice = new LoginService(loginModel);
            var passViewModel = new PassViewModel(loginModel, loginservice);
            passView.DataContext = passViewModel;
            passView.ShowDialog();

        }
    }
}
