using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository )
        {
            this.imageRepository = imageRepository;
        }
        // POST  /api/Images/Upload
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            // validate the uploaded file first
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // dto to domain model

                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileDescription = request.FileDescription,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileName = request.File.FileName,
                    FileSizeInBytes = request.File.Length
                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedFileExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported File Extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size more than 10MB");
            }
        }

    }
}
