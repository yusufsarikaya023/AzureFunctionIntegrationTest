using Test.IntegrationTest;

namespace Test;

[CollectionDefinition("Factory collection")]

public record FactoryCollection: ICollectionFixture<FunctionApplicationStartup>;