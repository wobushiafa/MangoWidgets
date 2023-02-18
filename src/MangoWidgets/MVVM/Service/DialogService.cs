using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using MangoWidgets.Interfaces;
using MangoWidgets.MVVM.Contracts;

namespace MangoWidgets.MVVM.Service;

public class DialogService:IDialogService
{
    private readonly ConcurrentDictionary<string, IDialogHost> _dialogHostDic = new();

    public virtual void SetDialogHost<T>(IDialogHost dialogHost)
    {
        _dialogHostDic[nameof(T)] = dialogHost;
    }

    public virtual IDialogHost GetDialogHost<T>()
    {
        _dialogHostDic.TryGetValue(nameof(T), out var dialogHost);
        if (dialogHost is null)
            throw new InvalidOperationException(
                $"The {typeof(DialogService)} cannot be used unless previously defined width {typeof(IDialogHost)}.{nameof(SetDialogHost)}<{nameof(T)}>().");
        return dialogHost;
    }

    public virtual async Task<bool?> ShowDialogAsync<T>(IDialogControl content)
    {
        var dialogHost = GetDialogHost<T>();
        return await dialogHost.ShowDialogAsync(content);
    }
}