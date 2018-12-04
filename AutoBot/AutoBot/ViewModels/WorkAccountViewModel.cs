using AutoBot.Helpers;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoBot.ViewModels
{
    public class WorkAccountViewModel:BaseViewModel
    {
        private WorkAccountModel selectedModel;

        public WorkAccountModel SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }


        private ObservableCollection<WorkAccountModel> model;
        public ObservableCollection<WorkAccountModel> Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public WorkAccountViewModel(ObservableCollection<WorkAccountModel> _model)
        {
            model = _model;
        }

        // команда на запуск ставок
        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(obj =>
                  {
                      WorkAccountModel selItem = obj as WorkAccountModel;
                      if (selItem == null) return;
                      SelectedModel.ButtonOn = !selItem.ButtonOn;
                      SelectedModel.BtnColor = new SolidColorBrush(Colors.Red);
                      // StartBet(selItem);

                  }));
            }
        }

    }
}
