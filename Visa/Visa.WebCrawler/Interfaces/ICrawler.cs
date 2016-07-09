using OpenQA.Selenium;

namespace Visa.WebCrawler.Interfaces
{
    public interface ICrawler
    {
        bool Error { get; }
        string OutData { get; }
        bool Canceled { get; set; }

        IWebElement FindElementWithChecking(By by);
    }
}
