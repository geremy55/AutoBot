using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.ViewModels;
using AutoBot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Dialogs.SettingsDialog
{
    public class SettingsDialogService : IBaseDialogService<ProgrammSettingsModel>
    {
        private SettingsView view;
        private SettingsViewModel dialogVM;
        private ISendSettings _sendSettings;

        public ProgrammSettingsModel ReturnedModel { get; set; }

        public SettingsDialogService(SettingsViewModel settingsViewModel, ISendSettings sendSettings)
        {
            dialogVM = settingsViewModel;
            dialogVM.OnPressOK += DialogVM_OnPressOK;
            _sendSettings = sendSettings;
        }

        private void DialogVM_OnPressOK(object sender, ProgrammSettingsModel e)
        {
            ReturnedModel = e;
            if (e != null)
            {
                _sendSettings.SetData(e);
                view.DialogResult = true;
            }
            else view.DialogResult = false;    
        }

        public bool ShowDialog(ProgrammSettingsModel model, string dialogTitle)
        {
            dialogVM.SetModel(model);
            view = new SettingsView();
            view.DataContext = dialogVM;
            view.Title = dialogTitle;
            return (bool)view.ShowDialog();
        }
    }
}
