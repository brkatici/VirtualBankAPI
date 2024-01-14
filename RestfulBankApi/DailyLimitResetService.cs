using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestfulBankApi
{
    public class DailyLimitResetService : BackgroundService
    {
        private readonly ILogger<DailyLimitResetService> _logger;
        private readonly IServiceProvider _services;

        public DailyLimitResetService(ILogger<DailyLimitResetService> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Daily limit reset service is running at: {time}", DateTimeOffset.Now);

                using (var scope = _services.CreateScope())
                {
                    var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                    await accountService.ResetDailyLimits();
                }

                // Her gün gece 00:00'da çalışacak şekilde ayarlandı
                var now = DateTimeOffset.Now;
                var nextMidnight = now.Date.AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);
                var delay = nextMidnight - now;

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
