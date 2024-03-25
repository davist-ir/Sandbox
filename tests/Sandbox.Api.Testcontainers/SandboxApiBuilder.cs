using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;

namespace Sandbox.Api.Testcontainers;

public class SandboxApiBuilder
    : ContainerBuilder<SandboxApiBuilder, SandboxApiContainer, SandboxApiConfiguration>
{
    public const string SandboxApiImage = "ghcr.io/davist-ir/sandbox.api:0.1.0";

    public const ushort SandboxApiHttpPort = 80;
    public const ushort SandboxApiHttpsPort = 443;

    /// <summary>
    /// Initializes a new instance of the <see cref="SandboxApiBuilder" /> class.
    /// </summary>
    public SandboxApiBuilder()
        : this(new())
    {
        this.DockerResourceConfiguration = this.Init().DockerResourceConfiguration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SandboxApiBuilder" /> class.
    /// </summary>
    /// <param name="configuration">The Docker resource configuration.</param>
    public SandboxApiBuilder(SandboxApiConfiguration configuration)
        : base(configuration)
    {
        this.DockerResourceConfiguration = configuration;
    }

    /// <inheritdoc />
    public override SandboxApiContainer Build()
    {
        this.Validate();

        return new(this.DockerResourceConfiguration, TestcontainersSettings.Logger);
    }

    /// <inheritdoc />
    protected override SandboxApiConfiguration DockerResourceConfiguration { get; }

    /// <inheritdoc />
    protected override SandboxApiBuilder Init()
    {
        return base.Init()
            .WithImage(SandboxApiImage)
            .WithPortBinding(SandboxApiHttpPort, true)
            .WithPortBinding(SandboxApiHttpsPort, true);
    }

    /// <inheritdoc />
    protected override SandboxApiBuilder Clone(IContainerConfiguration resourceConfiguration)
        => this.Merge(this.DockerResourceConfiguration, new(resourceConfiguration));

    /// <inheritdoc />
    protected override SandboxApiBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        => this.Merge(this.DockerResourceConfiguration, new(resourceConfiguration));

    /// <inheritdoc />
    protected override SandboxApiBuilder Merge(SandboxApiConfiguration oldValue, 
        SandboxApiConfiguration newValue)
        => new(new(oldValue, newValue));
}
