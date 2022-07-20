namespace Storage.Module.Classes
{
    public record TickerInfo
    {
        public TickerInfo(string fromTicker, string toTicker)
        {
            FromTicker = fromTicker;
            ToTicker = toTicker;
        }

        public string FromTicker { get; set; }
        public string ToTicker { get; set; }
    }
}