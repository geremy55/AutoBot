using AutoBot.Betting.Data;
using AutoBot.Enums;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Interfaces
{
    public interface IFactory
    {
        IBaseBackground Create(PlayerSettingsModel settings);
    }
}
