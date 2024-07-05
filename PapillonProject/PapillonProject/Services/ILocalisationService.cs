using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapillonProject.Services
{
    public interface ILocalisationService
    {
        Task<IEnumerable<string>> GetPays();
        Task<IEnumerable<string>> GetVilles();
    }
}