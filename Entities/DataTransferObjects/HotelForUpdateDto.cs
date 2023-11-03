using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class HotelForUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<LodgerForCreationDto> Lodgers { get; set; }
    }
}
