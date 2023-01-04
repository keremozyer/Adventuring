using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.IEnumerableExtensionTests;

public class ConcatSafeTests
{
    [Test]
    public void ConcatSafe_NullFirstCollection()
    {
        IEnumerable<string> list1 = null;
        IEnumerable<string> list2 = new List<string>() { "a" };

        IEnumerable<string> result = list1.ConcatSafe(list2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First, Is.EqualTo(list2.First()));
    }

    [Test]
    public void ConcatSafe_NullSecondCollection()
    {
        IEnumerable<string> list1 = new List<string>() { "a" };
        IEnumerable<string> list2 = null;

        IEnumerable<string> result = list1.ConcatSafe(list2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First, Is.EqualTo(list1.First()));
    }

    [Test]
    public void ConcatSafe_BothCollectionsNull()
    {
        IEnumerable<string> list1 = null;
        IEnumerable<string> list2 = null;

        Assert.That(list1.ConcatSafe(list2), Is.Null);
    }
}