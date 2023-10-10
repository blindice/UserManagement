using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.DTO
{
    public  class CreateUserDeviceDTO
    {
        public int UserId { get; set; }

        [Required]
        public string DeviceName { get; set; } = null!;
    }
}
