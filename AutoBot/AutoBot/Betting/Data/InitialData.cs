using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    public class InitialData
    {
        //процент выигрыша
        public double WinPercent { get; set; }
        //количество повторений ставки
        public int Repit { get; set; }
        //во сколько раз увеличить ставку
        public int BetUps { get; set; }
        //просадка
        public int Drawdoun { get; set; }
    }
}
