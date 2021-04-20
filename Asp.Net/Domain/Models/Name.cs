using EntityHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domainn.Models
{
    public class Name : Entity
    {
        [MaxLength(20)]
        public string ImieValue { get; set; }
    }
}
