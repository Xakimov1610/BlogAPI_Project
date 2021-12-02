using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Api.Mapper;
using Api.Models;
using Api.Service;
using System;
using Api.Data;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _log;

        private readonly IPostService _pc;

        private readonly ICommentService _cs;

        private readonly IMediaService _mc;
        private readonly BlogContext _ctx;

        public PostController(
            ILogger<PostController> log,
            IPostService pc,
            ICommentService cs,
            IMediaService mc,
            BlogContext ctx
        )
        {
            _log = log;
            _pc = pc;
            _cs = cs;
            _mc = mc;
            _ctx = ctx;
        }

        [HttpPost] 
        [ActionName(nameof(PostAsync))]

        public async Task<IActionResult> PostAsync(PostModel post)
        {
            var media = post.Medias.Select(id => _mc.GetAsync(id).Result);
            var result = await _pc.CreateAsync(post.ToPostEntity(media));


           if(result.IsSuccess)
            {
              _log.LogInformation($"Post create in DB: {post.ToPostEntity(media).Id}");
            return CreatedAtAction(nameof(PostAsync), new {id = post.ToPostEntity(media).Id }, post.ToPostEntity(media));
            }

            return BadRequest(result.Exception.Message);
        }


        [HttpGet]
        public async Task<IActionResult> GetPost()
        {

            var images = await _pc.GetAllAsync();

            return Ok(images
                .Select(i =>
                {
                    return new {
                        Id = i.Id,
                        Title = i.Title,
                        Description=i.Description,
                        Content=i.Content,
                        Viewed=i.Viewed,
                        CreatedAt=i.CreatedAt,
                        ModifiedAt=i.ModifiedAt,
                        Comments=i.Comments

                    };
              }));
        }


        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetIdMedia(Guid Id)
        {
      
           if(!await _pc.ExistsAsync(Id))
        {
            return NotFound();
        }

            var images = await _pc.GetIdAsync(Id);

            return Ok(images
                .Select(i =>
                {
                    return new {
                        Id = i.Id,
                        Title = i.Title,
                        Description=i.Description,
                        Content=i.Content,
                        Viewed=i.Viewed,
                        CreatedAt=i.CreatedAt,
                        ModifiedAt=i.ModifiedAt,
                        Comments=i.Comments

                    };
              }));
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> PostUpdate([FromRoute] Guid Id, PostUpdated updated)
        {
            var media = updated.Medias.Select(id => _mc.GetAsync(id).Result);
            var toEntity = updated.ToUpdatePostEntity(media);
            var result = await _pc.UpdatePostAsync(toEntity);
           
            if(result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Exception.Message);
        }

   
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid Id)
        {

            var post =  await _pc.DeleteAsync(Id);
              
            if (post.IsSuccess)
            {
                return Ok(post);
            }

            return BadRequest(post.Exception.Message);
            
        }


        private bool _updatedValid(PostUpdated updated)
        {
            return !(updated.Title == null &&
                    updated.Description == null &&
                    updated.Content == null);
                   
        }

    }
}

