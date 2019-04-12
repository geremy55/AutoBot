using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.ViewModels;
using AutoBot.Views;

namespace AutoBot.Dialogs.StrategyDialog
{
    public class StrategyDialogService : IBaseDialogService<PlayerSettingsModel>
    {
        private PlayerSettingsView view;
        private PlayerSettingsViewModel dialogVM;
        private ISendSettings _sendSettings;
        public PlayerSettingsModel ReturnedModel { get; set; }

        public StrategyDialogService(PlayerSettingsViewModel playerSettingsViewModel, ISendSettings sendSettings)
        {
            dialogVM = playerSettingsViewModel;
            dialogVM.OnPressOK += DialogVM_OnPressOK;
            _sendSettings = sendSettings;
        }
       
        public bool ShowDialog(PlayerSettingsModel model, string dialogTitle)
        { 
            if(model!=null)
            {
                dialogVM.SetModel(model);
            }
            else
            {
                dialogVM.SetModel(new PlayerSettingsModel());
            }
            view = new PlayerSettingsView();
            view.DataContext = dialogVM;
            view.Title = dialogTitle;
            return (bool)view.ShowDialog();
        }

        private void DialogVM_OnPressOK(object sender, PlayerSettingsModel e)
        {
            if (e == null) return;
            ReturnedModel = e;
            ReturnedModel.AccountToSendProfit = _sendSettings.GetData().WithdrawAdress;
            view.DialogResult = e != null ? true : false;
        }
    }
}
