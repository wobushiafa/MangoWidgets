using System;
using System.Threading.Tasks;
using MangoWidgets.Controls;
using MangoWidgets.MVVM.Contracts;

namespace MangoWidgets.MVVM.Service;

public class DialogService:IDialogService
{
    private IDialogHost _dialogHost;
    public virtual void SetDialogHost(IDialogHost dialogHost)
    {
        _dialogHost = dialogHost;
    }

    public virtual IDialogHost GetDialogHost()
    {
        if (_dialogHost is null)
            throw new InvalidOperationException(
                $"The {typeof(DialogService)} cannot be used unless previously defined width {typeof(IDialogHost)}.{nameof(SetDialogHost)}().");
        return _dialogHost;
    }

    public virtual async Task<bool?> ShowDialogAsync(IDialogControl content)
    {
        var dialogHost = GetDialogHost();
        return await dialogHost.ShowDialogAsync(content);
    }
}

public class DialogService<T> : DialogService,IDialogService<T>
{

}