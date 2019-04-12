using AutoBot.Dialogs.Interfaces;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.ViewModels
{
    public class SettingsViewModel : BaseViewModel, IDialogEvent<ProgrammSettingsModel>
    {
        public event EventHandler<ProgrammSettingsModel> OnPressOK;
        private IFileService<ProgrammSettingsModel> _fileService;

        private ProgrammSettingsModel _model;
        public ProgrammSettingsModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }

        public SettingsViewModel(IFileService<ProgrammSettingsModel> fileService)
        {
            _fileService = fileService;
        }

        public void SetModel(ProgrammSettingsModel model)
        {            
            Model = model;
            if(string.IsNullOrEmpty(Model.WithdrawAdress))
            {
                LoadSettings();
            }            
        }

        private void LoadSettings()
        {
            Model = _fileService.Open("set.dat");
        }

        private RelayCommand saveCommand;  
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      if (TestingInputData())
                      {
                          _fileService.Save("set.dat", Model);
                          OnPressOK?.Invoke(this, Model);
                      }
                  }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new RelayCommand(obj =>
                  {
                      OnPressOK?.Invoke(this, null);
                  }));
            }
        }
        private bool TestingInputData()
        {            
            return true;
        }
    }
}
