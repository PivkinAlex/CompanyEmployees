﻿using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILodgerRepository
    {
        Task<PagedList<Lodger>> GetLodgersAsync(Guid hotelId, LodgerParameters lodgerParameters, bool trackChanges);
        Task<Lodger> GetLodgerAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateLodgerForHotel(Guid hotelId, Lodger lodger);
        void DeleteLodger(Lodger lodger);
    }
}
