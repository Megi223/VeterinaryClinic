namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        List<string> SearchVet(string term);

        List<string> SearchServices(string term);

        List<string> SearchNews(string term);

        List<T> SearchVet<T>(string term);

        List<T> SearchServices<T>(string term);

        List<T> SearchNews<T>(string term);
    }
}
