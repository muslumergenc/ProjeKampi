using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos
{
    public class WriterDto
    {
        [Required(ErrorMessage ="BOŞ GEÇİLEMEZ !")]
        public string WriterName { get; set; }
        public string WriterSurname { get; set; }
        [Required(ErrorMessage = "BOŞ GEÇİLEMEZ !")]
        [EmailAddress]
        public string WriterEmail { get; set; }
        [Required(ErrorMessage = "BOŞ GEÇİLEMEZ !")]
        public string WriterPassword { get; set; }
    }
}
