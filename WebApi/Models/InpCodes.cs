using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public partial class InpCodes
    {
        public InpCodes()
        {
            InpCodesImages = new HashSet<InpCodesImages>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Topic { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        [InverseProperty("InpCodes")]
        public virtual ICollection<InpCodesImages> InpCodesImages { get; set; }
    }
}
