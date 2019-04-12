using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Interfaces
{
    public interface IBaseBackground<T, S>
    {
        event EventHandler<T> OnSendResult;
        event EventHandler<S> OnFinishMoney;
        event EventHandler<SessionInfo> OnCompletBet;

        void Start();
        void Stop();
    }
}
