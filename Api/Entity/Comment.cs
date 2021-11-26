using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entity
{
    public class Comment
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Author { get; set; }

        public string Content { get; set; }

        public EState State { get; set; }

        public Guid PostId { get; set; }

    }
}