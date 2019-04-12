using AutoBot.Dialogs.NewAccountDialog;
using AutoBot.Helpers;
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

namespace AutoBot.Dialogs.LoginDialog
{
    public class LoginDialogService : IBaseDialogService<ResultLoginModel>
    {
        private PassViewModel dialogVM;
        private PassView view;
        public ResultLoginModel ReturnedModel { get; set; }

        public LoginDialogService(PassViewModel passViewModel)
        {           
            dialogVM = passViewModel;
            dialogVM.OnPressOK += DialogVM_OnPressOK;             
        }

        private void DialogVM_OnPressOK(object sender, ResultLoginModel e)
        {
            ReturnedModel = e;
            view.DialogResult = e!=null?true:false;            
        }

        public bool ShowDialog(ResultLoginModel model, string dialogTitle)
        {
            view = new PassView();
            view.DataContext = dialogVM;
            view.Title = dialogTitle;
            return (bool)view.ShowDialog();
        }
    }
}
