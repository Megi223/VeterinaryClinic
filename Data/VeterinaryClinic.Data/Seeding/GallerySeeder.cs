namespace VeterinaryClinic.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Models;

    public class GallerySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Gallery.Any())
            {
                await dbContext.AddAsync(new Gallery { Title = "Medical Checkup", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684309/pricing-3_ycyqfx.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Dog Training", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684308/image_6_lqh8n4.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Grooming", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684307/image_2_miatpf.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Dog walking", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684307/image_1_szun8e.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Dog", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684307/gallery-7_odobkn.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Dental care", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684306/gallery-5_ltsrtq.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Birthday Pug", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684307/gallery-6_idj3nt.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Medical Checkup", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605867051/cat-on-table-relaxing-1024x683_dt77jr.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Laboratory", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605867051/02_laboratory_z9wnvr.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "The Clinic", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605867052/Vet-Clinic-7small_h3cjaw.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "Dog And Cat", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605867051/dogAndCat_e3fkae.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "The Clinic", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605867052/Vet-Clinic-3small_nkgeja.jpg" });
                await dbContext.AddAsync(new Gallery { Title = "The Clinic", ImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1605867052/Vet-Clinic-5small_aeodhg.jpg" });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
