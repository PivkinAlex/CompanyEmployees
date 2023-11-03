using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILodgerRepository
    {
        IEnumerable<Lodger> GetLodgers(Guid hotelId, bool trackChanges);
        Lodger GetLodger(Guid companyId, Guid id, bool trackChanges);
        void CreateLodgerForHotel(Guid hotelId, Lodger lodger);
        void DeleteLodger(Lodger lodger);
    }
}
