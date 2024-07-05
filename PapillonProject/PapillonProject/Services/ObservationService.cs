using PapillonProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PapillonProject.Services
{
    public class ObservationService : IObservationService
    {
        private IRestClientService restClientService = DependencyService.Get<IRestClientService>();
        private ITypePapillonService papillonService = DependencyService.Get<ITypePapillonService>();

        public async Task<bool> Add(int papillon, int compte)
        {
            Location location;

            try
            {
                location = await Geolocation.GetLocationAsync();
            }
            catch
            {
                Console.WriteLine("Impossible de récupérer la localisation.");

                return false;
            }

            if (location != null)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            }

            return await restClientService.Post($"observation/{papillon}/{compte}/{location.Latitude},{location.Longitude}");
        }

        public async Task<IEnumerable<MonObservation>> GetMesObservations()
        {
            var listObservation = await restClientService.GetItems<MonObservation>($"mes-observations");

            listObservation.ForEach(
                obs => obs.Papillon = papillonService.GetTypePapillon(Int32.Parse(obs.ButterflyId)));

            return listObservation;
        }

        public async Task<IEnumerable<Observation>> GetObservationsByPays(string country)
        {
            return await LoadObservation($"liste-par-pays/{country}");
        }

        public async Task<IEnumerable<Observation>> GetObservationsByVille(string city)
        {
            return await LoadObservation($"liste-par-ville/{city}");
        }

        private async Task<IEnumerable<Observation>> LoadObservation(string endpoint)
        {
            var listObservation = await restClientService.GetItems<Observation>(endpoint);

            listObservation.ForEach(
                obs => {
                    obs.Papillon = papillonService.GetTypePapillon(Int32.Parse(obs.TypePapillon));
                    obs.Date = DateTimeOffset.FromUnixTimeSeconds(Int32.Parse(obs.Timestamp)).UtcDateTime;
                });


            return listObservation;
        }
    }
}
