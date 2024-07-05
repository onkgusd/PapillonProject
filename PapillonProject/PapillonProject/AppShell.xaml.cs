using PapillonProject.Services;
using PapillonProject.ViewModels;
using PapillonProject.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PapillonProject
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TypePapillonDetailPage), typeof(TypePapillonDetailPage));
            Routing.RegisterRoute(nameof(TypesPapillonPage), typeof(TypesPapillonPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
