using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Module.Classes;
using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using Storage.Module.Repositories.Base.Interfaces;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Repositories
{
    public class ChainRepository : IChainRepository
    {
        private readonly DataContext _dataContext;
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<ChainRepository> _logger;
        public ChainRepository(DataContext dataContext, IBaseRepository baseRepository, ILogger<ChainRepository> logger)
        {
            _dataContext = dataContext;
            _baseRepository = baseRepository;
            _logger = logger;
        }

        #region Controller methods

        public IEnumerable<Chain> Get()
        {
            return _dataContext
            .Chains
            .Include(x => x.ExchangeChains)
                .ThenInclude(x => x.Exchange)
            .Include(x => x.Tickers)
            .Include(x => x.User)
            .Include(x => x.NotificationSound)
            .OrderBy(x => x.Id);
        }

        public async Task<Chain> GetByIdAsync(long id)
        {
            return await _dataContext
            .Chains
            .Include(x => x.ExchangeChains)
                .ThenInclude(x => x.Exchange)
            .Include(x => x.Tickers)
            .Include(x => x.NotificationSound)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Chain>> GetByUserIdAsync(long userId)
        {
            List<long> chainIds =
                await _dataContext
                .Chains
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .Select(x => x.Id)
                .ToListAsync();

            return _dataContext
                .Chains
                .Include(x => x.ExchangeChains)
                    .ThenInclude(x => x.Exchange)
                .Include(x => x.Tickers)
                .Include(x => x.NotificationSound)
                .Where(x => chainIds.Contains(x.Id))
                .OrderBy(x => x.Id);
        }

        public async Task<(bool IsSuccess, string Message)> CreateAsync(ChainDTO chainDTO)
        {
            Chain chain = new();

            // установим цвет
            chain.SRGB = chainDTO.SRGB;
            chain.Difference = chainDTO.Difference;
            chain.TwentyFourHoursVolume = chainDTO.TwentyFourHoursVolume;
            chain.IsEnabled = chainDTO.IsEnabled;

            // получим пользователя
            User user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == chainDTO.UserId);
            if (user == null)
            {
                return (false, $"Не найден пользователь {chainDTO.UserId}.");
            }

            chain.User = user;
            _dataContext.Attach(user);

            // получим звук уведомления 
            NotificationSound notificationSound = await _dataContext.NotificationSounds.FirstOrDefaultAsync(x => x.Id == chainDTO.NotificationSoundId);
            if (notificationSound == null)
            {
                return (false, $"Не найден звук уведомления {chainDTO.NotificationSoundId}.");
            }

            chain.NotificationSound = notificationSound;

            // получим список из exchange
            List<ExchangeChain> exchangeChains = new();

            for (int i = 0; i < chainDTO.ExchangeIds.Count(); i++)
            {
                ExchangeChain exchangeChain = new();

                Exchange exchange = await _dataContext.Exchanges.FirstOrDefaultAsync(x => x.Id == chainDTO.ExchangeIds.ElementAt(i));
                if (notificationSound == null)
                {
                    return (false, $"Не найдена биржа {chainDTO.ExchangeIds.ElementAt(i)}.");
                }

                exchangeChain.Order = i;
                exchangeChain.Exchange = exchange;
                _dataContext.Attach(exchange);

                exchangeChains.Add(exchangeChain);
            }

            // создадим выбранные тикеры
            List<Ticker> tickers = new();

            foreach (TickerInfo ticker in chainDTO.Tickers)
            {
                Ticker newTicker = new();

                newTicker.ToTicker = ticker.ToTicker;
                newTicker.FromTicker = ticker.FromTicker;

                tickers.Add(newTicker);
            }


            if (exchangeChains.Any())
            {
                chain.ExchangeChains = exchangeChains;
                _dataContext.AddRange(exchangeChains);
            }

            if (tickers.Any())
            {
                chain.Tickers = tickers;
                _dataContext.AddRange(tickers);
            }

            _dataContext.Add(chain);

            return await SaveChangesAsync();
        }

        public Task<(bool IsSuccess, string Message)> SaveChangesAsync()
        {
            return _baseRepository.SaveChangesAsync();
        }

        #endregion
    }
}