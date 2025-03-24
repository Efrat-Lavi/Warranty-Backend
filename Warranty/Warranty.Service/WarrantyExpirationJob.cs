////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading;
////using System.Threading.Tasks;
////using AutoMapper;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.Extensions.Hosting;
////using Microsoft.Extensions.Logging;
////using Warranty.Core.Interfaces;
////using Warranty.Core.Interfaces.Services;
////using Warranty.Core.Models;
////namespace Recipes.Service.Services
////{
////    public class WarrantyExpirationJob : BackgroundService
////    {
////        private readonly IServiceProvider _serviceProvider;
////        private readonly ILogger<WarrantyExpirationJob> _logger;
////        private readonly IRepositoryManager _iRepository;


////        public WarrantyExpirationJob(IServiceProvider serviceProvider, ILogger<WarrantyExpirationJob> logger, IRepositoryManager repository)
////        {
////            _serviceProvider = serviceProvider;
////            _logger = logger;
////            _iRepository = repository;
////        }

////        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
////        {
////            while (!stoppingToken.IsCancellationRequested)
////            {
////                using (var scope = _serviceProvider.CreateScope())
////                {
////                    //var dbContext = scope.ServiceProvider.GetRequiredService<IData>(); // ⬅️ הוספת גישה ל-DB
////                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

////                    DateTime today = DateTime.UtcNow.Date;
////                    DateTime alertDate = today.AddDays(30); // בדיקה של 30 יום לפני

////                    List<RecordModel> expiringWarranties = await _iRepository.recordRepository.GetRecordsByDate(alertDate);

////                    foreach (RecordModel record in expiringWarranties)
////                    {
////                        var user = record.User;
////                        if (user != null)
////                        {
////                            string emailBody = $@"
////                            <p>שלום {user.NameUser},</p>
////                            <p>האחריות על <strong>{record.Warranty.NameProduct}</strong> עומדת לפוג בתאריך {record.Warranty.ExpirationDate:dd/MM/yyyy}.</p>
////                            <p>כדי להאריך את האחריות, לחצי על הכפתור הבא:</p>
////                            <a href='https://your-site.com/extend-warranty/{record.Warranty.Id}' 
////                               style='background-color: blue; color: white; padding: 10px 20px; text-decoration: none;'>
////                                הארך אחריות
////                            </a>";

////                            var emailRequest = new EmailRequest
////                            {
////                                To = user.Email,
////                                Subject = "תזכורת: האחריות שלך עומדת לפוג",
////                                Body = emailBody,
////                                IsHtml = true
////                            };

////                            await emailService.SendEmailAsync(emailRequest);
////                        }
////                    }
////                }

////                _logger.LogInformation("Job: בדיקת תוקף אחריות הסתיימה בהצלחה.");
////                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken); // להריץ כל 24 שעות
////                //await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // להריץ כל 24 שעות
////            }
////        }
////    }
////}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Warranty.Core.Interfaces;
//using Warranty.Core.Interfaces.Services;
//using Warranty.Core.Models;

//namespace Recipes.Service.Services
//{
//    public class WarrantyExpirationJob : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private readonly ILogger<WarrantyExpirationJob> _logger;
//        private readonly IRepositoryManager _iRepository;

//        public WarrantyExpirationJob(IServiceProvider serviceProvider, ILogger<WarrantyExpirationJob> logger, IRepositoryManager repository)
//        {
//            _serviceProvider = serviceProvider;
//            _logger = logger;
//            _iRepository = repository;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                using (var scope = _serviceProvider.CreateScope())
//                {
//                    // הזרקת שירותים אחרים בתוך ה-scope, לדוג' שירות של שליחה למייל
//                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

//                    DateTime today = DateTime.UtcNow.Date;
//                    DateTime alertDate = today.AddDays(30); // בדיקה של 30 יום לפני
//                    Console.WriteLine(alertDate);
//                    // גישה ל-repository דרך ה-Injected service
//                    List<RecordModel> expiringWarranties = await _iRepository.recordRepository.GetRecordsByDate(alertDate);

//                    foreach (RecordModel record in expiringWarranties)
//                    {
//                        var user = record.User;
//                        if (user != null)
//                        {
//                            string emailBody = $@"
//                            <p>שלום {user.NameUser},</p>
//                            <p>האחריות על <strong>{record.Warranty.NameProduct}</strong> עומדת לפוג בתאריך {record.Warranty.ExpirationDate:dd/MM/yyyy}.</p>
//                            <p>כדי להאריך את האחריות, לחצי על הכפתור הבא:</p>
//                            <a href='https://your-site.com/extend-warranty/{record.Warranty.Id}' 
//                               style='background-color: blue; color: white; padding: 10px 20px; text-decoration: none;' >
//                                הארך אחריות
//                            </a>";

//                            var emailRequest = new EmailRequest
//                            {
//                                To = user.Email,
//                                Subject = "תזכורת: האחריות שלך עומדת לפוג",
//                                Body = emailBody,
//                                IsHtml = true
//                            };

//                            await emailService.SendEmailAsync(emailRequest);
//                        }
//                    }
//                }

//                _logger.LogInformation("Job: בדיקת תוקף אחריות הסתיימה בהצלחה.");
//                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // להריץ כל 24 שעות
//                //await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // להריץ כל 24 שעות
//            }
//        }
//    }
//}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Interfaces;
using Warranty.Core.Models;

public class WarrantyExpirationJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WarrantyExpirationJob> _logger;

    public WarrantyExpirationJob(IServiceProvider serviceProvider, ILogger<WarrantyExpirationJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // יצירת scope חדש עבור כל הריצה של job
            using (var scope = _serviceProvider.CreateScope())
            {
                // הזרקת שירותים מתוך ה-scope
                var repositoryManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                DateTime today = DateTime.UtcNow.Date;
                DateTime alertDate = today.AddDays(30); // בדיקה של 30 יום לפני

                // גישה ל-repository דרך ה-Injected service
                List<RecordModel> expiringWarranties = await repositoryManager.recordRepository.GetRecordsByDate(alertDate);

                foreach (RecordModel record in expiringWarranties)
                {
                    var user = record.User;
                    if (user != null)
                    {
                        //string emailBody = $@"
                        //<p>שלום {user.NameUser},</p>
                        //<p>האחריות על <strong>{record.Warranty.NameProduct}</strong> עומדת לפוג בתאריך {record.Warranty.ExpirationDate:dd/MM/yyyy}.</p>
                        //<p>כדי להאריך את האחריות, לחצי על הכפתור הבא:</p>
                        //<a href='https://your-site.com/extend-warranty/{record.Warranty.Id}' 
                        //   style='background-color: blue; color: white; padding: 10px 20px; text-decoration: none;' >
                        //    הארך אחריות
                        //</a>";
                        string emailBody = $@"
<p>Hello {user.NameUser},</p>
<p>The warranty for <strong style='font-size: 18px; color: #2E8B57;'>{record.Warranty.NameProduct}</strong> is about to expire on <strong>{record.Warranty.ExpirationDate:dd/MM/yyyy}</strong>.</p>
<p>To extend the warranty, please click the button below:</p>
<a href='https://your-site.com/extend-warranty/{record.Warranty.Id}' 
   style='display: inline-block; background-color: #10a37f; color: white; padding: 10px 20px; text-decoration: none; border-radius: 12px; font-weight: bold; text-align: center;'>
    Extend Warranty
</a>";


                        var emailRequest = new EmailRequest
                        {
                            To = user.Email,
                            Subject = "תזכורת: האחריות שלך עומדת לפוג",
                            Body = emailBody,
                            IsHtml = true
                        };

                        await emailService.SendEmailAsync(emailRequest);
                    }
                }
            }

            _logger.LogInformation("Job: בדיקת תוקף אחריות הסתיימה בהצלחה.");
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // להריץ כל 24 שעות
        }
    }
}

