using Octokit;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Visa.BusinessLogic.Managers
{
    public class UpdateManager
    {
        private readonly GitHubClient _client;

        public UpdateManager()
        {
            _client = new GitHubClient(new ProductHeaderValue("VisaCrawler"));
            LogIn();
        }


        public async Task<Release> GetPhantomJsRelease()
        {
            var releaseList = await _client.Repository.Release.GetAll("bbenetskyy", "VisaCrawler");
            return releaseList.FirstOrDefault(release => release.Name.Contains("PhantomJS"));

        }

        public async Task<Release> GetRelease()
        {
            var releaseList = await _client.Repository.Release.GetAll("bbenetskyy", "VisaCrawler");
            return releaseList.FirstOrDefault(release => !release.Name.Contains("PhantomJS"));

        }

        public bool NeedUpdate(Assembly currentAssembly, Release lastRelease)
        {
            return lastRelease.TagName.Contains(currentAssembly.GetName()
                                                               .Version.ToString());
        }



        private void LogIn()
        {
            _client.Connection.Credentials = new Credentials("bbenetskyy", "zaq12wsX");
        }
    }
}
