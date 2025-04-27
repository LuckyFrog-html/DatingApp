using Microsoft.Extensions.Configuration;
using DatingApp.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeMessage = MimeKit.MimeMessage;
using MailboxAddress = MimeKit.MailboxAddress;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using System.Net.Security;

namespace DatingApp.Infrastructure.Email
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _config;

		public EmailService(IConfiguration config)
		{
			_config = config;
		}

		public async Task SendEmailAsync(string email, string subject, string messageBody)
		{
			var settings = _config.GetSection("EmailSettings");

			string fromName = settings["FromName"];
			string fromEmail = settings["Username"];

			var message = new MimeMessage();

			message.From.Add(new MailboxAddress(fromName, fromEmail));
			message.To.Add(new MailboxAddress("", email));
			message.Subject = subject;

			message.Body = new TextPart("plain") { Text = messageBody };

			using var client = new SmtpClient();
			{
				client.ServerCertificateValidationCallback = (s, cert, chain, errors) => {
					// Игнорируем ошибки отзыва сертификата
					if (errors == SslPolicyErrors.None ||
						errors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors))
					{
						return true; // Принимаем сертификат
					}
					return false;
				};

				await client.ConnectAsync(
				settings["SmtpServer"],
				int.Parse(settings["Port"]),
				SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(fromEmail, settings["Password"]);
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}
			


		}
	}
}
