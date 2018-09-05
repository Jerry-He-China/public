using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net;
using System.Net.Mail;
//需要安装SharePoint SDK
using Microsoft.VisualStudio.SharePoint.Remote.Authentication;

namespace CustomSinkDemo
{
    public sealed class EmailSink : IObserver<EventEntry>
    {
        private const string DefaultSubject = "Email Sink Extension";
        private IEventTextFormatter formatter;
        private MailAddress sender;
        private MailAddressCollection recipients = new MailAddressCollection();
        private string subject;
        private string host;
        private int port;
        private NetworkCredential credentials;

        public EmailSink(string host, int port,
          string recipients, string subject, string credentials,
          IEventTextFormatter formatter)
        {
            this.formatter = formatter ?? new EventTextFormatter();
            this.host = host;
            this.port = GuardPort(port);
            this.credentials = new NetworkCredential("yourname@hotmail.com","password");//CredentialManager.GetCredentials(credentials);
            this.sender = new MailAddress(this.credentials.UserName);
            this.recipients.Add(GuardRecipients(recipients));
            this.subject = subject ?? DefaultSubject;
        }

        public void OnNext(EventEntry entry)
        {
            if (entry != null)
            {
                using (var writer = new StringWriter())
                {
                    this.formatter.WriteEvent(entry, writer);
                    Post(writer.ToString());
                }
            }
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        private void Post(string body)
        {
            using (var client = new SmtpClient(this.host, this.port)
            { Credentials = this.credentials, EnableSsl = true
            })
            using (var message = new MailMessage(this.sender, this.recipients[0])
            { Body = body, Subject = this.subject
            })
            {
                for (int i = 1; i < this.recipients.Count; i++)
                    message.CC.Add(this.recipients[i]);

                try
                {
                    client.Send(message);
                }
                catch (SmtpException e)
                {
                    SemanticLoggingEventSource.Log.CustomSinkUnhandledFault(
                      "SMTP error sending email: " + e.Message);
                }
                catch (InvalidOperationException e)
                {
                    SemanticLoggingEventSource.Log.CustomSinkUnhandledFault(
                      "Configuration error sending email: " + e.Message);
                }
                catch
                {
                    // additional exception handling code here.
                }

            }
        }

        private static int GuardPort(int port)
        {
            if (port < 0)
                throw new ArgumentOutOfRangeException("port");

            return port;
        }

        private static string GuardRecipients(string recipients)
        {
            if (recipients == null)
                throw new ArgumentNullException("recipients");
            if (string.IsNullOrWhiteSpace(recipients))
                throw new ArgumentException(
                  "The recipients cannot be empty", "recipients");

            return recipients;
        }
    }

    public static class EmailSinkExtensions
    {
        public static EventListener CreateListener(string host, int port,
            string recipients, string subject, string credentials,
            IEventTextFormatter formatter = null)
        {
            var listener = new ObservableEventListener();
            listener.LogToEmail(host, port, recipients, subject, credentials, formatter);
            return listener;
        }

        public static SinkSubscription<EmailSink> LogToEmail(
            this IObservable<EventEntry> eventStream, string host, int port,
            string recipients, string subject, string credentials,
            IEventTextFormatter formatter = null)
        {
            var sink = new EmailSink(host, port, recipients, subject, credentials, formatter);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<EmailSink>(subscription, sink);
        }
    }
}
