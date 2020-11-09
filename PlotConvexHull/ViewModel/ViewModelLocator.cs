using Microsoft.Extensions.DependencyInjection;

namespace PlotConvexHull.ViewModel
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}
