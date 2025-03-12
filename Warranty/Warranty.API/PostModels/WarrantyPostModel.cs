using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.API.PostModels
{
    public class WarrantyPostModel
    {
        public string NameProduct { get; set; }
        
        public string LinkFile { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int CompanyId { get; set; }
    }
}
