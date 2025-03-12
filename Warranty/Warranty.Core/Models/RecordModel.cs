using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.Core.Models
{
    public class RecordModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserModel User { get; set; }

        [ForeignKey("WarrantyId")]
        public int WarrantyId { get; set; }
        public WarrantyModel Warranty { get; set; }
    }
}
