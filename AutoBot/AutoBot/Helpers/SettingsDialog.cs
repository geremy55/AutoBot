using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.ViewModels;
using AutoBot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Helpers
{
    public class SettingsDialog : ISettingsDiallog
    {
        private SettingsModel _settings;
        public SettingsDialog (SettingsModel settings)
        {
            _settings = settings;
        }
        public void Start()
        {
            SettingsViewModel setViewModel = new SettingsViewModel(_settings);
            SettingsView view = new SettingsView();
            view.DataContext = setViewModel;
            view.ShowDialog();
        }
    }
}
