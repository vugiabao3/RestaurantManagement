using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Analytics.Queries.GetKitchenAnalytics
{
    public class GetKitchenAnalyticsHandler : IRequestHandler<GetKitchenAnalyticsQuery, KitchenAnalyticsResponse>
    {
        private readonly IKitchenAnalyticsRepository _repository;
        private readonly ICacheService _cache;

        public GetKitchenAnalyticsHandler(IKitchenAnalyticsRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public Task<KitchenAnalyticsResponse> Handle(GetKitchenAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var rawTo = request.To ?? DateTime.Now;
            var rawFrom = request.From ?? rawTo.AddDays(-7);

            var to = NormalizeToDatabaseTime(rawTo);
            var from = NormalizeToDatabaseTime(rawFrom);

            if (from > to)
                throw new BusinessException("Thoi gian bat dau khong duoc lon hon thoi gian ket thuc");

            var key = $"analytics:{from:O}:{to:O}:{request.KitchenAreaId}";
            return _cache.GetOrCreateAsync(
                key,
                () => _repository.GetSummaryAsync(from, to, request.KitchenAreaId),
                TimeSpan.FromSeconds(30));
        }

        private static DateTime NormalizeToDatabaseTime(DateTime value)
        {
            return value.Kind == DateTimeKind.Utc
                ? value.ToLocalTime()
                : value;
        }
    }
}
