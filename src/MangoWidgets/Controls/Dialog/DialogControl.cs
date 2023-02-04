using System.Windows.Controls;
using MangoWidgets.Common;

namespace MangoWidgets.Controls;

public class DialogControl:UserControl,IDialogControl
{
    public event RoutedDialogControlCloseEvent Closed;

    public void Close()
    {
        Close(null);
    }

    public void Close(bool? result = false)
    {
        Closed?.Invoke(this, result);
    }
    
}