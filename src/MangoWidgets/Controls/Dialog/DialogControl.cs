using System.Windows;
using System.Windows.Controls;
using MangoWidgets.Common;
using MangoWidgets.Interfaces;

namespace MangoWidgets.Controls;

public class DialogControl:UserControl,IDialogControl
{
    public bool? DialogResult
    {
        get { return (bool?)GetValue(DialogResultProperty); }
        set { SetValue(DialogResultProperty, value); }
    }
    public static readonly DependencyProperty DialogResultProperty =
        DependencyProperty.Register("DialogResult", typeof(bool?), typeof(DialogControl), new PropertyMetadata(null, OnDialogResultChangedCallback));
    private static void OnDialogResultChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DialogControl ctrl)
        {
            _ = bool.TryParse(e.NewValue.ToString(), out bool result);
            ctrl.Close(result);
        }
    }

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