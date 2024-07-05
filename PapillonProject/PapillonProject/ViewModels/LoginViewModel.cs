using PapillonProject.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PapillonProject.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private bool _loginError;
        private bool _canLogIn;

        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                CanLogIn = !string.IsNullOrEmpty(_login);
            }
        }

        public bool LoginError
        {
            get => _loginError;
            set => SetProperty(ref _loginError, value);
        }

 
        public bool CanLogIn
        {
            get => _canLogIn;
            set => SetProperty(ref _canLogIn, value);
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            Login = Preferences.Get("login", "");
        }

        private async void OnLoginClicked(object obj)
        {
            LoginError = false;
            IsBusy = true;

            if (await LoginService.Logon(Login))
            {
                Preferences.Set("login", Login);

                Application.Current.MainPage = new AppShell();

                await Shell.Current.GoToAsync($"//{nameof(TypesPapillonPage)}");
            }
            else
            {
                LoginError = true;
            }

            IsBusy = false;
        }
    }
}
