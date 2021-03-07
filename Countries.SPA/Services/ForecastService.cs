using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Countries.SPA.Services
{
    public interface IForecastService
    {
        Task<WeatherForecastDTO> GetWeatherForecastDTOsAsync();
    }

    public class ForecastService : IForecastService
    {
        private readonly HttpClient _client;
        private WeatherForecastDTO forecasts = null;
        private readonly string URI_API = "api/WeatherForecast";

        public ForecastService(HttpClient client) => this._client = client;

        public async Task<WeatherForecastDTO> GetWeatherForecastDTOsAsync()
        {
            this.forecasts = await this._client.GetFromJsonAsync<WeatherForecastDTO>($"{this.URI_API}");
            
            return await Task.FromResult(this.forecasts);
        }
    }
}
