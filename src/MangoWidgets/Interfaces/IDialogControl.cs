using MangoWidgets.Common;

namespace MangoWidgets.Interfaces;

/// <summary>
/// Dialog内容控件
/// </summary>
public interface IDialogControl
{
    /// <summary>
    /// <see cref="IDialogControl"/> 关闭事件,<see cref="IDialogHost"/>通过此事件的触发来完成ShowDialogAsync方法的执行
    /// </summary>
    public event RoutedDialogControlCloseEvent Closed;

    /// <summary>
    /// <see cref="IDialogControl"/> DialogResult
    /// </summary>
    public bool? DialogResult { get; set; }
}

/// <summary>
/// 更好的注入View,使ViewModel不需要引用View的命名空间
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDialogControl<out T> : IDialogControl where T : class
{
    T ViewModel { get; }
}