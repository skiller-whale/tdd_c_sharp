using Xunit;

namespace Wordle.Tests.Web.Framework;

[CollectionDefinition("WebTest")]
public class WebTestCollection : ICollectionFixture<RealServerTestFixture>
{
}
