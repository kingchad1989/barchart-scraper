namespace BarchartScraper
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using OpenQA.Selenium;
    using RestSharp;
    using RestSharp.Serializers.NewtonsoftJson;

    public class ApiClient
    {
        private readonly RestClient _client;

        private readonly ReadOnlyCollection<Cookie> _cookies;

        private readonly List<string> _fields = new List<string>
        {
            "symbol",
            "baseSymbol",
            "baseLastPrice",
            "baseSymbolType",
            "symbolType",
            "strikePrice",
            "expirationDate",
            "daysToExpiration",
            "bidPrice",
            "midpoint",
            "askPrice",
            "lastPrice",
            "volume",
            "openInterest",
            "volumeOpenInterestRatio",
            "volatility",
            "tradeTime",
            "symbolCode"
        };

        private readonly List<string> _meta = new List<string>
        {
            "field.shortName",
            "field.type",
            "field.description"
        };

        public ApiClient(ReadOnlyCollection<Cookie> cookies)
        {
            _cookies = cookies;
            _client = new RestClient("https://www.barchart.com/proxies/core-api/v1/");
            _client.UseNewtonsoftJson(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Include,
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public async Task<OptionsResponse> GetOptions()
        {
            var request = BuildRequest();
            var response = await _client.ExecuteAsync<OptionsResponse>(request);
            return response.Data;
        }

        private RestRequest BuildRequest()
        {
            var request = new RestRequest("options/get");

            // Add cookies to request
            foreach (var cookie in _cookies) request.AddCookie(cookie.Name, cookie.Value);

            // Add parameters to request
            request.AddParameter("fields", string.Join(",", _fields));
            request.AddParameter("meta", string.Join(",", _meta));
            request.AddParameter("orderBy", "volumeOpenInterestRatio");
            request.AddParameter("orderDir", "desc");
            request.AddParameter("baseSymbolTypes", "stock");
            request.AddParameter("between(volumeOpenInterestRatio,1.24,)", string.Empty);
            request.AddParameter("between(lastPrice,.10,)", string.Empty);
            request.AddParameter("between(tradeTime,2021-01-22,2021-01-25)", string.Empty);
            request.AddParameter("between(volume,500,)", string.Empty);
            request.AddParameter("between(openInterest,100,)", string.Empty);
            request.AddParameter("in(exchange,(AMEX,NASDAQ,NYSE))", string.Empty);
            request.AddParameter("page", 1);
            request.AddParameter("limit", 10000);
            request.AddParameter("raw", 1);

            // Add token header for API authentication
            var xsrfToken = Uri.UnescapeDataString(_cookies.First(cookie =>
                string.Equals(cookie.Name, "XSRF-TOKEN", StringComparison.CurrentCultureIgnoreCase)).Value);
            request.AddHeader("X-XSRF-TOKEN", xsrfToken);
            return request;
        }
    }
}