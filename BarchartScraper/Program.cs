namespace BarchartScraper
{
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var cookies = WebPage.GetCookies();
            var optionsResponse = await new ApiClient(cookies).GetOptions();
            var excelHelper = new Excel().WriteOptionsResponseToExcelFile(optionsResponse);
        }
    }
}