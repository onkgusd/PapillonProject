using PapillonProject.Services;
using PapillonProject.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PapillonProject
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<TypePapillonService>();
            DependencyService.Register<RestClientService>();
            DependencyService.Register<LoginService>();
            DependencyService.Register<ObservationService>();
            DependencyService.RegisterSingleton<LocalisationService>(new LocalisationService());

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
