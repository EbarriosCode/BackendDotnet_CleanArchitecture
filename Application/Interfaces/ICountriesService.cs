using Application.Helpers;
using Countries.Common.Classes;
using Countries.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICountriesService
    {
        Task<PagedList<Country>> GetCountries(CountryParameters parameters);
        Country GetCountry(int id);
        Task<Country> CreateCountryAsync(Country country);
        Task<Country> UpdateCountryAsync(Country country);
        Task<int> DeleteCountryAsync(Country country);
    }
}
