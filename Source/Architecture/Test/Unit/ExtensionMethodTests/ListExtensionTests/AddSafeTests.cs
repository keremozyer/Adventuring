using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.ListExtensionTests;

public class AddSafeTests
{
    [Test]
    public void AddSafe_Valid_Collection_Null_Element()
    {
        string existingValue = "a";
        List<string> list = new() { existingValue };
        list = list.AddSafe(null);

        Assert.That(list, Has.Count.EqualTo(1));
        Assert.That(list.First(), Is.EqualTo(existingValue));
    }

    [Test]
    public void AddSafe_Null_Collection_Null_Element()
    {
        List<string> list = null;
        list = list.AddSafe(null);

        Assert.That(list, Is.Null);
    }

    [Test]
    public void AddSafe_Null_Collection_Valid_Element()
    {
        List<string> list = null;
        list = list.AddSafe("a");

        Assert.That(list, Is.Not.Null);
        Assert.That(list, Has.Count.EqualTo(1));
        Assert.That(list.First(), Is.EqualTo("a"));
    }
}