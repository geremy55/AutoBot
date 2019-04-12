using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Interfaces
{
    public interface IWorkAccountController<T>
    {
        event EventHandler<T> OnAddAccount;
        void AddAccount();
        void EditAccount(T model);        
    }
}
