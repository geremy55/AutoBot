using AutoBot.Dialogs;
using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Controllers
{
    public class WorkAccountController : IWorkAccountController<PlayerSettingsModel>
    {
        public event EventHandler<PlayerSettingsModel> OnAddAccount;
        private IBaseDialogService<PlayerSettingsModel> _playerSettingsDialog;

        public WorkAccountController(IBaseDialogService<PlayerSettingsModel> playerSettingsDialog)
        {
            _playerSettingsDialog = playerSettingsDialog;
        }

        public void AddAccount()
        {
            SendRezult(null);
        }

        public void EditAccount(PlayerSettingsModel model)
        {
            SendRezult(model);
        }

        private void SendRezult(PlayerSettingsModel model)
        {
            if (_playerSettingsDialog.ShowDialog(model, "Настройки стратегии"))
            {
                var _model = _playerSettingsDialog.ReturnedModel;
                if (_model.Session == null) return;
                OnAddAccount?.Invoke(this, _model);
            }
        }
    }
}
