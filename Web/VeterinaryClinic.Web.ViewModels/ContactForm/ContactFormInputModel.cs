namespace VeterinaryClinic.Web.ViewModels.ContactForm
{
    using VeterinaryClinic.Services.Mapping;

    public class ContactFormInputModel : IMapFrom<VeterinaryClinic.Data.Models.ContactForm>
    {
        public string Username { get; set; }
    }
}
