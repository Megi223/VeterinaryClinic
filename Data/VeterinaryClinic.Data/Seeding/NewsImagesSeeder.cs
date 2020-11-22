namespace VeterinaryClinic.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services;

    public class NewsImagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.News.Any())
            {
                var service = (INewsScraperService)serviceProvider.GetService(typeof(INewsScraperService));
                await service.PopulateDbWithNews();
                dbContext.News.FirstOrDefault(x => x.Id == 1).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540761/43-researcherss_jton9j.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 2).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/index_bojkcw.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 3).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/cosmo_xzg3yk.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 4).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/flat-faced_af5cm5.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 5).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/human-activities_in2ke4.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 6).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/german-shepherd-dna_dpiene.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 7).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540760/children-autism_xskrd9.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 8).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540764/virusgenomes_pin83w.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 9).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540764/unravelingoneprion_qbjbpn.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 10).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/gasoline_qxoxdl.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 11).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540760/cats-covid_y7lf8z.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 12).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540761/catVSdog_gttlgg.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 13).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/environmentExposure_jkpfkl.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 14).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/rareBone_q27lhy.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 15).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540761/dogGame_vyel5v.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 16).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/hamstersCovid_isb8yx.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 17).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540764/sled-dog_fzvhe0.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 18).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/dogYears_ifpteu.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 19).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540764/whereDidComeFrom_jzxauw.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 20).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/mysteryabout_vuzzkf.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 21).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/dogFace_df0gcr.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 22).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540764/ReducingTransmissionRisk_d5zetz.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 23).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/petMentalHealth_xhbjam.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 24).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/neuterDog_gwowln.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 25).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/OneOutOfFour_ztwhqa.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 26).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540763/KeyToHappy_rmpwbg.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 27).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540760/activeDogs_pm1fuc.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 28).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540761/canine_fdmmzg.png";
                dbContext.News.FirstOrDefault(x => x.Id == 29).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540764/workingDogs_okpkts.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 30).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/FirstRehoming_sdyudn.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 31).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540761/DogsDoNotPreferFaces_nqwdva.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 32).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540760/adapt_fgvvpp.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 33).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540760/aromas_dlo8wo.jpg";
                dbContext.News.FirstOrDefault(x => x.Id == 34).ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605540762/FeedingCats_ipqzhv.jpg";
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
