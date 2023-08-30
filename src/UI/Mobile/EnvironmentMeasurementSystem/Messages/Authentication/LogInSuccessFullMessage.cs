using System.IdentityModel.Tokens.Jwt;

namespace EnvironmentMeasurementSystem.Messages.Authentication;
public class LogInSuccessFullMessage : ValueChangedMessage<JwtSecurityToken>
{
    public LogInSuccessFullMessage(JwtSecurityToken value) : base(value)
    {
    }
}
