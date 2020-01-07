using AutoBot.Betting.Data;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Interfaces
{
    public interface IBaseBackground
    {
        event EventHandler<BetResultData> OnSendResult;
        event EventHandler<string> OnFinishMoney;
        event EventHandler<SessionInfo> OnCompletBet;

        void Start();
        void Stop();
    }
}
