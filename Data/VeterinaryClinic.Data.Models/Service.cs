using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Data.Common.Models;

namespace VeterinaryClinic.Data.Models
{
    public class Service : BaseDeletableModel<int>
    {
        public Service()
        {
            this.VetsServices = new HashSet<VetsServices>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<VetsServices> VetsServices { get; set; }
    }
}
