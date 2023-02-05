using System.Threading.Tasks;
using MangoWidgets.Controls;

namespace MangoWidgets.MVVM.Contracts;

public interface IDialogService
{
    /// <summary>
    /// 设置<see cref="IDialogHost"/>控件,为了能让同一Service实现不同的View都能Dialog,所以采用泛型名称存储多个IDialogHost控件
    /// </summary>
    /// <typeparam name="T"><see cref="IDialogHost"/>所在的页面</typeparam>
    /// <param name="dialogHost"></param>
    void SetDialogHost<T>(IDialogHost dialogHost);

    /// <summary>
    /// 获取<see cref="IDialogHost"/>控件
    /// </summary>
    /// <typeparam name="T"><see cref="IDialogHost"/>所在的页面</typeparam>
    /// <returns></returns>
    IDialogHost GetDialogHost<T>();

    /// <summary>
    /// 显示<see cref="IDialogHost"/> 并等待content控件退出
    /// </summary>
    /// <typeparam name="T"><see cref="IDialogHost"/>所在的页面</typeparam>
    /// <param name="content"></param>
    /// <returns></returns>
    Task<bool?> ShowDialogAsync<T>(IDialogControl content);
}