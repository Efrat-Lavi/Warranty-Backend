using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(EmailRequest request);
    }
}