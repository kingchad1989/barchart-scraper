namespace BarchartScraper
{
    public class Raw    
    {
        public string Symbol { get; set; } 
        public string BaseSymbol { get; set; } 
        public double BaseLastPrice { get; set; } 
        public int BaseSymbolType { get; set; } 
        public string SymbolType { get; set; } 
        public double StrikePrice { get; set; } 
        public string ExpirationDate { get; set; } 
        public int DaysToExpiration { get; set; } 
        public double BidPrice { get; set; } 
        public double Midpoint { get; set; } 
        public double AskPrice { get; set; } 
        public double LastPrice { get; set; } 
        public int Volume { get; set; } 
        public int OpenInterest { get; set; } 
        public double VolumeOpenInterestRatio { get; set; } 
        public double Volatility { get; set; } 
        public int TradeTime { get; set; } 
    }
}