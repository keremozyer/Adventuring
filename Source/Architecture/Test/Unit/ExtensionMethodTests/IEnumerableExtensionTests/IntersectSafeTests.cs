using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.IEnumerableExtensionTests;

public class IntersectSafeTests
{
    [Test]
    public void IntersectSafe_NullFirstCollection()
    {
        IEnumerable<string> list1 = null;
        IEnumerable<string> list2 = new List<string>() { "a" };

        Assert.That(list1.IntersectSafe(list2), Is.Null);
    }

    [Test]
    public void IntersectSafe_NullSecondCollection()
    {
        IEnumerable<string> list1 = new List<string>() { "a" };
        IEnumerable<string> list2 = null;

        Assert.That(list1.IntersectSafe(list2), Is.Null);
    }

    [Test]
    public void IntersectSafe_BothCollectionNull()
    {
        IEnumerable<string> list1 = null;
        IEnumerable<string> list2 = null;

        Assert.That(list1.IntersectSafe(list2), Is.Null);
    }
}