using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Interfaces
{
    public interface ILoginService
    {
        event EventHandler<ResultLoginModel> OnLogin;
        void Login();
    }
}
