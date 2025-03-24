using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.Core.Models
{
    [Table("Warranties")]

    public class WarrantyModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string NameProduct { get; set; }
        
        [Required]
        public string LinkFile { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public List<RecordModel> Records { get; set; }
    }
}
