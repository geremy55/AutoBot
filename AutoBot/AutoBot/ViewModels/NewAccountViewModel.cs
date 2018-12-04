using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.ViewModels
{
    public class NewAccountViewModel :BaseViewModel
    {
        private NewAccountModel model;
        public NewAccountModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }
        public NewAccountViewModel( NewAccountModel _model)
        {
            model = _model;
        }
    }
}
