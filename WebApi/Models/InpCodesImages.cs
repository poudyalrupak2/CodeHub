using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public partial class InpCodesImages
    {
        [Key]
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        public Guid InpCodesId { get; set; }

        [ForeignKey(nameof(InpCodesId))]
        [InverseProperty("InpCodesImages")]
        public virtual InpCodes InpCodes { get; set; }
    }
}
