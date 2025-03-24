using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.Core.Models
{
    [Table("Users")]

    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string NameUser { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string HashPassword { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }


        public RoleModel Role  { get; set; }   

        public List<RecordModel> Records { get; set; }
    }
}
