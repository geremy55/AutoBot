using AutoBot.Models;

namespace AutoBot.Interfaces
{
    public interface ISendSettings
    {       

        void SetData(ProgrammSettingsModel settings);
        ProgrammSettingsModel GetData();
    }
}