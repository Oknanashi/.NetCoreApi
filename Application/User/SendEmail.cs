using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.User
{
    public class SendEmail
    {
        public class Command : IRequest<MessageForm>
        {
            public string Name { get; set; }
            public string Email { get; set; }

            public string Message { get; set; }
        }
        // public class CommandValidator :AbstractValidator<Command>{
        //     public CommandValidator(){
        //         RuleFor(x=>x.DisplayName).NotEmpty();
        //         RuleFor(x => x.Username).NotEmpty();
        //         RuleFor(x => x.Email).NotEmpty().EmailAddress();
        //         RuleFor(x => x.Password).Password();
        //     }
        // }
        public class Handler : IRequestHandler<Command, MessageForm>
        {


            public async Task<MessageForm> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // var credentials = new NetworkCredential("dedmopo3ik@gmail.com", "dfcz11BN");
                    // var mail = new MailMessage()
                    // {
                    //     From = new MailAddress("oknanashi@gmail.com"),
                    //     Subject = "Email Sender App",
                    //     Body = request.Message
                    // };
                    // mail.IsBodyHtml = true;
                    // mail.To.Add(new MailAddress(request.Email));
                    // var client = new SmtpClient()
                    // {
                    //     Port = 587,
                    //     DeliveryMethod = SmtpDeliveryMethod.Network,
                    //     UseDefaultCredentials = false,
                    //     Host = "smtp.gmail.com",
                    //     EnableSsl = true,
                    //     Credentials = credentials
                    // };
                    // client.Send(mail);
                    MailMessage message = new MailMessage(new MailAddress("belolok1@gmail.com")
                        ,new MailAddress("dedmopo3ik@gmail.com"));
                    message.Subject = "Good morning";
                    message.Body="Test Email";
                    SmtpClient client = new SmtpClient("smtp.gmail.com",465){
                        Credentials = new NetworkCredential("belolok1@gmail.com","dfcz11BN"),
                        EnableSsl = true
                    };
                    client.Send(message);
                    var sentMail = new MessageForm
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Message = request.Message
                    };
                    return sentMail;
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }
        }

    }
}