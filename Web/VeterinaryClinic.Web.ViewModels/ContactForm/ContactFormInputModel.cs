using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.ContactForm
{
    public class ContactFormInputModel : IMapFrom<VeterinaryClinic.Data.Models.ContactForm>
    {
        public string Username { get; set; }

        
    }
}
