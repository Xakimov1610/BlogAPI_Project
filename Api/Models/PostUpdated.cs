using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Entity;

namespace Api.Models
{
    public class PostUpdated
    {
        public Guid Id { get; set; }
        public Guid HeaderImageId { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public string Content { get; set; }
        public uint Viewed { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public IEnumerable<Guid> Medias { get; set; }
    }
}