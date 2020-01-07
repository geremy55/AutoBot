using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Betting.Data
{
    public class InitialBetData
    {
        public InitialBetData() { }

        private static List<InitialData> TableData = new List<InitialData>
        {
            new InitialData{ WinPercent = 5,        Repit=10,   BetUps=2, Drawdoun=170 },
            new InitialData{ WinPercent = 5.5,      Repit=9,    BetUps=2, Drawdoun=150 },
            new InitialData{ WinPercent = 6.24,     Repit=8,    BetUps=2, Drawdoun=140 },
            new InitialData{ WinPercent = 7.135,    Repit=7,    BetUps=2, Drawdoun=125 },
            new InitialData{ WinPercent = 8.325,    Repit=6,    BetUps=2, Drawdoun=110 },
            new InitialData{ WinPercent = 9.99,     Repit=5,    BetUps=2, Drawdoun=90 },
            new InitialData{ WinPercent = 12.487,   Repit=4,    BetUps=2, Drawdoun=75 },
            new InitialData{ WinPercent = 16.65,    Repit=3,    BetUps=2, Drawdoun=48 },
            new InitialData{ WinPercent = 24.975,   Repit=2,    BetUps=2, Drawdoun=35 },
            new InitialData{ WinPercent = 49.95,    Repit=1,    BetUps=2, Drawdoun=20 },
            new InitialData{ WinPercent = 66.6,     Repit=1,    BetUps=3, Drawdoun=15 },
            new InitialData{ WinPercent = 79.92,    Repit=1,    BetUps=5, Drawdoun=12 },
            new InitialData{ WinPercent = 88.8,     Repit=1,    BetUps=9, Drawdoun=10 }
        };

        public InitialData GetInitial(double winPercent)
        {
            return TableData.FirstOrDefault(p => p.WinPercent == winPercent || p.WinPercent > winPercent);
        }
    }
}
