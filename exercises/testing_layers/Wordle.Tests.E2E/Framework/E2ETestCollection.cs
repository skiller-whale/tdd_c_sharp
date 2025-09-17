using Xunit;

namespace Wordle.Tests.E2E.Framework;

[CollectionDefinition("E2ETest")]
public class E2ETestCollection : ICollectionFixture<RealServerTestFixture>
{
}
