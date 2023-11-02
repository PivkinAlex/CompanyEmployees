﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAllHotels(bool trackChanges);
        Hotel GetHotel(Guid hotelId, bool trackChanges);
        void CreateHotel(Hotel hotel);
        IEnumerable<Hotel> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
