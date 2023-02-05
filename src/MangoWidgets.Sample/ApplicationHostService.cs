using MangoWidgets.Sample.Views;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MangoWidgets.Sample;

class ApplicationHostService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var view = App.Resolve<MainView>();
        view.Show();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
