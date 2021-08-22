using Countries.Infra.Data.Repositories.Generic;
using Countries.Models.Models;

namespace Countries.Infra.Data.Repositories.Custom
{
    public interface ISubdivisionsRepository : IBaseRepository<Subdivision>
    {
    }
    public class SubdivisionsRepository : BaseRepository<Subdivision>, ISubdivisionsRepository
    {
        public SubdivisionsRepository(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        { }
    }
}
