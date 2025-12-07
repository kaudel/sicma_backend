using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicma.Entities
{
    public class Institution: BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
