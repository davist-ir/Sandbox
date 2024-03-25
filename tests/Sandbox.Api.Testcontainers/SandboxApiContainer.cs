using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace Sandbox.Api.Testcontainers;

public class SandboxApiContainer
    : DockerContainer
{
    private readonly SandboxApiConfiguration _configuration;

    public SandboxApiContainer(SandboxApiConfiguration configuration,
        ILogger logger)
        : base(configuration, logger) 
    {
        this._configuration = configuration;
    }
}
