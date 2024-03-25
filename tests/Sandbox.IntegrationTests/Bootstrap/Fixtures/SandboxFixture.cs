using Sandbox.IntegrationTests.Bootstrap.Containers;

namespace Sandbox.IntegrationTests.Bootstrap.Fixtures;

public class SandboxFixture
    : IDisposable
{
    private readonly Testcontainers _containers = new();

    public SandboxFixture()
    {
        this._containers.InitializeAsync().Wait();
    }

    #region IDisposable

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed) return;

        if (disposing)
        {
            this._containers.DisposeAsync().Wait();
        }

        this.disposed = true;
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}

[CollectionDefinition(nameof(SandboxCollection))]
public class SandboxCollection
    : ICollectionFixture<SandboxFixture>
{ }
