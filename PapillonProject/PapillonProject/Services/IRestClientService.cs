using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapillonProject.Services
{
    public interface IRestClientService
    {
        Task<List<T>> GetItems<T>(string endpoint);
        Task<object> GetItem(string endpoint);
        Task<bool> PostItem<T>(T item, string endpoint);
        Task<string> GetRawResponse(string endpoint);
        Task<bool> Post(string endpoint);
    }
}