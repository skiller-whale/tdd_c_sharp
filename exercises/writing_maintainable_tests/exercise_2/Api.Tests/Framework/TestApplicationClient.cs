using Xunit;

namespace Api.Tests.Framework;

[CollectionDefinition("ApiTest")]
public class ApiTestCollection : ICollectionFixture<ApiTestFixture>
{
}
