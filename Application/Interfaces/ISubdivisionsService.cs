using Application.Helpers;
using Countries.Common.Classes;
using Countries.Models.Models;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubdivisionsService
    {
        Task<PagedList<Subdivision>> GetSubdivisions(SubdivisionParameter parameters);
        Subdivision GetSubdivision(int id);
        Task<Subdivision> CreateSubdivisionAsync(Subdivision subdivision);
        Task<Subdivision> UpdateSubdivisionAsync(Subdivision subdivision);
        Task<int> DeleteSubdivisionAsync(Subdivision subdivision);
    }
}
