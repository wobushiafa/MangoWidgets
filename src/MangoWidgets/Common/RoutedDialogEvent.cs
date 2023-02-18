using System.Windows;
using MangoWidgets.Interfaces;

namespace MangoWidgets.Common;

public delegate void RoutedDialogHostEvent(IDialogHost sender,RoutedEventArgs e);

public delegate void RoutedDialogControlCloseEvent(IDialogControl sender, bool? result);