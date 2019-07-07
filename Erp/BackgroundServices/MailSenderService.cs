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
using Microsoft.Extensions.Configuration;

namespace Erp.BackgroundServices
{
    public class MailSenderService : BackgroundService
    {
     
       
        private readonly ILogger<MailSenderService> _logger;
        private Timer _timer;
        private IConfiguration _confg;
        private MailQueue _mailQueue;

        public IServiceProvider Services { get; }

        public MailSenderService(IConfiguration confg,
            ILoggerFactory loggerFactory, IServiceProvider services, MailQueue mailQueue)
        {
            _confg = confg;
            _mailQueue = mailQueue;
            Services = services;                                   
            _logger = loggerFactory.CreateLogger<MailSenderService>();
        }

       

        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Mail sender Hosted Service is starting.");   
            while (!cancellationToken.IsCancellationRequested)
            {
                var mail = await _mailQueue.DequeueAsync(cancellationToken);           
                if(mail != null)
                    sendMail(mail);

            }

            _logger.LogInformation("Mail sender Hosted Service is stopping.");
            
        }



        async Task sendMail(Email email)
        {
            using (var scope = Services.CreateScope())
            {
                                
                _logger.LogInformation("sending mail To " + email.recieverEmail);
                var message = new MimeMessage();

                MailboxAddress from = new MailboxAddress(_confg["Email:Smtp:Username"],
                _confg["Email:Smtp:Username"]);
                    Console.WriteLine(_confg["Email:Smtp:Username"]);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(email.recieverName,
                email.recieverEmail);
                message.To.Add(to);
                message.Subject = email.Subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = email.Message;
                message.Body = bodyBuilder.ToMessageBody();
                //item.IsSent = true;

                // await _emailUser.Update(item);
                using (SmtpClient client = new SmtpClient())
                {
                    try
                    {

                        client.Connect(_confg["Email:Smtp:Host"], int.Parse(_confg["Email:Smtp:Port"]), false);

                        client.Authenticate(_confg["Email:Smtp:Username"], _confg["Email:Smtp:Password"]);

                        client.Send(message);

                        client.Disconnect(true);
                    }
                    catch (Exception)
                    {

                        throw;
                    }        
                }

                 //   }
                //}
                //else
                //{
                //    _logger.LogInformation("No Mail To Send");
                //}
            }
        }
      
    }
}
