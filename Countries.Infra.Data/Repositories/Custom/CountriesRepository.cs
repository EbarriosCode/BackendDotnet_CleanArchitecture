using Countries.Infra.Data.Repositories.Generic;
using Countries.Models.Models;

namespace Countries.Infra.Data.Repositories.Custom
{
    public interface ICountriesRepository : IBaseRepository<Country>
    {
    }
    public class CountriesRepository : BaseRepository<Country>, ICountriesRepository
    {
        public CountriesRepository(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        { }
    }
}
