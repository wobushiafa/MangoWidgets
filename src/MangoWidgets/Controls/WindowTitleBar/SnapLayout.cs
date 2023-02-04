using System;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows;
using MangoWidgets.Dpi;

namespace MangoWidgets.Controls;

/// <summary>
/// Brings the Snap Layout functionality from Windows 11 to a custom <see cref="Controls.TitleBar"/>.
/// </summary>
internal sealed class SnapLayout
{
    private readonly SnapLayoutButton _maximizeButton;

    private readonly SnapLayoutButton _restoreButton;


    /// <summary>
    /// Currently active hover color.
    /// </summary>
    private SolidColorBrush _currentHoverColor = Brushes.Transparent;

    /// <summary>
    /// Default background.
    /// </summary>
    public SolidColorBrush DefaultButtonBackground { get; set; } = Brushes.Transparent;

    /// <summary>
    /// Hover background when light theme.
    /// </summary>
    public SolidColorBrush HoverColorLight = Brushes.Transparent;

    /// <summary>
    /// Hover background when dark theme.
    /// </summary>
    public SolidColorBrush HoverColorDark = Brushes.Transparent;

    /// <summary>
    /// Creates new instance.
    /// </summary>
    private SnapLayout(Window window, Controls.Button maximizeButton, MangoWidgets.Controls.Button restoreButton)
    {
        if (window == null)
            return;

        var windowHandle = new WindowInteropHelper(window).Handle;

        if (windowHandle == IntPtr.Zero)
            return;

        var windowDpi = DpiHelper.GetWindowDpi(windowHandle);

        _maximizeButton = new SnapLayoutButton(maximizeButton, TitleBarButton.Maximize, windowDpi.DpiScaleX);
        _restoreButton = new SnapLayoutButton(restoreButton, TitleBarButton.Restore, windowDpi.DpiScaleX);

        var hwnd = (HwndSource)PresentationSource.FromVisual(maximizeButton);

        if (hwnd != null)
            hwnd.AddHook(HwndSourceHook);
    }

    /// <summary>
    /// Determines whether the snap layout is supported.
    /// </summary>
    public static bool IsSupported()
    {
        return Win32.Utilities.IsOSWindows11OrNewer;
    }

    /// <summary>
    /// Registers the snap layout for provided buttons and window.
    /// </summary>
    public static SnapLayout Register(Window window, MangoWidgets.Controls.Button maximizeButton, MangoWidgets.Controls.Button restoreButton)
    {
        return new SnapLayout(window, maximizeButton, restoreButton);
    }

    /// <summary>
    /// Represents the method that handles Win32 window messages.
    /// </summary>
    /// <param name="hWnd">The window handle.</param>
    /// <param name="uMsg">The message ID.</param>
    /// <param name="wParam">The message's wParam value.</param>
    /// <param name="lParam">The message's lParam value.</param>
    /// <param name="handled">A value that indicates whether the message was handled. Set the value to <see langword="true"/> if the message was handled; otherwise, <see langword="false"/>.</param>
    /// <returns>The appropriate return value depends on the particular message. See the message documentation details for the Win32 message being handled.</returns>
    private IntPtr HwndSourceHook(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        var mouseNotification = (User32.WM)uMsg;

        System.Diagnostics.Debug.WriteLine($"INFO | MESSAGE: {mouseNotification}, OVER MAX: {_maximizeButton.IsMouseOver(lParam)}, OVER REST: {_restoreButton.IsMouseOver(lParam)}");

        switch (mouseNotification)
        {
            case User32.WM.MOVE:
                // Adjust [Size] of the buttons if the DPI is changed
                break;

            // Mouse leaves the window
            case User32.WM.NCMOUSELEAVE:
                _maximizeButton.RemoveHover(DefaultButtonBackground);
                _restoreButton.RemoveHover(DefaultButtonBackground);

                break;

            // Left button clicked down
            case User32.WM.NCLBUTTONDOWN:
                if (_maximizeButton.IsMouseOver(lParam))
                {
                    _maximizeButton.IsClickedDown = true;

                    handled = true;
                }

                if (_restoreButton.IsMouseOver(lParam))
                {
                    _restoreButton.IsClickedDown = true;

                    handled = true;
                }

                break;

            // Left button clicked up
            case User32.WM.NCLBUTTONUP:
                if (_maximizeButton.IsClickedDown && _maximizeButton.IsMouseOver(lParam))
                {
                    _maximizeButton.InvokeClick();

                    handled = true;

                    break;
                }

                if (_restoreButton.IsClickedDown && _restoreButton.IsMouseOver(lParam))
                {
                    _restoreButton.InvokeClick();

                    handled = true;
                }

                break;

            // Hit test, for determining whether the mouse cursor is over one of the buttons
            case User32.WM.NCHITTEST:
                if (_maximizeButton.IsMouseOver(lParam))
                {
                    _maximizeButton.Hover(_currentHoverColor);

                    handled = true;

                    return new IntPtr((int)User32.WM_NCHITTEST.HTMAXBUTTON);
                }

                _maximizeButton.RemoveHover(DefaultButtonBackground);

                if (_restoreButton.IsMouseOver(lParam))
                {
                    _restoreButton.Hover(_currentHoverColor);

                    handled = true;

                    return new IntPtr((int)User32.WM_NCHITTEST.HTMAXBUTTON);
                }

                _restoreButton.RemoveHover(DefaultButtonBackground);

                return IntPtr.Zero;
        }

        return IntPtr.Zero;
    }
}
