using Application.Helpers;
using Application.Interfaces;
using Countries.Common.Classes;
using Countries.Infra.Data.Repositories.Custom;
using Countries.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SubdivisionsService : ISubdivisionsService
    {
        private readonly ISubdivisionsRepository _repository;        
        public SubdivisionsService(ISubdivisionsRepository repository) => this._repository = repository;

        public async Task<PagedList<Subdivision>> GetSubdivisions(SubdivisionParameter parameters)
        {
            try
            {
                var result = new List<Subdivision>();

                if (parameters != null)
                {

                    result = this._repository.Get(null, null, string.Empty) as List<Subdivision>;
                }

                return await Task.Run(() => PagedList<Subdivision>.ToPagedList(result, parameters.PageNumber, parameters.PageSize));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Subdivision> CreateSubdivisionAsync(Subdivision subdivision)
        {
            Subdivision created = new();

            try
            {
                if (subdivision != null)
                {
                    created = await this._repository.CreateAsync(subdivision);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return created;
        }

        public Subdivision GetSubdivision(int id)
        {
            Subdivision result = null;
           
            try
            {
                List<Subdivision> subdivision = this._repository.Get(x => x.SubdivisonID == id, null, string.Empty) as List<Subdivision>;

                if (subdivision.Count > 0)
                    result = subdivision[0];
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public async Task<Subdivision> UpdateSubdivisionAsync(Subdivision subdivision)
        {
            Subdivision updated = new();

            try
            {
                if (subdivision != null)
                {
                    updated = await this._repository.UpdateAsync(subdivision, subdivision.SubdivisonID);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return updated;
        }

        public async Task<int> DeleteSubdivisionAsync(Subdivision subdivision)
        {
            int deleted = 0;

            try
            {
                if (subdivision != null)
                    deleted = await this._repository.DeleteAsync(subdivision);
            }
            catch (Exception)
            {
                throw;
            }

            return deleted;
        }
    }
}
