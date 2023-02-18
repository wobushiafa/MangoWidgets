using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MangoWidgets.Sample.ViewModels;

public partial class TestDialogViewModel:ObservableObject
{
    [ObservableProperty]
    private bool? _dialogResult;


    [RelayCommand]
    private void Close()
    {
        DialogResult = false;
    }
}
