using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoBot.Models
{
    public class LoginModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public string SelectedPath { get; set; } = "www.999dice.com";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string GoogleCode { get; set; } = "";
        private string statusText = "";
        public string StatusText
        {
            get => statusText;
            set
            {
                statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Login":
                        {
                            if (string.IsNullOrEmpty(Login))                            
                            {
                                error = "Введите логин";
                            }
                        }
                        break;
                    case "Password":
                        break;

                    case "GoogleCode":
                        {
                            if (string.IsNullOrEmpty(GoogleCode)) break;
                            Regex regex = new Regex(@"\d{6}");

                            if (!regex.IsMatch(GoogleCode))
                            {
                                error = "Нужно ввести 6 цифр";
                            }
                        }
                        break;
                }
                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
