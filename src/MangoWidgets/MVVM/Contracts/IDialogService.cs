using System.Threading.Tasks;
using MangoWidgets.Controls;

namespace MangoWidgets.MVVM.Contracts;

public interface IDialogService
{
    /// <summary>
    /// 设置<see cref="IDialogHost"/>控件
    /// </summary>
    /// <param name="dialogHost"></param>
    void SetDialogHost(IDialogHost dialogHost);

    /// <summary>
    /// 获取<see cref="IDialogHost"/>控件
    /// </summary>
    /// <returns></returns>
    IDialogHost GetDialogHost();

    /// <summary>
    /// 显示<see cref="IDialogHost"/> 并等待content控件退出
    /// </summary>
    /// <returns></returns>
    Task<bool?> ShowDialogAsync(IDialogControl content);
}

public interface IDialogService<T> : IDialogService
{
    
}