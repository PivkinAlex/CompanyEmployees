using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.RequestFeatures;

namespace Repository
{
    public class LodgerRepository : RepositoryBase<Lodger>, ILodgerRepository
    {
        public LodgerRepository(RepositoryContext repositoryContext) : base(repositoryContext){}
        public async Task<PagedList<Lodger>> GetLodgersAsync(Guid hotelId, LodgerParameters lodgerParameters, bool trackChanges)
        {
            var lodgers = await FindByCondition(e => e.HotelId.Equals(hotelId), trackChanges)
                .OrderBy(e => e.Name)
                .ToListAsync();
            return PagedList<Lodger>.ToPagedList(lodgers, lodgerParameters.PageNumber, lodgerParameters.PageSize);
        }
        public async Task<Lodger> GetLodgerAsync(Guid companyId, Guid id, bool trackChanges) => await FindByCondition(e => e.HotelId.Equals(companyId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateLodgerForHotel(Guid hotelId, Lodger lodger)
        {
            lodger.HotelId = hotelId;
            Create(lodger);
        }
        public void DeleteLodger(Lodger lodger)
        {
            Delete(lodger);
        }
    }
}
