using PapillonProject.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PapillonProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class TypePapillonDetailViewModel : BaseViewModel
    {
        private int _itemId;
        private string _nom;
        private string _texte;
        private UriImageSource _image;
        private int _nbSpecimen = 1;
        private bool _canSendObservation;

        public Command AddObservationCommand { get; }
        
        public int Id { get; set; }

        public string Texte
        {
            get => _texte;
            set => SetProperty(ref _texte, value);
        }

        public string Nom
        {
            get => _nom;
            set => SetProperty(ref _nom, value);
        }

        public UriImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        public int NbSpecimen
        {
            get => _nbSpecimen;
            set
            {
                CanSendObservation = value > 0;

                SetProperty(ref _nbSpecimen, value);
            }
        }

        public bool CanSendObservation
        {
            get => _canSendObservation;
            set => SetProperty(ref _canSendObservation, value);
        }

        public TypePapillonDetailViewModel()
        {
            AddObservationCommand = new Command(async () => await ExecuteAddObservationCommand());
        }

        private async Task ExecuteAddObservationCommand()
        {
            IsBusy = true;

            if (_nbSpecimen > 0)
            {
                await ObservationService.Add(Id, _nbSpecimen);
            }

            IsBusy = false;
        }

        public void LoadItemId(int id)
        {
            try
            {
                var item = TypePapillonService.GetTypePapillon(id);

                Id = item.Id;
                Texte = item.Texte;
                Title = item.Nom;
                Image = new UriImageSource
                        {
                            Uri = item.Image,
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(5, 0, 0, 0)
                        };
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
