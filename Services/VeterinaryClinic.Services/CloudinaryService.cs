namespace VeterinaryClinic.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly string defaultProfilePicUrl = @"https://res.cloudinary.com/dpwroiluv/image/upload/v1606144918/default-profile-icon-16_vbh95n.png";

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploudAsync(IFormFile file)
        {
            if (file == null || this.IsFileValid(file) == false)
            {
                return this.defaultProfilePicUrl;
            }

            string url = " ";
            byte[] fileBytes;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                fileBytes = stream.ToArray();
            }

            using (var uploadStream = new MemoryStream(fileBytes))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, uploadStream),
                };
                var result = await this.cloudinary.UploadAsync(uploadParams);

                url = result.Uri.AbsoluteUri;
            }

            return url;
        }


        public bool IsFileValid(IFormFile file)
        {
            if (file == null)
            {
                return true;
            }

            string[] validTypes = new string[]
            {
                "image/x-png", "image/jpeg", "image/jpg", "image/png",
            };

            if (validTypes.Contains(file.ContentType) == false)
            {
                return false;
            }

            return true;
        }
    }
}
