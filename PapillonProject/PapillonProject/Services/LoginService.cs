using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PapillonProject.Services
{
    public class LoginService : ILoginService
    {
        private IRestClientService _restClientService = DependencyService.Get<IRestClientService>();
        private string _login;

        public async Task<bool> Logon(string login)
        {
            _login = login;
            return await _restClientService.Post(string.Concat("login/", login));
        }

        public string GetCurrentLogin()
        {
            return _login;
        }
    }
}
