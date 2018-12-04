using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Models
{
    public class ResultLoginModel
    {
        public SessionInfo Session { get; set; }
        public string ErrorResult { get; set; } = "";
    }
}
