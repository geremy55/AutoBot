using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Interfaces
{
    public interface ILoginService<T, R>
    {
        event EventHandler<T> OnLogin;        
        void Start(R model);
    }
}
