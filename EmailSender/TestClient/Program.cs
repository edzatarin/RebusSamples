using EmailSender.Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace TestClient
{
    internal class Program
    {
        private static void Main()
        {
            using var bus = Configure.With(new BuiltinHandlerActivator())//Configure.OneWayClient()
                .Transport(t => t.UseMsmqAsOneWayClient())
                .Routing(r => r.TypeBased().MapAssemblyOf<SendEmail>("emailsender"))
                .Start();

            using (bus)
            {
                while (true)
                {
                    var recipient = ReadLine("recipient");

                    if (string.IsNullOrEmpty(recipient))
                    {
                        break;
                    }

                    var subject = ReadLine("subject");
                    var body = ReadLine("body");

                    bus.Send(new SendEmail(recipient, subject, body)).Wait();
                }
            }
        }

        private static string? ReadLine(string what)
        {
            Console.Write($"Please enter {what} > ");
            var text = Console.ReadLine();
            return text;
        }
    }
}