using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MangoWidgets.Common;

namespace MangoWidgets.Controls;

public class DialogHost:ContentControl,IDialogHost
{
    private TaskCompletionSource<bool?>?_tsc = null;

    public bool IsShown
    {
        get => (bool)GetValue(IsShownProperty);
        protected set => SetValue(IsShownProperty, value);
    }

    public double DialogWidth
    {
        get => (double)GetValue(DialogWidthProperty);
        set => SetValue(DialogWidthProperty,value);
    }

    public double DialogHeight
    {
        get=>(double)GetValue(DialogHeightProperty); 
        set=>SetValue(DialogHeightProperty,value);
    }
    
    public event RoutedDialogHostEvent? Opened
    {
        add => AddHandler(OpenedEvent, value);
        remove => RemoveHandler(OpenedEvent, value);
    }
    
    public event RoutedDialogHostEvent? Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }
    
    #region 依赖属性
    public static readonly DependencyProperty IsShownProperty = DependencyProperty.Register(
        nameof(IsShown), typeof(bool), typeof(DialogHost), new PropertyMetadata(false,OnIsShownChange));

    public static readonly DependencyProperty DialogWidthProperty = DependencyProperty.Register(nameof(DialogWidth),
        typeof(double),
        typeof(DialogHost),
        new PropertyMetadata(420d));
    
    public static readonly DependencyProperty DialogHeightProperty = DependencyProperty.Register(nameof(DialogHeight),
        typeof(double),
        typeof(DialogHost),
        new PropertyMetadata(200d));
    #endregion
    
    #region 路由事件

    public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent(nameof(Opened),
        RoutingStrategy.Bubble,
        typeof(RoutedDialogEvent),
        typeof(DialogHost));

    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(nameof(Closed),
        RoutingStrategy.Bubble,
        typeof(RoutedDialogEvent),
        typeof(DialogHost));
    
    #endregion

    private static void OnIsShownChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DialogHost dialogHost)
            return;
        if(dialogHost.IsShown)
            dialogHost.OnOpened();
        else 
            dialogHost.OnClosed();
    }

    protected virtual void OnOpened()
    {
        var newEvent = new RoutedEventArgs(OpenedEvent, this);
        RaiseEvent(newEvent);
    }

    protected virtual void OnClosed()
    {
        var newEvent = new RoutedEventArgs(ClosedEvent, this);
        RaiseEvent(newEvent);
    }
    
    public bool Show()
    {
        if (IsShown)
            return false;
        IsShown = true;
        return IsShown;
    }

    public bool Hide()
    {
        if (!IsShown)
            return false;
        IsShown = false;
        return true;
    }

    public Task<bool?> ShowDialogAsync(IDialogControl content)
    {
        Content = content;
        content.Closed += OnContentClosed;
        IsShown = true;
        _tsc = new TaskCompletionSource<bool?>();
        return _tsc.Task;
    }

    private void OnContentClosed(IDialogControl sender, bool? result)
    {
        _tsc?.TrySetResult(result);
        sender.Closed -= OnContentClosed;
        IsShown = false;
        Content = null;
    }
}