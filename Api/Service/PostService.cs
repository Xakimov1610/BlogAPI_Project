using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Service
{
    public class PostService : IPostService
    {
        private readonly BlogContext _ctx;
        private readonly ILogger<PostService> _log;

        public PostService(ILogger<PostService> logger, BlogContext context)
        {
            _ctx = context;
            _log = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception, Post Post)> CreateAsync(Post post)
        {
         try
        {
            await _ctx.Posts.AddAsync(post);
            await _ctx.SaveChangesAsync();

            _log.LogInformation($"Post create in DB: {post}");

            return (true, null, post);
        }
        catch(Exception e)
        {
             _log.LogInformation($"Create post to DB failed: {e.Message}", e);
            return (false, e, null);
        }

        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid Id)
        {
              if(!await ExistsAsync(Id))
            {
                _log.LogInformation($"delete post to DB failed: {Id}");

                return(false, new ArgumentException($"There is no Post with given Id: {Id}"));
            }
            // var post = _ctx.Posts.Where(p => p.Id == Id).Include(m => m.Medias).First();
            //     var medias = _ctx.Posts.FirstOrDefault(p => p.Id == Id).Medias.ToList();
            //     var comments = _ctx.Posts.FirstOrDefault(p => p.Id == Id).Comments.ToList();
            _ctx.Posts.Remove(await GetAsync(Id));
            await _ctx.SaveChangesAsync();
            _log.LogInformation($"Post remove in DB: {Id}");
           
            return (true, null);
        }

        public Task<bool> ExistsAsync(Guid id)
          => _ctx.Posts
        .AnyAsync(p => p.Id == id);

        public Task<List<Post>> GetAllAsync()
        => _ctx.Posts
            .AsNoTracking()
            .Include(m => m.Comments)
            .Include(m => m.Medias)
            .ToListAsync();
         public Task<List<Post>> GetIdAsync(Guid id)
        => _ctx.Posts
            .AsNoTracking()
            .Where(i => i.Id == id)
             .Include(m => m.Comments)
            .Include(m => m.Medias)
            .ToListAsync();
        public Task <Post> GetAsync(Guid id)
        => _ctx.Posts.FirstOrDefaultAsync(a => a.Id == id);
        

        public async Task<(bool IsSuccess, Exception Exception,Post Post)> UpdatePostAsync(Post post)
        {
            try
            {
                if(await _ctx.Posts.AnyAsync(t => t.Id == post.Id))
                {
                    _ctx.Posts.Update(post);
                    await _ctx.SaveChangesAsync();

                    return (true, null,post);
                }
                else
                {
                    return (false, new Exception($"Post with given ID: {post.Id} doesnt exist!"),null);
                }
            }
            catch(Exception e)
            {
                return (false, e,null);
            }
        }
    }
}