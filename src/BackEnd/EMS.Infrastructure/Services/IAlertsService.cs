using EMS.Domain.Dtos;

namespace EMS.Infrastructure.Services
{
    public interface IAlertsService
    {
        List<Alert> GetAlerts();
    }
}