namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VeterinaryClinic.Data.Common.Models;

    public class Gallery : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
