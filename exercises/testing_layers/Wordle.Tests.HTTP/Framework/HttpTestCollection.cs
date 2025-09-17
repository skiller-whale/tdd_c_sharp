using Xunit;

namespace Wordle.Tests.HTTP.Framework;

[CollectionDefinition("HttpTest")]
public class HttpTestCollection : ICollectionFixture<HttpTestFixture>
{
}
