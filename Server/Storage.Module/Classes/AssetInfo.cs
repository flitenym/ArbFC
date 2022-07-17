namespace Storage.Module.Classes
{
    public record AssetInfo
    {
        public AssetInfo(string fromAsset, string toAsset)
        {
            FromAsset = fromAsset;
            ToAsset = toAsset;
        }

        public string FromAsset { get; set; }
        public string ToAsset { get; set; }
    }
}