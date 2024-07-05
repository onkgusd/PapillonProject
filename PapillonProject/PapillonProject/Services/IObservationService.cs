using PapillonProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PapillonProject.Services
{
    public interface IObservationService
    {
        Task<bool> Add(int papillon, int compte);
        Task<IEnumerable<MonObservation>> GetMesObservations();
        Task<IEnumerable<Observation>> GetObservationsByPays(string country);
        Task<IEnumerable<Observation>> GetObservationsByVille(string city);
    }
}
