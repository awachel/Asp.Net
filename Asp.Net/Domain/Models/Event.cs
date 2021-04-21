using EntityHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Event : Entity
    {
        [MaxLength(100)]
        [Required]
        public string EventName { get; set; }

        public virtual ICollection<person> People { get; set; }


    }
}
