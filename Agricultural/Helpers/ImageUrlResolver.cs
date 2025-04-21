using Agricultural.Core.Models;
using AutoMapper;

namespace Agricultural.Helpers
{
    public class ImageUrlResolver : IValueResolver<PlantImages, object, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(PlantImages source, object destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImageUrl))
            {
                return null;
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return source.ImageUrl; // لو مش فيه Request، يرجّع المسار النسبي زي ما هو
            }

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}{source.ImageUrl}";
        }
    }
}
