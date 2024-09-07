using AspNetCoreBlazorTest.Models;
using Refit;
using System.Collections.ObjectModel;

namespace AspNetCoreBlazorTest.Interfaces
{
    public interface IJsonApi
    {
        [Get("/users")]
        Task<ObservableCollection<Admin>> GetAdmins();
    }
}
