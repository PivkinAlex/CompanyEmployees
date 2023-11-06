using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges);
        Task<Hotel> GetHotelAsync(Guid hotelId, bool trackChanges);
        void CreateHotel(Hotel hotel);
        Task<IEnumerable<Hotel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteHotel(Hotel hotel);
    }
}
