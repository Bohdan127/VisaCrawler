using System.Resources;

namespace Visa.Resources
{
  public  class UkUaManager : ResourceManager
    {
        public UkUaManager() : base("Visa.Resources.uk-UA.uk-UA", typeof(UkUaManager).Assembly)
        {

        }
    }
}
