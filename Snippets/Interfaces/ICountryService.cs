using Snippets.Entities;

namespace Snippets.Interfaces
{
    public interface ICountryService
    {
        Task<List<Root>> GetCountry();
    }
}
