using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public partial class InpWorks
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
    }
}
