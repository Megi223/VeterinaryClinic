using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Services.Data
{
    public interface ICommentsService
    {
        Task AddAsync(string vetId, string ownerId, string content, int? parentId = null);

        bool IsInCorrectVetId(int commentId, string vetId);
    }
}
