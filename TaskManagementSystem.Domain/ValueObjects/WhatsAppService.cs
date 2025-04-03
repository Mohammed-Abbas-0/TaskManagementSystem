using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TaskManagementSystem.Domain.ValueObjects
{
    public class WhatsAppService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _whatsappFromNumber;

        public WhatsAppService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _whatsappFromNumber = configuration["Twilio:WhatsappFromNumber"];
        }

        public void SendWhatsAppMessage(string toPhoneNumber, string messageBody)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber($"whatsapp:{toPhoneNumber}"));

            messageOptions.From = new PhoneNumber(_whatsappFromNumber);
            messageOptions.Body = messageBody;

            var message = MessageResource.Create(messageOptions);

            Console.WriteLine($"Message sent: {message.Body}");
        }
    }
}
