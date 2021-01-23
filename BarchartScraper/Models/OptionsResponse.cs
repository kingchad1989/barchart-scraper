using System.Collections.Generic;

namespace BarchartScraper
{
    public class OptionsResponse    
    {
        public int Count { get; set; } 
        public int Total { get; set; } 
        public List<Datum> Data { get; set; } 
        public Meta Meta { get; set; } 
    }
}