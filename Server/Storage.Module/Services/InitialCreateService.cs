using Storage.Module.Entities;
using Storage.Module.Services.Interfaces;
using Storage.Module.StaticClasses;
using System.Threading.Tasks;

namespace Storage.Module.Services
{
    public class InitialCreateService : IInitialCreateService
    {
        private readonly DataContext _dataContext;
        public InitialCreateService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InitialCreateValuesAsync()
        {
            if (_dataContext.Database.EnsureCreated())
            {
                // базовые настройки
                Settings binanceApiKey = new ()
                {
                    Key = SettingsKeys.BinanceApiKey,
                    Value = null
                };
                _dataContext.Add(binanceApiKey);

                Settings binanceApiSecret = new ()
                {
                    Key = SettingsKeys.BinanceApiSecret,
                    Value = null
                };
                _dataContext.Add(binanceApiSecret);

                Exchange exchangeBinanceSpot = new()
                {
                    Name = ExchangeNames.BinanceSpot
                };
                _dataContext.Add(exchangeBinanceSpot);

                Exchange exchangeBinanceFutures = new()
                {
                    Name = ExchangeNames.BinanceFutures
                };
                _dataContext.Add(exchangeBinanceFutures);

                User admin = new()
                {
                    UserName = "admin",
                    Password = "admin"
                };
                _dataContext.Add(admin);

                await _dataContext.SaveChangesAsync();
            }
        }
    }
}