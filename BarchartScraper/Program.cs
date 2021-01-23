using System.Threading.Tasks;

namespace BarchartScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cookies = SeleniumPageHelper.GetCookies();
            var apiClient = new ApiClient(cookies);
            var optionsResponse = await apiClient.GetOptions();
        }
    }
}
