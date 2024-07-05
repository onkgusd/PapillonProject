using PapillonProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.App.Assist.AssistStructure;

namespace PapillonProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObservationsPage : ContentPage
    {
        ObservationsViewModel _viewModel;

        public ObservationsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ObservationsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}