using AutoBot.ViewModels;
using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Models
{
    [Serializable]
    public class BaseModel: BaseViewModel
    {  
        public virtual void InitModel() { }
    }
}
