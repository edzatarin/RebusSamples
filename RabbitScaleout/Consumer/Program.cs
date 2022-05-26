using Messagex;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;

namespace Consumer;

internal class Program
{
    private static void Main()
    {
        using var adapter = new BuiltinHandlerActivator();
        adapter.Handle<Job>(async job =>
        {
            Console.WriteLine("Processing job {0}", job.JobNumber);

            await Task.Delay(200);
        });

        Configure.With(adapter)
            .Logging(l => l.ColoredConsole(LogLevel.Warn))
            .Transport(t => t.UseRabbitMq("amqp://localhost", "consumer"))
            .Options(o => o.SetMaxParallelism(5))
            .Start();

        adapter.Bus.Subscribe<Job>().Wait();

        Console.WriteLine("Consumer listening - press ENTER to quit");
        Console.ReadLine();
    }
}