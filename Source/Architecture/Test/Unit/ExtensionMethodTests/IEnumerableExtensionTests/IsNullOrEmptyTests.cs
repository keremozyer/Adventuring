using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.IEnumerableExtensionTests;

public class IsNullOrEmptyTests
{
    [Test]
    public void IsNullOrEmpty_Collection_Empty()
    {
        IEnumerable<string> list = new List<string>();

        Assert.That(list.IsNullOrEmpty(), Is.True);
    }

    [Test]
    public void IsNullOrEmpty_Collection_Null()
    {
        IEnumerable<string> list = null;

        Assert.That(list.IsNullOrEmpty(), Is.True);
    }

    [Test]
    public void IsNullOrEmpty_Collection_WithElements()
    {
        IEnumerable<string> list = new List<string>() { "a" };

        Assert.That(list.IsNullOrEmpty(), Is.False);
    }
}