namespace VeterinaryClinic.Web.ViewModels.ChatMessages
{
    using System.Collections.Generic;
    using System.Linq;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class SelectChatViewModel : IMapFrom<Pet>
    {
        public string OwnerId { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerFullName => this.OwnerFirstName + " " + this.OwnerLastName;

        public ICollection<Pet> OwnerPets { get; set; }

        public string Pets => string.Join(", ", this.OwnerPets.Select(x => x.Name));
    }
}
