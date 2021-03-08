using Application.Helpers;
using Application.Interfaces;
using Countries.Common.Classes;
using Countries.Infra.Data.Repositories.Custom;
using Countries.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _repository;
        private string includeProperties = string.Empty;
        public CountriesService(ICountriesRepository repository) => this._repository = repository;

        public async Task<PagedList<Country>> GetCountries(CountryParameters parameters)
        {            
            try
            {                
                var result = new List<Country>();

                if (parameters != null)
                {
                    #region Filter by Name or Alpha2 Code and Orderby Numeric Code
                    
                    if (!string.IsNullOrEmpty(parameters.Name) && parameters.OrderByNumericCode)
                        result = this._repository.Get(x => x.Name.Contains(parameters.Name), x => x.OrderBy(y => y.NumericCode), includeProperties) as List<Country>;

                    else if (!string.IsNullOrEmpty(parameters.Name) && !parameters.OrderByNumericCode)
                        result = this._repository.Get(x => x.Name.Contains(parameters.Name), null, includeProperties) as List<Country>;

                    else if (!string.IsNullOrEmpty(parameters.Alpha2) && parameters.OrderByNumericCode)
                        result = this._repository.Get(x => x.Alpha_2.Contains(parameters.Alpha2), x => x.OrderBy(y => y.NumericCode), includeProperties) as List<Country>;

                    else if (!string.IsNullOrEmpty(parameters.Alpha2) && !parameters.OrderByNumericCode)
                        result = this._repository.Get(x => x.Alpha_2.Contains(parameters.Alpha2), null, includeProperties) as List<Country>;

                    else if (!string.IsNullOrEmpty(parameters.Name) && !string.IsNullOrEmpty(parameters.Alpha2) && parameters.OrderByNumericCode)
                        result = this._repository.Get(x => x.Name.Contains(parameters.Name) && x.Alpha_2.Contains(parameters.Alpha2), x => x.OrderBy(y => y.NumericCode), includeProperties) as List<Country>;

                    else if (!string.IsNullOrEmpty(parameters.Name) && !string.IsNullOrEmpty(parameters.Alpha2) && !parameters.OrderByNumericCode)
                        result = this._repository.Get(x => x.Name.Contains(parameters.Name) && x.Alpha_2.Contains(parameters.Alpha2), null, includeProperties) as List<Country>;

                    else
                        result = this._repository.Get(null, null, includeProperties) as List<Country>;
                    #endregion
                }

                return await Task.Run(() => PagedList<Country>.ToPagedList(result, parameters.PageNumber, parameters.PageSize));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            Country created = new();

            try
            {
                if (country != null)
                {
                    created = await this._repository.CreateAsync(country);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return created;
        }

        public Country GetCountry(int id)
        {
            Country result = null;
            includeProperties = "Subdivisions";

            try
            {
                List<Country> country = this._repository.Get(x => x.CountryID == id, null, includeProperties) as List<Country>;

                if (country.Count > 0)
                    result = country[0];
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            Country updated = new();

            try
            {
                if(country != null)
                {
                    updated = await this._repository.UpdateAsync(country, country.CountryID);                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return updated;
        }

        public async Task<int> DeleteCountryAsync(Country country)
        {
            int deleted = 0;

            try
            {
                if (country != null)
                    deleted = await this._repository.DeleteAsync(country);                
            }
            catch (Exception)
            {
                throw;
            }

            return deleted;
        }
    }
}
