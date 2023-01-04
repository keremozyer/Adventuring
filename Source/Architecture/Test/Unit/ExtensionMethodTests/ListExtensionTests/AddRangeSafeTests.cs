using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.ListExtensionTests;

public class AddRangeSafeTests
{
    [Test]
    public void AddSafe_Valid_SourceCollection_Null_ElementsToAdd()
    {
        List<string> list = new() { "a" };
        list = list.AddRangeSafe(null);

        Assert.That(list, Has.Count.EqualTo(1));
        Assert.That(list.First(), Is.EqualTo("a"));
    }

    [Test]
    public void AddSafe_Null_SourceCollection_Null_ElementsToAdd()
    {
        List<string> list = null;
        list = list.AddRangeSafe(null);

        Assert.That(list, Is.Null);
    }

    [Test]
    public void AddSafe_Null_SourceCollection_Valid_ElementsToAdd()
    {
        List<string> list = null;
        list = list.AddRangeSafe(new string[] { "a", "b" });

        Assert.That(list, Is.Not.Null);
        Assert.That(list, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(list[0], Is.EqualTo("a"));
            Assert.That(list[1], Is.EqualTo("b"));
        });
    }
}