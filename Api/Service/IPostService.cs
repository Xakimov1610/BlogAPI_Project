using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entity;

namespace Api.Service
{
    public interface IPostService
    {
    Task<(bool IsSuccess, Exception Exception, Post Post)> CreateAsync(Post post);
    Task<List<Post>> GetAllAsync();
    Task<Post> GetAsync(Guid id);
    Task<(bool IsSuccess, Exception Exception, Post Post)> UpdatePostAsync(Post post);
    Task<bool> ExistsAsync(Guid id);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
   
    }
}