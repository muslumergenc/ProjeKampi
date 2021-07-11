using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YouTubeMvc.Models
{
    public class ContactMailModel
    {
        public string Ad { get; set; }
        public string Email { get; set; }
        public string Konu { get; set; }
        public string Mesaj{ get; set; }
    }
}