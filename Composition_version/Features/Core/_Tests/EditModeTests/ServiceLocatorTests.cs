using MC.Core;
using NUnit.Framework;

[TestFixture]
public class ServiceLocatorTests
{
    [SetUp]
    public void Setup()
    {
        ServiceLocator.Clear();
    }

    public interface ITestService { }
    public class TestService : ITestService { }

    [Test]
    public void WhenServiceIsRegisteredWithInterface_ItCanBeRetrievedByInterface()
    {
        var testServiceInstance = new TestService();

        ServiceLocator.Register<ITestService>(testServiceInstance);

        var retrievedService = ServiceLocator.Get<ITestService>();


        Assert.IsNotNull(retrievedService);
        Assert.That(retrievedService, Is.SameAs(testServiceInstance));

        Assert.Throws<System.Collections.Generic.KeyNotFoundException>(
            () => ServiceLocator.Get<TestService>());
    }
}