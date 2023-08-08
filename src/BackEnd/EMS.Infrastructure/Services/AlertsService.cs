using EMS.Domain.Dtos;

namespace EMS.Infrastructure.Services;

public class AlertsService : IAlertsService
{
    public List<Alert> GetAlerts()
    {
        var randomNumOfMarkers = new Random();
        int num = randomNumOfMarkers.Next(20);

        var response = new List<Alert>();

        for (int i = 0; i < num; i++)
        {
            response.AddRange(new Alert[] {
                new Alert()
                {
                    RuleName = "Chiller pressure too high",
                    Severity = "Critical",
                    Count = i,
                    Explanation = $"Random explanation {i}"
                },
                new Alert()
                {
                    RuleName = "Elevator vibration stopped",
                    Severity = "Warning",
                    Count = i,
                    Explanation = $"Random explanation {i}"
                },
                new Alert()
                {
                    RuleName = "High than normal cargo",
                    Severity = "Critical",
                    Count = i,
                    Explanation = $"Random explanation {i}"
                } });
        }
        return response;
    }
}