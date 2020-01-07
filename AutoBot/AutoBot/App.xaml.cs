using AutoBot.Views;
using AutoBot.ViewModels;
using System.Windows;
using AutoBot.Models;
using AutoBot.Helpers;
using System.Collections.ObjectModel;
using AutoBot.Services;
using Ninject;
using AutoBot.Interfaces;
using AutoBot.Controllers;
using AutoBot.Betting.Data;
using AutoBot.Dialogs;
using AutoBot.Dialogs.LoginDialog;
using AutoBot.Dialogs.NewAccountDialog;
using AutoBot.Dialogs.SettingsDialog;
using AutoBot.Dialogs.StrategyDialog;
using AutoBot.Betting.Interfaces;
using AutoBot.Betting.Services;

namespace AutoBot

{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<ILoginService<ResultLoginModel, LoginModel>>().To<LoginService>();
            kernel.Bind<ILoginService<string, NewAccountModel>>().To<CreateNewAccountService>();

            kernel.Bind<ISendSettings>().To<SendSettingsContainer>().InSingletonScope();            

            kernel.Bind<IBaseDialogService<ResultLoginModel>>().To<LoginDialogService>();
            kernel.Bind<IBaseDialogService<string>>().To<NewAccountDialogService>();
            kernel.Bind<IBaseDialogService<ProgrammSettingsModel>>().To<SettingsDialogService>().InSingletonScope();
            kernel.Bind<IBaseDialogService<PlayerSettingsModel>>().To<StrategyDialogService>();

            kernel.Bind<IGetAccountDataService>().To<GetAccountDataService>();

            kernel.Bind<IWorkAccountController<PlayerSettingsModel>>().To<WorkAccountController>(); 
            
            kernel.Bind<IBaseBackground>().To<BackSingleController>();
           // kernel.Bind<IBaseBackground>().To<BackMultyController>();           

            kernel.Bind<IDialogService>().To<FileDialogService>();

            kernel.Bind<IFileService<PlayerSettingsModel>>().To<BinarySerializeUserSet>();            
            
            kernel.Bind<IFileService<ProgrammSettingsModel>>().To<BinarySerializePrgSet>();

            kernel.Bind<IBetting<MultipleBetData>>().To<MultyBettingService>();
            kernel.Bind<IBetting<SingleBetData>>().To<SingleBettingService>();
            kernel.Bind<IFactory>().To<BackgroundFactory>();

            var appVM = kernel.Get<MainViewModel>();
            MainView mainView = new MainView();
            mainView.userGrid.DataContext = appVM;

            var workVM = kernel.Get<WorkAccountViewModel>();
            mainView.WorkAccountGrid.DataContext = workVM;            

            mainView.Show();
        }
    }
}
