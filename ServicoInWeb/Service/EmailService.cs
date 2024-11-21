using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailModel _emailModel;
        private readonly UrlService _UrlService;

        public EmailService(IOptions<EmailModel> emailModel, UrlService urlService)
        {
            _emailModel = emailModel.Value;
            _UrlService = urlService;
        }

        public void SendEmailAsync(string emailDestinatario, string token)
        {
            string callBackUrl = $"{_UrlService.GetBaseUrl()}/Senha/RedefinirSenha?token={token}";//gera a url para retornar para a plataforma corretamente

            var Email = new MailMessage();
            Email.From = new MailAddress(_emailModel.EmailRemetente, _emailModel.Remetente);
            Email.To.Add(emailDestinatario);
            Email.Subject = "Redefinição de senha";
            Email.Body = EmailTemplates.RedefinirSenha.Replace("{callBackUrl}", callBackUrl); 
            Email.IsBodyHtml = true;

            EmailConfig().Send(Email);
        }

        private SmtpClient EmailConfig(){
            return new SmtpClient()
            {
                Host = _emailModel.EnderecoServidor,
                Port = _emailModel.Porta,
                EnableSsl = _emailModel.UsarSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _emailModel.EmailRemetente,
                    _emailModel.Senha
                )
            };
        }
    }

    public class EmailTemplates{
        public static string RedefinirSenha = "Conforme solicitado segue o link para redefinição de senha: <a href='{callBackUrl}'>Clique aqui</a>. <br> Caso não tenha solicitado a troca de senha por favor desconsidere o e-mail";
    }
}