﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class HotelForCreatonDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<HotelForCreatonDto> Hotels { get; set; }
    }
}
