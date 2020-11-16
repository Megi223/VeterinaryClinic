using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Data.Common.Models;

namespace VeterinaryClinic.Data.Models
{
    public class VetsServices : BaseDeletableModel<int>
    {
        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }
    }
}
