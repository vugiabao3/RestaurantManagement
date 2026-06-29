using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Authentication;

namespace RestaurantManagement.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(
        IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string body)
    {
        var smtpClient = new SmtpClient(
            _emailSettings.SmtpServer)
        {
            Port = _emailSettings.Port,

            Credentials = new NetworkCredential(
                _emailSettings.SenderEmail,
                _emailSettings.AppPassword
            ),

            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(
                _emailSettings.SenderEmail,
                _emailSettings.SenderName
            ),

            Subject = subject,

            Body = body,

            IsBodyHtml = false
        };

        mailMessage.To.Add(to);

        await smtpClient.SendMailAsync(mailMessage);
    }
}