using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext nZWalksDbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,  NZWalksDbContext nZWalksDbContext
            )
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksDbContext = nZWalksDbContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localImagePath = Path.Combine(
                webHostEnvironment.ContentRootPath,
                "Images",
                image.FileName);

            // upload image to local path
            using var stream = new FileStream(localImagePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // return https://localhost:port/Images/image.jpg

            string urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}";
            image.FilePath = urlFilePath;

            // Add to image DB

            await nZWalksDbContext.Images.AddAsync(image);
            await nZWalksDbContext.SaveChangesAsync();

            return image;
        }
    }
}
