namespace VeterinaryClinic.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task AddAsync(string vetId, string ownerId, string content, int? parentId = null);

        bool IsInCorrectVetId(int commentId, string vetId);
    }
}
