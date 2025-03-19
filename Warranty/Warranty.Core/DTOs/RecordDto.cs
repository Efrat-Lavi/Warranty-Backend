using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.DTOs
{
    public class RecordDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int WarrantyId { get; set; }
        public WarrantyDto Warranty { get; set; }
        public RoleDto RoleWarranty { get; set; }

    }
}
