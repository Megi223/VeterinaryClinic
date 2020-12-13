namespace VeterinaryClinic.Web.ViewModels.ChatMessages
{
    using System.ComponentModel.DataAnnotations;

    public class SendChatMessageInputModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string UserId { get; set; }

        public string CallerId { get; set; }
    }
}
