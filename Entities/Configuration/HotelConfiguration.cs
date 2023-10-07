using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData
            (
            new Hotel
            {
                Id = new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"),
                Name = "Holiday Inn",
                Address = "Ul Pushkina, dom kolotushkina"
            },
            new Hotel
            {
                Id = new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"),
                Name = "Radisson Blue",
                Address = "Ul Kolotushkina, dom Pushkina"
            }
            );
        }
    }
}
