using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    public class InpCodes:BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [DisplayName("Description With Code")]
        public string Description { get; set; }
        public List<InpCodesImages> inpCodesImages { get; set; }

        [NotMapped]
        public List<HttpPostedFileBase> Images { get; set; }


    }
}
