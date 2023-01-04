using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;

namespace Adventuring.Contexts.AdventureManager.Test.Unit.Adventure.Domain;

public class AdventureTreeTests : BaseAdventureTestFixture
{
    [Test]
    public void AdventureTree_Create_Successful()
    {
        string adventureName = "aa";
        string firstQuestion = "bb";

        AdventureTree adventureTree = new(adventureName, firstQuestion);

        Assert.That(adventureTree.AdventureName, Is.EqualTo(adventureName));
        Assert.That(adventureTree.StartingNode, Is.Not.Null);
        Assert.That(adventureTree.StartingNode.NodeMessage, Is.EqualTo(firstQuestion));
    }

    [Test]
    public void AdventureTree_Create_AdventureName_Null()
    {
        Assert.That(() => new AdventureTree(null, "aa"),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AdventureTree.AdventureName)));
    }

    [Test]
    public void AdventureTree_Create_AdventureName_Empty()
    {
        Assert.That(() => new AdventureTree("", "aa"),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AdventureTree.AdventureName)));
    }

    [Test]
    public void AdventureTree_Create_AdventureName_Whitespace()
    {
        Assert.That(() => new AdventureTree("   ", "aa"),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AdventureTree.AdventureName)));
    }

    [Test]
    public void AdventureTree_GetTargetNodeMessage_OneLayer()
    {
        (string message, bool isEndNode) = base.ThreeWithOneLayer.GetTargetNodeMessage(new bool[] { true });

        Assert.That(message, Is.EqualTo("positive"));
        Assert.That(isEndNode, Is.EqualTo(true));
    }

    [Test]
    public void AdventureTree_GetTargetNodeMessage_StartingLayer()
    {
        (string message, bool isEndNode) = base.ThreeWithThreeLayer.GetTargetNodeMessage(new bool[] { true });

        Assert.That(message, Is.EqualTo("positive"));
        Assert.That(isEndNode, Is.EqualTo(false));
    }

    [Test]
    public void AdventureTree_GetTargetNodeMessage_MiddleLayer()
    {
        (string message, bool isEndNode) = base.ThreeWithThreeLayer.GetTargetNodeMessage(new bool[] { true, false });

        Assert.That(message, Is.EqualTo("positive->negative"));
        Assert.That(isEndNode, Is.EqualTo(false));
    }

    [Test]
    public void AdventureTree_GetTargetNodeMessage_EndLayer()
    {
        (string message, bool isEndNode) = base.ThreeWithThreeLayer.GetTargetNodeMessage(new bool[] { true, false, true });

        Assert.That(message, Is.EqualTo("positive->negative->positive"));
        Assert.That(isEndNode, Is.EqualTo(true));
    }
}