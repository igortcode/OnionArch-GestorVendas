using Gestao.Core.Interfaces.CrossCutting;
using Gestao.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Notification.Notification.CrossCutting
{
    public class EmailCrossCutting : IEmailCrossCutting
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostEnvironment _hostEnvironment;

        public EmailCrossCutting(IOptions<EmailSettings> emailSettings, IHostEnvironment hostEnvironment)
        {
            _emailSettings = emailSettings.Value;
            _hostEnvironment = hostEnvironment;
        }
        public async Task Enviar(string para, string userName, string template, string assunto, IEnumerable<KeyValuePair<string, string>> parametros = null)
        {

            var tos = new List<EmailAddress>();
            string body = String.Empty;

            tos.Add(new EmailAddress(para));

            if (!string.IsNullOrEmpty(userName))
                tos.Add(new EmailAddress(userName));

            var plainTextContent = "Mio de Bão Lanchonete";

            var from = new EmailAddress(_emailSettings.From, _emailSettings.DisplayName);
            var templateFileInfo = _hostEnvironment.ContentRootFileProvider.GetFileInfo(template);
            if (!templateFileInfo.Exists)
                throw new FileNotFoundException("Template de email não encontrado!", templateFileInfo.PhysicalPath);

            using (Stream templateStream = templateFileInfo.CreateReadStream())
            using (var streamReader = new StreamReader(templateStream))
            {
                body = streamReader.ReadToEnd();
            }

            foreach (KeyValuePair<string, string> parametro in parametros ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                body = body.Replace(parametro.Key, parametro.Value);
            }

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, assunto, plainTextContent, body);
            var client = new SendGridClient(_emailSettings.Key);
            await client.SendEmailAsync(msg);
        }
    }
}
