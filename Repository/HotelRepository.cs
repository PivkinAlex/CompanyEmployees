using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(RepositoryContext repositoryContext) : base(repositoryContext){}
        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Hotel> GetHotelAsync(Guid hotelId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(hotelId), trackChanges).SingleOrDefaultAsync();
        public void CreateHotel(Hotel hotel) => Create(hotel);
        public async Task<IEnumerable<Hotel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteHotel(Hotel hotel)
        {
            Delete(hotel);
        }
    }
}
