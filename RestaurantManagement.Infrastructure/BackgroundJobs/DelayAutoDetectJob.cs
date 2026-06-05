using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantManagement.Application.Interfaces.Kitchen;

namespace RestaurantManagement.Infrastructure.BackgroundJobs;

public class DelayAutoDetectJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DelayAutoDetectJob> _logger;

    public DelayAutoDetectJob(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<DelayAutoDetectJob> logger)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var enabled = _configuration.GetValue<bool>("DelayAutoDetect:Enabled");
        if (!enabled) return;

        var seconds = Math.Max(10, _configuration.GetValue<int>("DelayAutoDetect:IntervalSeconds", 30));
        var multiplier = _configuration.GetValue<decimal>("DelayAutoDetect:ThresholdMultiplier", 1.5m);
        var systemUserId = _configuration.GetValue<int>("DelayAutoDetect:SystemUserId", 1);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IKitchenTrackingService>();
                var result = await service.TuDongPhatHienMonChamAsync(multiplier, systemUserId);
                if (result.SoMonDuocPhatHien > 0)
                    _logger.LogInformation("Tự động phát hiện {Count} món chậm.", result.SoMonDuocPhatHien);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tự động phát hiện món chậm.");
            }

            await Task.Delay(TimeSpan.FromSeconds(seconds), stoppingToken);
        }
    }
}
