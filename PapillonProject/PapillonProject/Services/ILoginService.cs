using System.Threading.Tasks;

namespace PapillonProject.Services
{
    public interface ILoginService
    {
        string GetCurrentLogin();
        Task<bool> Logon(string login);
    }
}