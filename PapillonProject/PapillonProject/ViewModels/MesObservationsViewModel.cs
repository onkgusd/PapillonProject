using PapillonProject.Models;
using PapillonProject.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PapillonProject.ViewModels
{
    public class MesObservationsViewModel : BaseViewModel
    {
        private MonObservation _selectedItem;

        public ObservableCollection<MonObservation> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<MonObservation> ItemTapped { get; }

        public MesObservationsViewModel()
        {
            Title = "Mes observations";

            Items = new ObservableCollection<MonObservation>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<MonObservation>(OnItemSelected);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await ObservationService.GetMesObservations();

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
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public MonObservation SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(MonObservation item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TypePapillonDetailPage)}?{nameof(TypePapillonDetailViewModel.ItemId)}={item.Papillon.Id}");
        }
    }
}