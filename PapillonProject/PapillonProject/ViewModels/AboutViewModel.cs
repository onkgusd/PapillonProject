using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PapillonProject.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "A propos";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.u-picardie.fr/catalogue-formations/FI/co/master-miage.html"));
        }

        public ICommand OpenWebCommand { get; }
    }
}