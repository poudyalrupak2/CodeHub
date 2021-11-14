using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
   public  class CodeHubVm
    {
        public Models.InpCodes Inpcodes { get; set; }
        public IEnumerable<InpCodesImages> Img { get; set; }
    }
}
