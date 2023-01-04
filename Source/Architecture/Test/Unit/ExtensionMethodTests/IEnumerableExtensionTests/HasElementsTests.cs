using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.IEnumerableExtensionTests;

public class HasElementsTests
{
    [Test]
    public void HasElements_True()
    {
        IEnumerable<string> list = new List<string>() { "a" };

        Assert.That(list.HasElements(), Is.True);
    }

    [Test]
    public void HasElements_Collection_Empty()
    {
        IEnumerable<string> list = new List<string>();

        Assert.That(list.HasElements(), Is.False);
    }

    [Test]
    public void HasElements_Collection_Null()
    {
        IEnumerable<string> list = null;

        Assert.That(list.HasElements(), Is.False);
    }
}