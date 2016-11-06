using System;
using System.Reflection;
using Visa.BusinessLogic.Managers;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mg = new UpdateManager();
            mg.NeedUpdate(Assembly.GetEntryAssembly(), mg.GetPhantomJsRelease()
                                                         .Result);
            Console.ReadLine();
        }
    }
}