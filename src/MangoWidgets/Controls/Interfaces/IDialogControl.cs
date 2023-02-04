using MangoWidgets.Common;

namespace MangoWidgets.Controls;

/// <summary>
/// Dialog内容控件
/// </summary>
public interface IDialogControl
{
    /// <summary>
    /// <see cref="IDialogControl"/> 关闭事件,<see cref="IDialogHost"/>通过此事件的触发来完成ShowDialogAsync方法的执行
    /// </summary>
    public event RoutedDialogControlCloseEvent Closed;
}