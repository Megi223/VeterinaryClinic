namespace VeterinaryClinic.Web.ViewModels.Ratings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PostRatingInputModel
    {
        public string VetId { get; set; }

        [Range(1, 5)]
        public byte Score { get; set; }
    }
}
