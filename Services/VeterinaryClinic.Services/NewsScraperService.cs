using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services
{
    public class NewsScraperService : INewsScraperService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        public NewsScraperService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;

            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);
        }

        public async Task PopulateDbWithNews()
        {
            var concurrentBag = new ConcurrentBag<NewsDTO>();
            IDocument document = await this.OpenUrlAsync();

            var links = this.GetLinks(document);

            foreach (var link in links)
            {
                var newsDto = await this.GetAPieceOfNews(link);
                concurrentBag.Add(newsDto);
            }

            foreach (var newsDto in concurrentBag)
            {
                News news = new News
                {
                    Title = newsDto.Title,
                    Summary = newsDto.Summary,
                    Content = newsDto.Content,
                };
                await this.newsRepository.AddAsync(news);
                await this.newsRepository.SaveChangesAsync();
            }
        }

        private async Task<IDocument> OpenUrlAsync()
        {
            return await this.context.OpenAsync("https://www.sciencedaily.com/news/plants_animals/veterinary_medicine/");
        }

        private async Task<NewsDTO> GetAPieceOfNews(string link)
        {
            var url = $"https://www.sciencedaily.com{link}";
            var doc2 = await this.context.OpenAsync(url);
            var title = doc2.QuerySelector("#headline").TextContent;
            var summary = doc2.QuerySelector("#abstract").TextContent;
            var first = doc2.QuerySelector("#first").TextContent;
            var contentArr = doc2.QuerySelectorAll("#text > p")
                .Select(x => x.TextContent)
                .ToList();
            var content = string.Join(Environment.NewLine, contentArr);
            NewsDTO newsDTO = new NewsDTO
            {
                Title = title,
                Summary = summary,
                Content = content,
            };
            return newsDTO;
        }

        private IEnumerable<string> GetLinks(IDocument document)
        {
            return document.QuerySelectorAll(".latest-head > a")
            .Select(x => x.GetAttribute("href"))
            .ToList();
        }
    }
}
