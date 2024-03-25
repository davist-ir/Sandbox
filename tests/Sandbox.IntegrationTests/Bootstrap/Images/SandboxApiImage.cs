using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;

namespace Sandbox.IntegrationTests.Bootstrap.Images;

internal class SandboxApiImage
    : IImage,
    IAsyncLifetime
{
    public string Repository
        => this._image.Repository;

    public string Name
        => this._image.Name;

    public string Tag
        => this._image.Tag;

    public string FullName
    => this._image.FullName;

    private readonly IImage _image = new DockerImage("ghcr.io/davist-ir", "sandbox.api", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());

    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public Task DisposeAsync()
        => Task.CompletedTask;

    public string GetHostname()
        => this._image.GetHostname();

    public async Task InitializeAsync()
    {
        await this._semaphoreSlim.WaitAsync();

        try
        {
            await new ImageFromDockerfileBuilder()
                .WithName(this)
                .WithDeleteIfExists(false)
                .WithDockerfile("src/Sandbox.Api/Dockerfile")
                .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), string.Empty)
                .WithBuildArgument("RESOURCE_REAPER_SESSION_ID", ResourceReaper.DefaultSessionId.ToString("D"))
                .Build()
                .CreateAsync();
        }
        finally
        {
            this._semaphoreSlim.Release();
        }
    }
}