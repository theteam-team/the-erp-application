using Erp.Data;
using Erp.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.DependencyInjection;
using Erp.Data.Entities;

namespace Erp.BackgroundServices
{
    public class SystemBackgroundService : BackgroundService
    {
        //private readonly IEmailRepository _email;
        //private readonly IEmailUserRepository _emailUser;
     
       
        private readonly ILogger<SystemBackgroundService> _logger;
        private Timer _timer;

        public IServiceProvider Services { get; }

        public SystemBackgroundService(
            ILoggerFactory loggerFactory, IServiceProvider services)
        {
            //_email = email;
            Services = services;
            //_emailUser = emailUser;
          
           
            //TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<SystemBackgroundService>();
        }

       

        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Mail sender Hosted Service is starting.");   
            while (!cancellationToken.IsCancellationRequested)
            {
               
                await sendMail();
                Thread.Sleep(50000000);

            }

            _logger.LogInformation("Queued sender Hosted Service is stopping.");
        }



        async Task sendMail()
        {
            using (var scope = Services.CreateScope())
            {
                
                var _emailUser = scope.ServiceProvider.GetRequiredService<IEmailUserRepository>();
                var _email = scope.ServiceProvider.GetRequiredService<IEmailRepository>();
                var _accountDbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                List<UserHasEmail> userHasEmails = await _emailUser.GetUnSentMails();
                if (userHasEmails != null && userHasEmails.Count > 0)
                {
                    foreach (var item in userHasEmails)
                    {
                        Email email = await _email.GetById((object)item.EmailId);

                        var user = _accountDbContext.ErpUsers.Where(u => u.Id == item.ApplicationUserId).FirstOrDefault();


                        var message = new MimeMessage();

                        MailboxAddress from = new MailboxAddress("Admin",
                        "phyzia123@gmail.com");
                        message.From.Add(from);

                        MailboxAddress to = new MailboxAddress("User",
                        user.Email);
                        message.To.Add(to);
                        message.Subject = email.Subject;
                        BodyBuilder bodyBuilder = new BodyBuilder();
                        //bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
                        bodyBuilder.TextBody = email.Message;
                        message.Body = bodyBuilder.ToMessageBody();
                        item.IsSent = true;

                        _logger.LogInformation("sending mail To " + user.Email);
                         await _emailUser.Update(item);
                        /*using (SmtpClient client = new SmtpClient())
                        {
                            client.Connect("smtp.gmail.com", 587, false);
                            
                            client.Authenticate("phyzia123@gmail.com", "");

                            client.Send(message);
                            client.Disconnect(true);
                        }*/

                    }
                }
                else
                {
                    //_logger.LogInformation("No Mail To Send");
                }
            }
        }
        //protected Task foo()
        //{
        //    Task[] tasks = new Task[5];
        //    for (int i = 0; i <= 4; i++)
        //    {
        //        tasks[i] = Task.Run(() => {
        //            int TaskId = (int)Task.CurrentId;
        //            // Each task begins by requesting the semaphore.
        //            Console.WriteLine("Task {0} begins and waits for the semaphore.",
        //                              Task.CurrentId);
        //            Func<CancellationToken, Task> func = _cancellationToken =>
        //            {
        //                Console.WriteLine("Task {0}", TaskId);
        //                return Task.CompletedTask;
        //            };


        //            TaskQueue.QueueBackgroundWorkItem(func);


        //            //Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);

        //            // The task just sleeps for 1+ seconds.



        //        });
        //    }
        //    return Task.CompletedTask;

        //}
    }
}
