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
    public interface ISubdivisionsHttpService
    {
        Task<PagingResponse<SubdivisionDTO>> GetSubdivisionsDTOAsync(SubdivisionParameter parameters);
        Task<SubdivisionDTO> GetByID(int id);
        Task<SubdivisionDTO> Create(SubdivisionDTO model);
        Task<SubdivisionDTO> Update(SubdivisionDTO model);
        Task<int> Delete(int id);
    }
    public class SubdivisionsHttpService : ISubdivisionsHttpService
    {
        private readonly HttpClient _client;
        public SubdivisionsHttpService(HttpClient client) => this._client = client;

        public async Task<PagingResponse<SubdivisionDTO>> GetSubdivisionsDTOAsync(SubdivisionParameter parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString(Literals.URI_API_SUBDIVISIONS, queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<SubdivisionDTO>
            {
                Items = JsonSerializer.Deserialize<List<SubdivisionDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<SubdivisionDTO> GetByID(int id)
        {
            var response = await this._client.GetFromJsonAsync<SubdivisionDTO>($"{Literals.URI_API_SUBDIVISIONS}/{id}");
            return response;
        }

        public async Task<SubdivisionDTO> Create(SubdivisionDTO model)
        {
            // Insertar en el api
            var response = await this._client.PostAsJsonAsync(Literals.URI_API_SUBDIVISIONS, model);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                model = await response.Content.ReadFromJsonAsync<SubdivisionDTO>();
                return model;
            }
            else
            {
                string str = await response.Content.ReadAsStringAsync();
                throw new Exception(str);
            }
        }

        public async Task<SubdivisionDTO> Update(SubdivisionDTO model)
        {
            var response = await _client.PutAsJsonAsync(Literals.URI_API_SUBDIVISIONS, model);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                model = await response.Content.ReadFromJsonAsync<SubdivisionDTO>();
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
            var response = await _client.DeleteAsync($"{Literals.URI_API_SUBDIVISIONS}/{id}");

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