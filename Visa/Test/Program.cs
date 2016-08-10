using System;
using Visa.WebCrawler.SeleniumCrawler;

namespace Test
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var regUser = new RegisterUser();
            regUser.GoToUrl();
            regUser.StartRegistration();
            regUser.ReloadPage();
            Console.ReadLine();
        }
    }
}