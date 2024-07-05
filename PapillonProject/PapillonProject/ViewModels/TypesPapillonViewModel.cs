using PapillonProject.Models;
using PapillonProject.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PapillonProject.ViewModels
{
    public class TypesPapillonViewModel : BaseViewModel
    {
        private TypePapillon _selectedItem;

        public ObservableCollection<TypePapillon> Items { get; }
        public Command<TypePapillon> ItemTapped { get; }

        public TypesPapillonViewModel()
        {
            Title = "Parcourir les types de papillons";
            Items = new ObservableCollection<TypePapillon>();
            ItemTapped = new Command<TypePapillon>(OnItemSelected);
        }

        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await TypePapillonService.GetTypePapillons(true);

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

        public async Task OnAppearing()
        {
            IsBusy = true;
            await LoadItems();
            SelectedItem = null;
        }

        public TypePapillon SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public async Task AddObservation(int idTypePapillon, int nbSpecimen)
        {
            IsBusy = true;
            await ObservationService.Add(idTypePapillon, nbSpecimen);
            IsBusy = false;
        }

        async void OnItemSelected(TypePapillon item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TypePapillonDetailPage)}?{nameof(TypePapillonDetailViewModel.ItemId)}={item.Id}");
        }
    }
}