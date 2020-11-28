using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeterinaryClinic.Web.ViewModels.Ratings
{
    public class PostRatingInputModel
    {
        public string VetId { get; set; }

        [Range(1, 5)]
        public byte Score { get; set; }
    }
}
