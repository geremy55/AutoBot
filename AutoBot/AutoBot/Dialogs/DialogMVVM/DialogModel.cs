using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Dialogs.DialogMVVM
{
    public class DialogModel : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private int number;
        private string name;

        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }
        
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
;           }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
