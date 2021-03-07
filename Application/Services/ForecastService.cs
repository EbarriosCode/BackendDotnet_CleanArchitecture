using Application.DTO;
using Application.Interfaces;
using Countries.Infra.Data.Repositories;

namespace Application.Services.CustomRepositories
{
    public class ForecastService : IForecastService
    {
        private readonly IForecastRepository _repository;
        public ForecastService(IForecastRepository repository)
        {
            _repository = repository;
        }
        public WeatherForecastDTO GetForecasts()
        {
            return new WeatherForecastDTO()
            {
                Forecasts = _repository.Get(null, null, string.Empty)
            };
        }
    }
}
