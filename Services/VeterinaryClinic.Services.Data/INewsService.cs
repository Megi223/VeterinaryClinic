namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VeterinaryClinic.Web.ViewModels.News;

    public interface INewsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(int id);

        int GetCount();

        Task AddNewsAsync(AddNewsInputModel model);
    }
}
