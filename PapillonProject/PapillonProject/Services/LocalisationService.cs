using PapillonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PapillonProject.Services
{
    public class LocalisationService : ILocalisationService
    {
        IRestClientService _restClientService;

        List<string> _ville;
        List<string> _pays;

        public LocalisationService()
        {
            _restClientService = DependencyService.Get<IRestClientService>();
        }

        public async Task<IEnumerable<string>> GetVilles()
        {
            await LoadLocalisation();

            return _ville;
        }

        public async Task<IEnumerable<string>> GetPays()
        {
            await LoadLocalisation();

            return _pays.Distinct();
        }

        private async Task<bool> LoadLocalisation()
        {
            _ville = new List<string>();
            _pays = new List<string>();

            var listLocalisation = await _restClientService.GetItems<Localisation>("liste-villes");

            listLocalisation.ForEach(localisation =>
            {
                _ville.Add(localisation.City);
                _pays.Add(localisation.Country);
            });

            return true;
        }
    }
}
