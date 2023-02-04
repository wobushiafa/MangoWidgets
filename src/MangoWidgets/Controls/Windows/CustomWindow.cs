﻿using System;
using System.Windows.Interop;
using System.Windows.Shell;
using System.Windows;
using MangoWidgets.Appearance;

namespace MangoWidgets.Controls;

public class CustomWindow : Window
{
    #region Private properties

    private WindowInteropHelper? _interopHelper = null;

    private HwndSource? _hwndSource = null;

    #endregion Private properties

    #region Protected properties

    /// <summary>
    /// Contains helper for accessing this window handle.
    /// </summary>
    protected WindowInteropHelper InteropHelper
    {
        get => _interopHelper ??= new WindowInteropHelper(this);
    }

    /// <summary>
    /// Container WPF presenter handle.
    /// </summary>
    protected HwndSource HwndSource
    {
        get => _hwndSource ??= HwndSource.FromHwnd(InteropHelper.Handle);
    }

    #endregion Protected properties

    #region Public properties

    /// <summary>
    /// Property for <see cref="ExtendsContentIntoTitleBar"/>.
    /// </summary>
    public static readonly DependencyProperty ExtendsContentIntoTitleBarProperty = DependencyProperty.Register(
        nameof(ExtendsContentIntoTitleBar),
        typeof(bool), typeof(CustomWindow), new PropertyMetadata(false, OnExtendsContentIntoTitleBarChanged));

    /// <summary>
    /// Property for <see cref="WindowCornerPreference"/>.
    /// </summary>
    public static readonly DependencyProperty WindowCornerPreferenceProperty = DependencyProperty.Register(
        nameof(WindowCornerPreference),
        typeof(WindowCornerPreference), typeof(CustomWindow),
        new PropertyMetadata(WindowCornerPreference.Round, OnCornerPreferenceChanged));

    /// <summary>
    /// Property for <see cref="WindowBackdropType"/>.
    /// </summary>
    public static readonly DependencyProperty WindowBackdropTypeProperty = DependencyProperty.Register(
        nameof(WindowBackdropType),
        typeof(BackgroundType), typeof(CustomWindow), new PropertyMetadata(BackgroundType.None, OnBackdropTypeChanged));

    /// <summary>
    /// Gets or sets a value determining whether the <see cref="Window"/> content should be extended into title bar.
    /// </summary>
    public bool ExtendsContentIntoTitleBar
    {
        get => (bool)GetValue(ExtendsContentIntoTitleBarProperty);
        set => SetValue(ExtendsContentIntoTitleBarProperty, value);
    }

    /// <summary>
    /// Gets or sets a value determining corner preference for current <see cref="Window"/>.
    /// </summary>
    public WindowCornerPreference WindowCornerPreference
    {
        get => (WindowCornerPreference)GetValue(WindowCornerPreferenceProperty);
        set => SetValue(WindowCornerPreferenceProperty, value);
    }

    /// <summary>
    /// Gets or sets a value determining preferred backdrop type for current <see cref="Window"/>.
    /// </summary>
    public BackgroundType WindowBackdropType
    {
        get => (BackgroundType)GetValue(WindowBackdropTypeProperty);
        set => SetValue(WindowBackdropTypeProperty, value);
    }

    #endregion Public properties

    #region Constructors

    /// <summary>
    /// Creates new instance and sets default style.
    /// </summary>
    public CustomWindow()
    {
        SetResourceReference(StyleProperty, typeof(CustomWindow));
    }

    /// <summary>
    /// Overrides default properties.
    /// </summary>
    static CustomWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
        //HeightProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(600d));
        //WidthProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(1100d));
        //MinHeightProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(320d));
        //MinWidthProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(460d));
        //WindowStyleProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(WindowStyle.SingleBorderWindow));
        //AllowsTransparencyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(false));
    }

    #endregion Constructors

    #region Protected methods

    /// <inheritdoc />
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        ApplyWindowCornerPreferenceInternal(WindowCornerPreference);
        ApplyWindowBackdropInternal(WindowBackdropType);
        ExtendsContentIntoTitleBarInternal(ExtendsContentIntoTitleBar);
    }

    /// <summary>
    /// This virtual method is called when <see cref="ExtendsContentIntoTitleBar"/> is changed.
    /// </summary>
    protected virtual void OnExtendsContentIntoTitleBarChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
            return;

        ExtendsContentIntoTitleBarInternal(newValue);
    }

    /// <summary>
    /// Private <see cref="ExtendsContentIntoTitleBarProperty"/> property callback.
    /// </summary>
    private static void OnExtendsContentIntoTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not CustomWindow window)
            return;

        window.OnExtendsContentIntoTitleBarChanged((bool)e.OldValue, (bool)e.NewValue);
    }

    /// <summary>
    /// This virtual method is called when <see cref="WindowCornerPreference"/> is changed.
    /// </summary>
    protected virtual void OnCornerPreferenceChanged(WindowCornerPreference oldValue, WindowCornerPreference newValue)
    {
        if (oldValue == newValue)
            return;

        ApplyWindowCornerPreferenceInternal(newValue);
    }

    /// <summary>
    /// Private <see cref="WindowCornerPreference"/> property callback.
    /// </summary>
    private static void OnCornerPreferenceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not CustomWindow window)
            return;

        window.OnCornerPreferenceChanged((WindowCornerPreference)e.OldValue, (WindowCornerPreference)e.NewValue);
    }

    /// <summary>
    /// This virtual method is called when <see cref="WindowBackdropType"/> is changed.
    /// </summary>
    protected virtual void OnBackdropTypeChanged(BackgroundType oldValue, BackgroundType newValue)
    {
        if (oldValue == newValue)
            return;

        ApplyWindowBackdropInternal(newValue);
    }

    /// <summary>
    /// Private <see cref="WindowBackdropType"/> property callback.
    /// </summary>
    private static void OnBackdropTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not CustomWindow window)
            return;

        window.OnBackdropTypeChanged((BackgroundType)e.OldValue, (BackgroundType)e.NewValue);
    }

    #endregion Protected methods

    #region Private methods

    private void ApplyWindowCornerPreferenceInternal(WindowCornerPreference cornerPreference)
    {
        if (InteropHelper.Handle == IntPtr.Zero)
            return;

        UnsafeNativeMethods.ApplyWindowCornerPreference(InteropHelper.Handle, cornerPreference);
    }

    private void ApplyWindowBackdropInternal(BackgroundType backdropType)
    {
        if (backdropType == BackgroundType.Unknown || backdropType == BackgroundType.None)
        {
            // Removes backdrop and tries to restore default background
            Appearance.Background.Remove(this);

            return;
        }

        if (!ExtendsContentIntoTitleBar)
            throw new InvalidOperationException($"Cannot apply backdrop effect if {nameof(ExtendsContentIntoTitleBar)} is false.");

        if (backdropType == BackgroundType.Acrylic && !Win32.Utilities.IsOSWindows11Insider1OrNewer &&
            !AllowsTransparency)
            throw new InvalidOperationException("In the Windows system below 22523 build, the Acrylic effect cannot be applied if the Window does not have AllowsTransparency set to True.");

        // Set backdrop effect and remove background from window and it's composition area
        Appearance.Background.Apply(this, WindowBackdropType);
    }

    private void ExtendsContentIntoTitleBarInternal(bool extendContent)
    {
        if (!extendContent)
        {
            WindowChrome.SetWindowChrome(this, null);

            return;
        }

        UnsafeNativeMethods.RemoveWindowTitlebar(this);

        // TODO: Rewrite custom window chrome

        WindowChrome.SetWindowChrome(this,
            new WindowChrome
            {
                CaptionHeight = 1,
                CornerRadius = new CornerRadius(4),
                GlassFrameThickness = new Thickness(-1),
                ResizeBorderThickness = this.ResizeMode == ResizeMode.NoResize ? new Thickness(0) : new Thickness(4),
                UseAeroCaptionButtons = false
            });
    }

    #endregion Private methods
}