using AutoBot.Betting.Data;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using Dice.Client.Web;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace AutoBot.ViewModels
{
    public class WorkAccountViewModel:BaseViewModel
    {
        private readonly IWorkAccountController<PlayerSettingsModel> _workAccountController;
        private readonly ICreateThreadForPlayer<BetResultData, SingleBetData> _threadForPlayer;

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

        public WorkAccountViewModel(ObservableCollection<WorkAccountModel> _model, IWorkAccountController<PlayerSettingsModel> workAccountController,
            ICreateThreadForPlayer<BetResultData, SingleBetData> threadForPlayer)
        {
            Model = _model;
            _workAccountController = workAccountController;
            _workAccountController.OnAddAccount += _workAccountController_OnAddAccount;
            _threadForPlayer = threadForPlayer;
        }

        private void _workAccountController_OnAddAccount(object sender, PlayerSettingsModel e)
        {
            var currentModel = Model.FirstOrDefault(a => a.Session.Username == e.Session.Username);
            if (currentModel != null)
            {
                currentModel.PlayerSettings = e;
                currentModel.Session = e.Session;
                currentModel.Currency = e.Currency;
                currentModel.UserBalance = e.Session[e.Currency].Balance;
                currentModel.Percent = e.MaxPercent;
                currentModel.UserName = e.Session.Username;
            }
            else
            {
                var model = new WorkAccountModel
                {
                    PlayerSettings = e,
                    UserBalance = e.Session[e.Currency].Balance,
                    Currency = e.Currency,
                    Session = e.Session,
                    Percent = e.MaxPercent,
                    UserName = e.Session.Username
                };
                Model.Insert(Model.Count, model);
                SelectedModel = model;
            }            
        }     

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      _workAccountController.AddAccount();
                         
                  }));
            }
        }       

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      WorkAccountModel selItem = obj as WorkAccountModel;
                      if (selItem == null) return;
                      _workAccountController.EditAccount(selItem.PlayerSettings);                     
                  }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      WorkAccountModel selItem = obj as WorkAccountModel;
                      if (selItem == null) return;
                      Model.Remove(Model.FirstOrDefault(i => i.UserName == selItem.Session.Username));                      
                  }));
            }
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

                      selItem.ButtonOn = !selItem.ButtonOn;
                      if (selItem.ButtonOn == false)
                      {
                          var background = _threadForPlayer.Create(selItem.PlayerSettings);
                          background.OnSendResult += Background_OnSendResult;
                          background.OnFinishMoney += Background_OnFinishMoney;
                          background.OnCompletBet += Background_OnCompletBet;

                          selItem.Background = background;
                          selItem.BetNumber = 0;
                          selItem.MaxPositive = 0;
                          selItem.NegativeNuberMax = 0;
                          selItem.BtnGo = "Стоп";
                          selItem.BtnColor = new SolidColorBrush(Colors.Red);
                          selItem.Background.Start();
                      }
                      else
                      {
                          selItem.BtnColor = new SolidColorBrush(Colors.Gray);
                          selItem.Background.Stop();
                      }
                  }));
            }
        }

        private void Background_OnCompletBet(object sender, SessionInfo e)
        {
            var item = Model.FirstOrDefault(i => i.UserName == e.Username);
            if (item == null) return;
            item.BtnGo = "Старт";
            item.BtnColor = new SolidColorBrush(Colors.GreenYellow);
            
        }

        private void Background_OnSendResult(object sender, BetResultData e)
        {
            var item = Model.FirstOrDefault(i=>i.UserName == e.Session.Username);
            if (item==null) return;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if(item.BtnColor.Color == Colors.DeepPink)
                {
                    item.BtnColor = new SolidColorBrush(Colors.GreenYellow);
                }
                if (item.PositiveNuberMax < 1)
                {
                    item.PositiveNuberMax = 1;
                }
                if (e.BetCount == 1)
                {
                    item.MaxPositive += 1;
                }
                else if (e.BetCount > 1)
                {
                    item.MaxPositive = 0;
                }

                if (e.Profit != 0) item.Profit = e.Profit;
                if (e.Percent != 0) item.Percent = e.Percent;
                if (e.Balance != 0) item.UserBalance = e.Balance;
                item.BetNumber++;
                item.NegativeNuberMax = Math.Max(item.NegativeNuberMax, e.BetCount);
                item.PositiveNuberMax = Math.Max(item.PositiveNuberMax, item.MaxPositive);
            });
        }

        private void Background_OnFinishMoney(object sender, SingleBetData e)
        {
            var item = Model.FirstOrDefault(i => i.UserName == e.Session.Username);
            if (item == null) return;
            Application.Current.Dispatcher.Invoke(() =>
            {
                item.BtnColor = new SolidColorBrush(Colors.DeepPink);
            });
        }

    }
}
