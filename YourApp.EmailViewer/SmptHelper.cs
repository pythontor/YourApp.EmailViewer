using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;

public class SmtpHelper
{
    private string smtpServer;
    private int smtpPort;
    private string username;
    private string password;

    public SmtpHelper(string smtpServer, int smtpPort, string username, string password)
    {
        this.smtpServer = smtpServer;
        this.smtpPort = smtpPort;
        this.username = username;
        this.password = password;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(username)); // Отправитель
            message.To.Add(new MailboxAddress(to)); // Получатель
            message.Subject = subject; // Тема письма

            var textPart = new TextPart("plain")
            {
                Text = body // Текст сообщения
            };

            message.Body = textPart;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort, false);
                await client.AuthenticateAsync(username, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}