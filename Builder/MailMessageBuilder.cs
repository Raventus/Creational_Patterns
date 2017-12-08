using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Builder
{
    public sealed class MailMessageBuilder
    {
        private readonly MailMessage _mailMessage = new MailMessage();

        public MailMessageBuilder From(string adress)
        {
            _mailMessage.From = new MailAddress(adress);
            return this;
        }

        public MailMessageBuilder To(string address)
        {
            _mailMessage.To.Add(address);
            return this;
        }

        public MailMessageBuilder Cc(string address)
        {
            _mailMessage.CC.Add(address);
            return this;
        }

        public MailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public MailMessageBuilder Body(string body, Encoding encoding)
        {
            _mailMessage.Body = body;
            _mailMessage.BodyEncoding = encoding;
            return this;
        }

        public MailMessage Build()
        {
            if (_mailMessage.To.Count == 0)
            {
                throw new InvalidOperationException("Invoke To method to build this object");
            }
            return _mailMessage;
        }
    }

    public class Client
    {
        public Client()
        {
            var mail = new MailMessageBuilder().From("st@mail.ru").To("raven@mail.ru").Body("Please fix my comp", Encoding.UTF8).Build();
        }
    }


    // Можно также использовать методы расшрения класса MailMessage
    public static class MailMessageBuilderEx
    {
        public static MailMessage From (this MailMessage mailMessage, string address)
        {
            mailMessage.From = new MailAddress(address);
            return mailMessage;
        }

        public static MailMessage To (this MailMessage mailMessage, string address)
        {
            mailMessage.To.Add (address);
            return mailMessage;
        }

        /// ... и так далее
    }

    // можно спроектировать класс с внутренним строителем, где один из методов возвращает его и только через него можно получить доступ к прайват полям, после обращения, которые
    // становяться неизменяемыми
    public class MailMessage2
    {
        private string _to;
        private string _from;
        private string _subject;
        private string _body;
        private MailMessage2() { }

        public static MailMessageBuilder2 With ()
        {
            return new MailMessageBuilder2(new MailMessage2());
        }

        public string To { get { return _to; } }
        public string From { get { return _from; } }
        public string Subject { get { return _subject; } }
        public string Body { get { return _body; } }

        public class MailMessageBuilder2
        {
            private readonly MailMessage2 _mailMessage2;

            internal MailMessageBuilder2(MailMessage2 mailMessage2)
            {
                this._mailMessage2 = mailMessage2;
            }

            public MailMessageBuilder2 From(string from)
            {
                _mailMessage2._from = from;
                return this;
            }

            public MailMessageBuilder2 To(string to)
            {
                _mailMessage2._to = to;
                return this;
            }
     
            public MailMessageBuilder2 Subject(string subject)
            {
                _mailMessage2._subject = subject;
                return this;
            }

            public MailMessageBuilder2 Body(string body)
            {
                _mailMessage2._body = body;
                return this;
            }

            public MailMessage2 Build()
            {

                return _mailMessage2;
            }

          
        }

        public class Client2
        {
            public Client2()
            {
                var mail = MailMessage2.With().From("123").To("123312").Body("123312123").Build();
            }
        }
    }
}