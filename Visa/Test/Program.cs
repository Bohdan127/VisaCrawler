using Visa.BusinessLogic.Managers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stateManager = new StateManager();
            stateManager.GetCurrentSiteAvailability();
        }
    }
}