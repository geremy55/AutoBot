using AutoBot.Helpers;
using System;
using System.ComponentModel;
using AutoBot.Dialogs.Interfaces;

namespace AutoBot.Dialogs.DialogMVVM
{
    public class DialogViewModel : INotifyPropertyChanged, IDialogEvent<bool>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<bool> OnPressOK;

        private DialogModel myModel;
        public DialogModel MyModel
        {
            get => myModel;
            set
            {
                myModel = value;
                OnPropertyChanged("MyModel");
            }
        }

        public DialogViewModel(DialogModel model)
        {
            MyModel = model;
        }

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ??
                  (okCommand = new RelayCommand(obj =>
                  {                      
                      if(TestingInputData())
                      {
                          OnPressOK?.Invoke(this, true);
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
                      OnPressOK?.Invoke(this, false);
                  }));
            }
        }

        private bool TestingInputData()
        {
            if(MyModel.Name=="aaa") return true;
            return false;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
