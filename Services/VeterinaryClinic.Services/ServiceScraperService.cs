namespace VeterinaryClinic.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp;
    using AngleSharp.Dom;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.DTOs;

    public class ServiceScraperService : IServiceScraperService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        public ServiceScraperService(IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;

            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);
        }

        public async Task PopulateDbWithServices()
        {
            var concurrentBag = new ConcurrentBag<ServiceDTO>();
            IDocument document = await this.OpenUrlAsync();
            List<string> images = GetImages(document);
            List<string> links = GetLinks(document);

            for (int i = 0; i < links.Count; i++)
            {
                ServiceDTO serviceDTO = await this.CreateServiceDTO(images, links, i);
                concurrentBag.Add(serviceDTO);
            }

            foreach (var serviceDto in concurrentBag)
            {
                Service service = new Service
                {
                    Name = serviceDto.Name,
                    ImageUrl = serviceDto.ImageUrl,
                    Description = serviceDto.Description,
                };
                await this.servicesRepository.AddAsync(service);
                await this.servicesRepository.SaveChangesAsync();
            }

        }

        private async Task<ServiceDTO> CreateServiceDTO(List<string> images, List<string> links, int i)
        {
            var doc2 = await this.context.OpenAsync(links[i]);
            var name = doc2.QuerySelector(".entry-title").TextContent;
            var description = doc2.QuerySelector(".entry-content").OuterHtml.ToString();
            var formattedDescription = description.Replace("Sofia", "M&K");

            ServiceDTO serviceDTO = new ServiceDTO
            {
                Name = name,
                ImageUrl = images[i],
                Description = formattedDescription,
            };
            return serviceDTO;
        }

        private async Task<IDocument> OpenUrlAsync()
        {
            return await this.context.OpenAsync("https://sofiavetclinic.bg/en/services/");
        }

        private static List<string> GetLinks(IDocument document)
        {
            return document.QuerySelectorAll(".entry-subpages > .entry-content > .page-image > a")
                .Select(x => x.GetAttribute("href"))
                .ToList();
        }

        private static List<string> GetImages(IDocument document)
        {
            return document.QuerySelectorAll(".entry-subpages > .entry-content > .page-image > a > img")
                .Select(x => x.GetAttribute("src"))
                .ToList();
        }
    }
}
