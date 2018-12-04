using AutoBot.Views;
using AutoBot.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoBot.Models;
using AutoBot.Helpers;
using System.Collections.ObjectModel;

namespace AutoBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainView view = new MainView();
            var loginDialog = new LoginDialog();

            SettingsModel settingsModel = new SettingsModel();
            var settingsDialog = new SettingsDialog(settingsModel); 

            MainModel mainModel = new MainModel();
            MainViewModel mainViewModel = new MainViewModel(mainModel, loginDialog, settingsDialog);
            view.UserGrid.DataContext = mainViewModel;

            ObservableCollection<WorkAccountModel> workModel = new ObservableCollection<WorkAccountModel>
            { new WorkAccountModel() };
            WorkAccountViewModel workViewModel = new WorkAccountViewModel(workModel);
            view.WorkAccountGrid.DataContext = workViewModel;



            view.Show();
        }
    }
}
