using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvironmentMeasurementSystem.Application.Interfaces;

namespace EnvironmentMeasurementSystem.Services.Authentication;
public interface IAuthenticationService : IService
{
    AuthenticationResult AuthenticationResult { get; }
    Task<AuthenticationResult> SignInInteractively(CancellationToken cancellationToken);
    Task<AuthenticationResult> AcquireTokenSilent(CancellationToken cancellationToken);
    Task LogoutAsync(CancellationToken cancellationToken);
}
