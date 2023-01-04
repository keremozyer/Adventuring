using Adventuring.Architecture.Concern.Extension;

namespace Adventuring.Architecture.Test.Unit.ExtensionMethodTests.ICollectionExtensionTests;

public class RemoveByTests
{
    class TestElementClass
    {
        public string TestProperty { get; set; }

        public TestElementClass(string testProperty)
        {
            this.TestProperty = testProperty;
        }
    }

    [Test]
    public void RemoveBy_ElementExists()
    {
        string propertyValue = "a";
        ICollection<TestElementClass> list = new List<TestElementClass>()
        {
            new(propertyValue)
        };

        list.RemoveBy(e => e.TestProperty == propertyValue);

        Assert.That(!list.Any(l => l.TestProperty == propertyValue));
    }

    [Test]
    public void RemoveBy_ElementNotExists()
    {
        string propertyValue = "a";
        ICollection<TestElementClass> list = new List<TestElementClass>()
        {
            new(propertyValue)
        };

        list.RemoveBy(e => e.TestProperty == propertyValue + propertyValue);

        Assert.That(list.Any(l => l.TestProperty == propertyValue));
    }
}