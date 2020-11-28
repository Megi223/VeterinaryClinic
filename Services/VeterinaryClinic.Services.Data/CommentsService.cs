using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task AddAsync(string vetId, string ownerId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                VetId = vetId,
                OwnerId = ownerId,
            };
            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        // Checks whether the parent comment is for the same vet as the comment I am trying to add
        public bool IsInCorrectVetId(int commentId, string vetId)
        {
            var commentVetId = this.commentsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.VetId).FirstOrDefault();
            return commentVetId == vetId;
        }
    }
}
