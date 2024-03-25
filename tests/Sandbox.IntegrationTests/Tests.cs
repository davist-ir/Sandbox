using Sandbox.IntegrationTests.Bootstrap.Fixtures;

namespace Sandbox.IntegrationTests;

[Collection(nameof(SandboxCollection))]
public class Tests
{
    private readonly SandboxFixture _fixture;

    public Tests(SandboxFixture fixture)
    {
        this._fixture = fixture;
    }

    [Fact]
    public void Test()
    {
        Assert.True(true);
    }
}
