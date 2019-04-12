using System;

namespace AutoBot.Dialogs.Interfaces
{
    public interface IDialogEvent<T>
    {
        event EventHandler<T> OnPressOK;
    }
}
