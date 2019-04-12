using AutoBot.Models;

namespace AutoBot.Interfaces
{
    public interface ICreateThreadForPlayer<T, S>
    {
        IBaseBackground<T, S> Create(PlayerSettingsModel settingsModel);     
    }
}
