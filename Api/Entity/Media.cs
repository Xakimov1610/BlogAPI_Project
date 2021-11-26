using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Entity
{
    public class Media
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string ContentType { get; set; }

        
        public byte[] Data { get; set; }
    }
}