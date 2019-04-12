using AutoBot.Dialogs.DialogMVVM;
using AutoBot.Models;
using AutoBot.Services;
using AutoBot.ViewModels;
using AutoBot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Dialogs.NewAccountDialog
{
    public class NewAccountDialogService : IBaseDialogService<string>
    {
        private NewAccountView view;
        private NewAccountViewModel dialogVM;
        public string ReturnedModel { get; set; }

        public NewAccountDialogService(NewAccountViewModel newAccountViewModel)
        {            
            dialogVM = newAccountViewModel;
            dialogVM.OnPressOK += DialogVM_OnPressOK;            
        }

        private void DialogVM_OnPressOK(object sender, string e)
        {
            ReturnedModel = e;
            view.DialogResult = e != null ? true : false;           
        }

        public bool ShowDialog(string model, string dialogTitle)
        {
            view = new NewAccountView();
            view.DataContext = dialogVM;
            view.Title = dialogTitle;
            return (bool)view.ShowDialog();
        }
    }
}
