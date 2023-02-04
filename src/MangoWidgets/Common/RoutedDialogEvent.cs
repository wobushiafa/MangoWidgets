using System.Windows;
using MangoWidgets.Controls;

namespace MangoWidgets.Common;

public delegate void RoutedDialogHostEvent(IDialogHost sender,RoutedEventArgs e);

public delegate void RoutedDialogControlCloseEvent(IDialogControl sender, bool? result);