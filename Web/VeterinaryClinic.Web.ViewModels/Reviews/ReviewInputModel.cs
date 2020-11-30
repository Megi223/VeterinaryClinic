namespace VeterinaryClinic.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewInputModel
    {
        [Required]
        [Display(Name = "Please tell us what you think of our clinic here")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
