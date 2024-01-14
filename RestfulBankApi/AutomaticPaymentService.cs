using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace RestfulBankApi
{
    public class AutomaticPaymentService : BackgroundService
    {
        private readonly ILogger<AutomaticPaymentService> _logger;
        private readonly IServiceProvider _services;

        public AutomaticPaymentService(ILogger<AutomaticPaymentService> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Automatic payment service is running at: {time}", DateTimeOffset.Now);

                using (var scope = _services.CreateScope())
                {
                    var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                    // Burada otomatik ödeme işlemlerini yapabilirsiniz
                    await scopedProcessingService.ProcessPayments();
                }

                await Task.Delay(TimeSpan.FromDays(30), stoppingToken); // Her saatte bir çalışacak şekilde ayarlandı, istediğiniz aralığı verebilirsiniz
            }
        }
    }

}
