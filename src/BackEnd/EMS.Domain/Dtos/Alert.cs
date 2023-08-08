namespace EMS.Domain.Dtos;

public class Alert
{
    public string RuleName { get; set; }
    public string Severity { get; set; }
    public int Count { get; set; }
    public string Explanation { get; set; }
}