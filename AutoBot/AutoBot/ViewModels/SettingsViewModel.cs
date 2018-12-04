using AutoBot.Helpers;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private SettingsModel _model;
        public SettingsModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }

        public SettingsViewModel(SettingsModel model)
        {
            Model = model;
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      
                  }));
            }
        }
    }
}
