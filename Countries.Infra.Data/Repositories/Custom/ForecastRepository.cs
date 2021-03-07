using Countries.Infra.Data.DataContext;
using Countries.Infra.Data.Repositories.Generic;
using Countries.Models.Models;
using System.Collections.Generic;

namespace Countries.Infra.Data.Repositories
{
    public interface IForecastRepository : IBaseRepository<WeatherForecast>
    {
    }

    public class ForecastRepository : BaseRepository<WeatherForecast>, IForecastRepository
    {            
        public ForecastRepository(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        { }
    }
}
