using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Hotel> GetAllHotels(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
        public Hotel GetHotel(Guid hotelId, bool trackChanges) => FindByCondition(c => c.Id.Equals(hotelId), trackChanges).SingleOrDefault();
        public void CreateHotel(Hotel hotel) => Create(hotel);
        public IEnumerable<Hotel> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
    }
}
