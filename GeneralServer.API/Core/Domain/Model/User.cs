using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralServer.API.Core.Domain.Model
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
