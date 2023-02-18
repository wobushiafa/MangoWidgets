using MangoWidgets.Controls;
using MangoWidgets.MVVM.Contracts;
using MangoWidgets.Sample.ViewModels;

namespace MangoWidgets.Sample.Views;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : CustomWindow
{
    public MainViewModel ViewModel { get; init; }

    private readonly IDialogService _dialogService;
    public MainView(MainViewModel viewModel,IDialogService dialogService)
    {
        ViewModel = viewModel;
        DataContext= ViewModel;

        InitializeComponent();

        _dialogService = dialogService;
        _dialogService.SetDialogHost<MainViewModel>(RootDialogHost);
    }

    private async void btnShowDialog_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var dialogView = App.Resolve<IDialogControl<TestDialogViewModel>>();
        await _dialogService.ShowDialogAsync<MainViewModel>(dialogView);
    }
}