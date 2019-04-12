using AutoBot.Dialogs.DialogMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Dialogs
{
    public class MyDialogService : IBaseDialogService<DialogModel>
    {              
        private DialogView view;
        private DialogViewModel dialogVM;
        public DialogModel ReturnedModel { get; set; }

        public MyDialogService()
        {
            ReturnedModel = new DialogModel();
            dialogVM = new DialogViewModel(ReturnedModel);
            dialogVM.OnPressOK += DialogVM_OnPressOK;
            view = new DialogView();
        }

        private void DialogVM_OnPressOK(object sender, bool e)
        {            
            view.DialogResult = e;
            dialogVM.OnPressOK -= DialogVM_OnPressOK;
        }

        public bool ShowDialog(DialogModel model, string dialogTitle)
        {            
            view.DataContext = dialogVM;
            view.Title = dialogTitle;
            return (bool)view.ShowDialog();
        }
    }
}
