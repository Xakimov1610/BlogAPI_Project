using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entity;

namespace Api.Service
{
    public interface IMediaService
    {
        Task<(bool IsSuccess, Exception Exception,List<Media> Media)> CreateMediaAsync(List<Media> media);
        Task<Media> GetAsync(Guid id);
        Task<List<Media>> GetAllAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
    }
}