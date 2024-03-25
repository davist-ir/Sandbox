
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Sandbox.Api.Testcontainers;
using Sandbox.IntegrationTests.Bootstrap.Images;

namespace Sandbox.IntegrationTests.Bootstrap.Containers;

internal class Testcontainers
    : IAsyncLifetime
{
    private static readonly SandboxApiImage SandboxApiImage = new();

    private readonly INetwork _network;
    private readonly SandboxApiContainer _sandboxApiContainer;

    public Testcontainers()
    {
        this._network = new NetworkBuilder()
            .Build();

        this._sandboxApiContainer = new SandboxApiBuilder()
            .WithImage(SandboxApiImage)
            .WithNetwork(this._network)
            .Build();
    }

    public async Task DisposeAsync()
    {
        await this._sandboxApiContainer.DisposeAsync().AsTask();
        await this._network.DisposeAsync().AsTask();
    }

    public async Task InitializeAsync()
    {
        await SandboxApiImage.InitializeAsync();
        await this._network.CreateAsync();
        await this._sandboxApiContainer.StartAsync();
    }
}
