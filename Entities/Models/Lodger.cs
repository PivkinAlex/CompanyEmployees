using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Lodger
    {
        [Column("LodgerId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Lodger name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        public uint Room { get; set; }
        [ForeignKey(nameof(Hotel))]
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
