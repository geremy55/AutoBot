using AutoBot.Interfaces;
using AutoBot.Models;

namespace AutoBot.Helpers
{
    public class SendSettingsContainer : ISendSettings
    {
        private ProgrammSettingsModel Settings { get; set; }

        public SendSettingsContainer()
        {

        }

        public ProgrammSettingsModel GetData()
        {
            return Settings;
        }

        public void SetData(ProgrammSettingsModel settings)
        {
            Settings = settings;
        }
    }
}