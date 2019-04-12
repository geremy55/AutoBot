using AutoBot.Betting.Data;
using AutoBot.Betting.Services;
using AutoBot.Controllers;
using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Services
{
    public class CreateThreadForPlayerService : ICreateThreadForPlayer<BetResultData, SingleBetData>
    {        
        public CreateThreadForPlayerService()
        { 
        }       

        public IBaseBackground<BetResultData, SingleBetData> Create(PlayerSettingsModel settingsModel)
        {  
            return new BackgroundBettingController(new SingleBettingService(), settingsModel);
        }        
    }
}
