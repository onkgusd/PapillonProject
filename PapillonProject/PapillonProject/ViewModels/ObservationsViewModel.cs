using PapillonProject.Models;
using PapillonProject.Services;
using PapillonProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PapillonProject.ViewModels
{
    public class ObservationsViewModel : BaseViewModel
    {
        private Observation _selectedItem;
        private List<string> _villes;
        private List<string> _pays;
        private string _selectedVille;
        private string _selectedPays;
        private bool _searchByPays;
        private bool _isLoading;

        public bool SearchByPays
        {
            get
            {
                return _searchByPays;
            }
            set
            {
                if (_searchByPays != value)
                {
                    SetProperty(ref _searchByPays, value);
                    IsBusy = true;
                }
            }
        }

        public string SelectedVille
        {
            get
            {
                return _selectedVille;
            }
            set
            { 
                if (SetProperty(ref _selectedVille, value))
                {
                    OnVilleSelected();
                }
            }
        }


        public string SelectedPays
        {
            get
            {
                return _selectedPays;
            }
            set
            {
                if (SetProperty(ref _selectedPays, value))
                {
                    OnPaysSelected();
                }
            }
        }

        public List<string> Villes
        {
            get =>_villes;
            set => SetProperty(ref _villes, value);
        }
        public List<string> Pays
        {
            get => _pays;
            set => SetProperty(ref _pays, value);
        }

        public ObservableCollection<Observation> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Observation> ItemTapped { get; }

        public ILocalisationService _localisationService;

        public ObservationsViewModel()
        {
            _localisationService = DependencyService.Get<ILocalisationService>();

            Title = "Observations";

            Items = new ObservableCollection<Observation>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Observation>(OnItemSelected);
        }

        public async void LoadLocation()
        {
            Villes = (await _localisationService.GetVilles()).ToList();
            SelectedVille = Preferences.Get("LastVille", Villes.FirstOrDefault());

            Pays = (await _localisationService.GetPays()).ToList();
            SelectedPays = Preferences.Get("LastPays", Pays.FirstOrDefault());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if(_isLoading == true)
            {
                return;
            }

            _isLoading = true;
            
            IsBusy = true;
            
            Items.Clear();

            try
            {
                var items = SearchByPays ? await ObservationService.GetObservationsByPays(_selectedPays) : await ObservationService.GetObservationsByVille(_selectedVille);

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                _isLoading = false;
            }
        }

        public void OnAppearing()
        {
            SelectedItem = null;
            LoadLocation();
        }

        public Observation SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Observation item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TypePapillonDetailPage)}?{nameof(TypePapillonDetailViewModel.ItemId)}={item.TypePapillon}");
        }

        async void OnVilleSelected()
        {
            if(_selectedVille == null) return;

            Preferences.Set("LastVille", _selectedVille);

            await ExecuteLoadItemsCommand();
        }

        async void OnPaysSelected()
        {
            if (_selectedPays == null) return;

            Preferences.Set("LastPays", _selectedPays);

            await ExecuteLoadItemsCommand();
        }
    }
}
