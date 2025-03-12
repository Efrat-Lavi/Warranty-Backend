﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Repositories
{

    public interface ICompanyRepository:IRepository<CompanyModel>
    {
        Task<List<CompanyModel>> GetFullAsync();

    }
}
