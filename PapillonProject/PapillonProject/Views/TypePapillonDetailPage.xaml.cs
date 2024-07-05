using PapillonProject.ViewModels;
using System;
using Xamarin.Forms;

namespace PapillonProject.Views
{
    public partial class TypePapillonDetailPage : ContentPage
    {
        TypePapillonDetailViewModel _viewModel;

        public TypePapillonDetailPage()
        {
            InitializeComponent();

            try
            {
                BindingContext = _viewModel = new TypePapillonDetailViewModel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}