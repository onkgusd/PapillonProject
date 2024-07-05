using Android.Provider;
using Newtonsoft.Json;
using PapillonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PapillonProject.Services
{
    public class TypePapillonService : ITypePapillonService
    {
        private IRestClientService _restClientService = DependencyService.Get<IRestClientService>();
        private IEnumerable<TypePapillon> _listTypePapillon;

        public TypePapillon GetTypePapillon(int id) => _listTypePapillon.FirstOrDefault(t => t.Id == id);

        public async Task<IEnumerable<TypePapillon>> GetTypePapillons(bool forceRefresh = false)
        {
            if (_listTypePapillon == null)
                _listTypePapillon = await LoadListPapillon();

            return _listTypePapillon;
        }

        private async Task<IEnumerable<TypePapillon>> LoadListPapillon()
        {
            var response = await _restClientService.GetRawResponse("liste-papillons");
            var items = JsonConvert.DeserializeObject<Dictionary<int, TypePapillon>>(response);
            var typePapillonsList = new List<TypePapillon>();

            foreach (KeyValuePair<int, TypePapillon> item in items)
            {
                item.Value.Id = item.Key;
                item.Value.Image = new Uri(new Uri("https://daviddurand.info/D228/papillons/"), item.Value.Image);

                typePapillonsList.Add(item.Value);
            }

            return typePapillonsList;
        }
    }
}
