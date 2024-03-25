using Docker.DotNet.Models;
using DotNet.Testcontainers.Configurations;

namespace Sandbox.Api.Testcontainers;

public class SandboxApiConfiguration
    : ContainerConfiguration
{
    public SandboxApiConfiguration()
        : base()
    { }

    public SandboxApiConfiguration(IResourceConfiguration<CreateContainerParameters> configuration)
        : base(configuration)
    { }

    public SandboxApiConfiguration(IContainerConfiguration configuration)
        : base(configuration)
    { }

    public SandboxApiConfiguration(SandboxApiConfiguration oldValue,
        SandboxApiConfiguration newValue)
        : base(oldValue, newValue)
    { }
}
