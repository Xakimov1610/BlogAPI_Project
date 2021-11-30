using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entity;

namespace Api.Service
{
    public interface IMediaService
    {
        Task<bool> ExistsAsync(Guid id);
        Task<Media> GetAsync(Guid id);
        Task<List<Media>> GetAllAsync();
        Task<(bool IsSuccess, Exception Exception)> InsertAsync(List<Media> media);
        Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
    }
}