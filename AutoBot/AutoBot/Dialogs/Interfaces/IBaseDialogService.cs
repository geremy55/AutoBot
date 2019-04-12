using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Dialogs
{
    public interface IBaseDialogService<T>
    {
        T ReturnedModel { get; set; }
        bool ShowDialog(T model, string dialogTitle);
    }
}
