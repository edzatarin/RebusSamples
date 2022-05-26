using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EmailSender.Installers;

namespace EmailSender
{
    class Program
    {
        static void Main()
        {
            using (var container = new WindsorContainer())
            {
                container.Install(FromAssembly.Containing<RebusInstaller>(), FromAssembly.Containing<InfrastructureInstaller>());

                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
            }
        }
    }
}
