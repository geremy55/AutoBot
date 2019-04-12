using AutoBot.Interfaces;
using AutoBot.Models;
using AutoBot.Properties;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AutoBot.Services
{
    public class CreateNewAccountService : ILoginService<string, NewAccountModel>
    {        
        public event EventHandler<string> OnLogin;
        private NewAccountModel _newAccountModel;

        public CreateNewAccountService()
        { }

        public void Start(NewAccountModel model)
        {
            _newAccountModel = model;
            GetNewAccount();
        }

        private async void GetNewAccount()
        {
            BeginSessionResponse sessionResult=null;
            try
            {
                sessionResult = await DiceWebAPI.BeginSessionAsync(Settings.Default.ApiSettings);
            }
            catch(Exception ex)
            {
            }
            
            if (sessionResult.Success)
            {
                CreateUserResponse newAccResult = null;
                try
                {
                    newAccResult = await DiceWebAPI.CreateUserAsync(sessionResult.Session, _newAccountModel.Login, _newAccountModel.PasswordOne);
                }
                catch (Exception ex)
                {
                }

                if (newAccResult.Success)
                {
                    MessageBox.Show("New account created!");
                    OnLogin?.Invoke(this, _newAccountModel.Login);
                }
                else
                {
                    ErrorProcessingCreateUser(newAccResult);
                }
               
            }
            else
            {
                ErrorProcessingBeginSession(sessionResult);
            }
           
        }

        private void ErrorProcessingBeginSession(BeginSessionResponse error)
        {
            if(error.InvalidApiKey)
            {
                MessageBox.Show("Invalid API Key!");
            }  
        }

        private void ErrorProcessingCreateUser(CreateUserResponse error)
        {            
            if (error.RateLimited)
            {
                Thread.Sleep(30000);
                GetNewAccount();
            }           
            else if (error.UsernameAlreadyTaken)
                MessageBox.Show("That username is already taken, please try another");
            else if (error.AccountAlreadyHasUser)
                MessageBox.Show("This account already has a username");           
            else if (error.ErrorMessage != null)
                MessageBox.Show("Error: " + error.ErrorMessage);
            else
                MessageBox.Show("An error has occurred, please try again.");
        }
    }
}
