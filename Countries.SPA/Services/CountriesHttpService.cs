using Application.DTO;
using Application.Helpers;
using Countries.Common.Classes;
using Countries.Common.Resources;
using Countries.SPA.Helpers.Paging;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Countries.SPA.Services
{
    public interface ICountriesHttpService
    {
        Task<PagingResponse<CountryDTO>> GetCountriesDTOAsync(CountryParameters parameters);
        Task<List<CountryDTO>> GetCountriesDTOAsync();
        Task<CountryDTO> GetByID(int id);
        Task<CountryDTO> Create(CountryDTO model);
        Task<CountryDTO> Update(CountryDTO model);
        Task<int> Delete(int id);
    }

    public class CountriesHttpService : ICountriesHttpService
    {
        private readonly HttpClient _client;        
        public CountriesHttpService(HttpClient client) => this._client = client;

        public async Task<PagingResponse<CountryDTO>> GetCountriesDTOAsync(CountryParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString(Literals.URI_API_COUNTRIES, queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<CountryDTO>
            {
                Items = JsonSerializer.Deserialize<List<CountryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;            
        }

        public async Task<List<CountryDTO>> GetCountriesDTOAsync()
        {
            var result = await this._client.GetFromJsonAsync<List<CountryDTO>>(Literals.URI_API_COUNTRIES);
            return await Task.FromResult(result);
        }

        public async Task<CountryDTO> GetByID(int id)
        {
            var response = await this._client.GetFromJsonAsync<CountryDTO>($"{Literals.URI_API_COUNTRIES}/{id}");
            return response;
        }

        public async Task<CountryDTO> Create(CountryDTO model)
        {
            // Insertar en el api
            var response = await this._client.PostAsJsonAsync(Literals.URI_API_COUNTRIES, model);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                model = await response.Content.ReadFromJsonAsync<CountryDTO>();                
                return model;
            }
            else
            {
                string str = await response.Content.ReadAsStringAsync();
                throw new Exception(str);
            }
        }

        public async Task<CountryDTO> Update(CountryDTO model)
        {            
            var response = await _client.PutAsJsonAsync(Literals.URI_API_COUNTRIES, model);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                model = await response.Content.ReadFromJsonAsync<CountryDTO>();               
                return model;
            }
            else
            {
                string str = await response.Content.ReadAsStringAsync();
                throw new Exception(str);
            }
        }

        public async Task<int> Delete(int id)
        {
            // Borrar del Api
            var response = await _client.DeleteAsync($"{Literals.URI_API_COUNTRIES}/{id}");

            if (response.StatusCode == HttpStatusCode.NoContent)    
                return 1;
            else
            {
                string str = await response.Content.ReadAsStringAsync();
                throw new Exception(str);
            }
        }
    }
}
