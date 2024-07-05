using PapillonProject.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PapillonProject.Views
{
    public partial class MesObservationsPage : ContentPage
    {
        MesObservationsViewModel _viewModel;

        public MesObservationsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MesObservationsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}