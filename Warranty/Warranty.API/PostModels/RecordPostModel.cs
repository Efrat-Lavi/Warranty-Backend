﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.API.PostModels
{
    public class RecordPostModel
    {
        public int UserId { get; set; }
        public int WarrantyId { get; set; }
    }
}
