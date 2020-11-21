namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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
