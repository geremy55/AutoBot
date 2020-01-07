using AutoBot.Betting.Data;
using AutoBot.Betting.Services;
using AutoBot.Controllers;
using AutoBot.Enums;
using AutoBot.Interfaces;
using AutoBot.Models;

namespace AutoBot.Services
{
    public class BackgroundFactory : IFactory
    {
        public IBaseBackground Create(PlayerSettingsModel settings)
        {            
            switch (settings.BetType)
            {
                case BetTypeEnum.SingleBet:
                    var outData = new BackSingleController(new SingleBettingService(), settings);
                    return (IBaseBackground)outData;
                case BetTypeEnum.MultyBet:
                    var single = new SingleBettingService();
                    var outMData = new BackMultyController(new MultyBettingService(single), settings);
                    return (IBaseBackground)outMData; 
            }
            return null;
        }
    }
}
