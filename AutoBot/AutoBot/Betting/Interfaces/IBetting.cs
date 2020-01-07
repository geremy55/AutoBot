using AutoBot.Betting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Interfaces
{
    public interface IBetting<T>
    {
        event EventHandler<T> OnFinishMoney;
        BetResultData StartBetting(T data);
    }
}
