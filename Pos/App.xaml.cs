using DataLayer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Pos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services = ConfigureServices();

            base.OnStartup(e);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
