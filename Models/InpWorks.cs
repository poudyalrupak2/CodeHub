using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class InpWorks:BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public String Description { get; set; }
        public string Date { get; set; }
    }
}
