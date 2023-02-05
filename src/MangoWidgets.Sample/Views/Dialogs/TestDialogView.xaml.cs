using MangoWidgets.Controls;
using MangoWidgets.Sample.ViewModels;

namespace MangoWidgets.Sample.Views
{
    /// <summary>
    /// TestDialogView.xaml 的交互逻辑
    /// </summary>
    public partial class TestDialogView : DialogControl
    {
        public TestDialogViewModel ViewModel { get; init; }

        public TestDialogView(TestDialogViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext= ViewModel;
            InitializeComponent();
        }
    }
}
