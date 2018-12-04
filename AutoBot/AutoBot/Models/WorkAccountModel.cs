using Dice.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoBot.Models
{
    public class WorkAccountModel
    {
        //private readonly SessionInfo Session;
        public string UserName { get; set; } = "Name";
        public decimal Balance { get; set; } = 100;
        public double Percent { get; set; } = 15;
        public decimal Profit { get; set; } = 1;
        public int BetNumber { get; set; } = 25;
        public int PositiveNuberMax { get; set; } = 0;
        public int NegativeNuberMax { get; set; } = 0;
        public bool ButtonOn { get; set; } = true;
        public string BtnGo { get; set; } = "Старт";
        public SolidColorBrush BtnColor { get; set; }  = new SolidColorBrush(Colors.GreenYellow);        
        
    }
}
