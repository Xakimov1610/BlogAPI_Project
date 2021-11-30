using System.IO;
using Api.Entity;
using Microsoft.AspNetCore.Http;

namespace Api.Mapper
{
    public static class ModelEntityMappers
{
    public static Media ToEntity(this IFormFile media)
    {
        using var stream = new MemoryStream();
        media.CopyTo(stream);

        return new Media(contentType: media.ContentType, data: stream.ToArray());
    }
}
}