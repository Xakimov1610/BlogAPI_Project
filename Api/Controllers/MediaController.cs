using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _log;
        private readonly IMediaService _ser;

        public MediaController(ILogger<MediaController> log,IMediaService service)
        {
          _log = log;
          _ser = service;
        }

    [HttpPost]
    public async Task<IActionResult> PostMediaAsync([FromForm]MediaModel media)
    {
        var files = media.Data.Select(GetImageEntity).ToList();
       
        var result = await _ser.CreateMediaAsync(files);
       
         if(result.IsSuccess)
        {
           _log.LogInformation($"Media create in DB: {media}");
             return Ok(files.Select(i =>
        {
            return new 
            {
                Id = i.Id,
                ContentType = i.ContentType,
                Size = i.Data.Length,
            };
        }));
        }
        return BadRequest(result.Exception.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetMedia()
    {
      var images = await _ser.GetAllAsync();

        return Ok(images.Select(i =>
        {
            return new 
            {
                ContentType = i.ContentType,
                Size = i.Data.Length,
                Id = i.Id
            };
        }));
    }

    [HttpGet]
    [Route("/media/{id}")]
    public async Task<IActionResult> GetMedia(Guid id)
    {
        var file = await _ser.GetAsync(id);

        var stream = new MemoryStream(file.Data);

        return File(stream, file.ContentType);
    }

     [HttpDelete]
     [Route("{Id}")]
     public async Task<IActionResult> Delete([FromRoute]Guid Id)
    {

         var media =  await _ser.DeleteAsync(Id);
              
         if(media.IsSuccess)
        {
           _log.LogInformation($"Media delete in DB: {media}");
            return Ok();
        }
        return BadRequest(media.Exception.Message);
            
    }

      private Api.Entity.Media GetImageEntity(IFormFile file)
    {
        using var stream = new MemoryStream();

        file.CopyTo(stream);

        return new Api.Entity.Media()
        {
            Id = Guid.NewGuid(),
            ContentType = file.ContentType,
            Data = stream.ToArray()
        };
    }

    }
}