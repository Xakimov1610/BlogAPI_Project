using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entity
{
    public class Media
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContentType { get; set; }
        
        [Required]
        [MaxLength(3 * 1024 * 1024)]
        public byte[] Data { get; set; }  
        
    }
}