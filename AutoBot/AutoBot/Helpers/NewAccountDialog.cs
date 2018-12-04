using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.ViewModels;
using AutoBot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Helpers
{
    class NewAccountDialog : ILoginDialog
    {
        public void Login()
        {
            var model = new NewAccountModel();
            var newAccView = new NewAccountView();
            var newAccountViewModel = new NewAccountViewModel(model);
            newAccView.DataContext = newAccountViewModel;
            newAccView.ShowDialog();
        }
    }
}
