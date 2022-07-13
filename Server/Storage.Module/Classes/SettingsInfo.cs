using Storage.Module.Entities;
using Storage.Module.StaticClasses;
using System.Collections.Generic;
using System.Linq;

namespace Storage.Module.Classes
{
    public class SettingsInfo
    {
        public string BinanceApiKey { get; set; }
        public string BinanceApiSecret { get; set; }

        public (bool IsValid, string ValidError) IsValid()
        {
            if (string.IsNullOrEmpty(BinanceApiKey))
            {
                return (false, "BinanceApiKey не задан");
            }

            if (string.IsNullOrEmpty(BinanceApiSecret))
            {
                return (false, "BinanceApiSecret не задан");
            }

            return (true, null);
        }

        public void SetFieldsBySettings(List<Settings> settings)
        {
            foreach(Settings item in settings)
            {
                switch (item.Key)
                {
                    case SettingsKeys.BinanceApiKey:
                        {
                            BinanceApiKey = item.Value;
                            break;
                        }
                    case SettingsKeys.BinanceApiSecret:
                        {
                            BinanceApiSecret = item.Value;
                            break;
                        }
                }
            }
        }
    }
}