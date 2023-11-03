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
    public class LodgerRepository : RepositoryBase<Lodger>, ILodgerRepository
    {
        public LodgerRepository(RepositoryContext repositoryContext) : base(repositoryContext){}
        public IEnumerable<Lodger> GetLodgers(Guid hotelId, bool trackChanges) => FindByCondition(e => e.HotelId.Equals(hotelId), trackChanges)
            .OrderBy(e => e.Name);
        public Lodger GetLodger(Guid companyId, Guid id, bool trackChanges) => FindByCondition(e => e.HotelId.Equals(companyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();
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
