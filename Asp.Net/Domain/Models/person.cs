
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models 
{
     public class person: EntityHelper.Entity
    {
        [MaxLength(20)]
        [Required]
        public string Name{ get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Surname{ get; set; }
        
        
        [EmailAddress]
        [Required]
        public string EmailAdress{ get; set; }

        [Column(TypeName = "smalldatetime")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsAdult { get; set; }

        public virtual ICollection<Event> Events { get; set; }

    }
}
