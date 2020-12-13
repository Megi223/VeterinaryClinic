namespace VeterinaryClinic.Data.Models
{
    using VeterinaryClinic.Data.Common.Models;

    public class Notification : BaseDeletableModel<int>
    {
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public string Content { get; set; }
    }
}
