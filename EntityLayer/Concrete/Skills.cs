using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Skills
    {
        [Key]
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public byte Range { get; set; }
    }
}
