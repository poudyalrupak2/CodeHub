using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models.ViewModel
{
  public   class SignupViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
    }
}
