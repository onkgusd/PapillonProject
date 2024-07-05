using Android.Systems;
using PapillonProject.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using DeviceInfo = Xamarin.Essentials.DeviceInfo;

namespace PapillonProject.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        IObservationService _observationService;
        public ILocalisationService _localisationService;

        private bool _allUser;
        public bool AllUsers
        {
            get { return _allUser; }
            set
            {
                SetProperty(ref _allUser, value);
                LoadPins();
            }
        }

        public ObservableCollection<Pin> Observations { get; }

        public MapViewModel()
        {
            _observationService = DependencyService.Get<IObservationService>();
            _localisationService = DependencyService.Get<ILocalisationService>();
            Observations = new ObservableCollection<Pin>();

            Title = "Carte des observations";
        }

        public async void SetLocalisation(Xamarin.Forms.Maps.Map map)
        {
            try
            {
                var location = await (Geolocation.GetLastKnownLocationAsync() ?? Geolocation.GetLocationAsync());

                if (location != null)
                    map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, location.Latitude, location.Longitude));
            }
            catch
            {
                Console.WriteLine("Impossible de récupération la localisation actuelle.");
            }
        }

        public async void LoadPins()
        {
            Observations.Clear();

            if (_allUser)
            {
                var listPays = (await _localisationService.GetPays()).ToList();

                listPays.ForEach(
                    async pays =>
                    {
                        (await _observationService.GetObservationsByPays(pays))
                        .GroupBy(obs => obs.LatLng)
                        .ForEach(
                            obs =>
                            {
                                // Obligé de "remplacer" le . par une virgule pour éviter un plantage en java
                                var position = GetPosition(obs.Key);

                                string lib = string.Join(", ", obs.GroupBy(o => new { o.Papillon, o.User })
                                                                  .Select(o => $"{o.FirstOrDefault().Papillon.Nom} ({o.FirstOrDefault().User})")
                                                                  .ToArray());

                                bool withCurrentUser = obs.Count(o => o.User == LoginService.GetCurrentLogin()) > 0;

                                Observations.Add(new Pin
                                {
                                    Label = lib,
                                    Type = PinType.Place,
                                    Position = position,
                                });
                            });
                    });
            }
            else
            {
                var mesObservations = await _observationService.GetMesObservations();

                mesObservations.GroupBy(
                        obs => obs.Coordinates
                    ).ForEach(obs =>
                        {
                            // Obligé de "remplacer" le . par une virgule pour éviter un plantage en java
                            var position = GetPosition(obs.Key);

                            string lib = string.Join(", ", obs.Select(o => o.Papillon.Nom).ToArray());

                            Observations.Add(new Pin
                            {
                                Label = lib,
                                Type = PinType.Place,
                                Position = position
                            });
                        });
            }

        }

        private Position GetPosition(string coordinates)
        {
            double latitude;
            double longitude;

            if (Device.RuntimePlatform == Device.Android && DeviceInfo.DeviceType == DeviceType.Physical)
            {
                // Obligé de "remplacer" le . par une virgule pour éviter un plantage en java dans le cas d'une device physique Android
                latitude = Double.Parse(coordinates.Split(',')[0].Replace(".", ","));
                longitude = Double.Parse(coordinates.Split(',')[1].Replace(".", ","));
            }
            else
            {
                latitude = Double.Parse(coordinates.Split(',')[0]);
                longitude = Double.Parse(coordinates.Split(',')[1]);
            }

            return new Position (latitude, longitude);
        }

        public void OnAppearing()
        {
            LoadPins();
        }
    }
}
