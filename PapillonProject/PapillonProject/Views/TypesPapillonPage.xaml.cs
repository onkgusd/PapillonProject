using Android.Telephony.Euicc;
using PapillonProject.Models;
using PapillonProject.ViewModels;
using PapillonProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PapillonProject.Views
{
    public partial class TypesPapillonPage : ContentPage
    {
        TypesPapillonViewModel _viewModel;

        public TypesPapillonPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TypesPapillonViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            TypePapillon typePapillon = ((ImageButton)sender).BindingContext as TypePapillon;

            int nbSpecimen;

            var input = await DisplayPromptAsync($"Observation - {typePapillon.Nom}", "Indiquez le nombre de specimens observés", "Enregister mon observation", "Annuler", "Nombre de specimen", 10, Keyboard.Numeric, "1");

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (Int32.TryParse(input, out nbSpecimen)){
                await _viewModel.AddObservation(typePapillon.Id, nbSpecimen);
            }
            else
            {
                await DisplayAlert("Saisie incorrecte", "Chiffre entier uniquement.", "C'est compris !");
                ImageButton_Clicked(sender, e);
            }
        }
    }
}