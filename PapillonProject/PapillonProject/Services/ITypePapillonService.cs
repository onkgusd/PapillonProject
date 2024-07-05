using PapillonProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PapillonProject.Services
{
    public interface ITypePapillonService
    {
        TypePapillon GetTypePapillon(int id);
        Task<IEnumerable<TypePapillon>> GetTypePapillons(bool forceRefresh = false);
    }
}
