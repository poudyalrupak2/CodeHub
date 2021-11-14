using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InpCodesImages:BaseEntity
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public InpCodes InpCodes { get; set; }
        public Guid InpCodesId { get; set; }
    }
}
