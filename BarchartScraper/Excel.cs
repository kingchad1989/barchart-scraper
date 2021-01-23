namespace BarchartScraper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using OfficeOpenXml;

    public class Excel
    {
        public async Task WriteOptionsResponseToExcelFile(OptionsResponse optionsResponse)
        {
            var rows = new List<string[]>();
            // Add header 
            rows.Add(new[]
            {
                "Symbol",
                "Price",
                "Type",
                "Strike",
                "Exp Date",
                "DTE",
                "Bid",
                "Midpoint",
                "Ask",
                "Last",
                "Volume",
                "Open Int",
                "Vol/OI",
                "IV",
                "Last Trade"
            });
            // Now add row for each datum
            rows.AddRange(optionsResponse.Data.Select(data =>
                new[]
                {
                    data.BaseSymbol,
                    data.BaseLastPrice,
                    data.SymbolType,
                    data.StrikePrice,
                    data.ExpirationDate,
                    data.DaysToExpiration,
                    data.BidPrice,
                    data.Midpoint,
                    data.AskPrice,
                    data.LastPrice,
                    data.Volume,
                    data.OpenInterest,
                    data.VolumeOpenInterestRatio,
                    data.Volatility,
                    data.TradeTime
                }));

            using var excel = new ExcelPackage();
            var worksheet = excel.Workbook.Worksheets.Add("Worksheet1");
            var headerRange = "A1:" + char.ConvertFromUtf32(rows[0].Length + 64) + "1";
            worksheet.Cells[headerRange].LoadFromArrays(rows);
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Output.xlsx");
            await excel.SaveAsAsync(fileInfo);
        }
    }
}