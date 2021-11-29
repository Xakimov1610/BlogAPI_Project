using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Entity;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] MediaModel media)
        {
            var images = media.Data.Select(f =>
            {
                using var stream = new MemoryStream();
                f.CopyTo(stream);
                return new Media(contentType: f.ContentType, data: stream.ToArray());
            }).ToList();

            var result = await _mediaService.InsertAsync(images);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediaService.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var file = await _mediaService.GetAsync(id);
            var stream = new MemoryStream(file.Data);
            return File(stream, file.ContentType);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _mediaService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}