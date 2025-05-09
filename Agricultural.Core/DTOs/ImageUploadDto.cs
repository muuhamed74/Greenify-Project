
using Microsoft.AspNetCore.Http;

namespace Agricultural.Core.DTOs
{
    public class ImageUploadDto
    {
        public IFormFile Image { get; set; }
    }

}
