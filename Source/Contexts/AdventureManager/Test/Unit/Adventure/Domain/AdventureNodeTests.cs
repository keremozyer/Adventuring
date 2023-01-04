using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;

namespace Adventuring.Contexts.AdventureManager.Test.Unit.Adventure.Domain;

public class AdventureNodeTests : BaseAdventureTestFixture
{
    [Test]
    public void AdventureNode_Create_Successful()
    {
        string nodeMessage = "aa";

        AdventureNode adventureNode = new(nodeMessage);

        Assert.That(adventureNode.NodeMessage, Is.EqualTo(nodeMessage));
    }

    [Test]
    public void AdventureNode_Create_NodeMessage_Null()
    {
        Assert.That(() => new AdventureNode(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AdventureNode.NodeMessage)));
    }

    [Test]
    public void AdventureNode_Create_NodeMessage_Empty()
    {
        Assert.That(() => new AdventureNode(""),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AdventureNode.NodeMessage)));
    }

    [Test]
    public void AdventureNode_Create_NodeMessage_Whitespace()
    {
        Assert.That(() => new AdventureNode("  "),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AdventureNode.NodeMessage)));
    }

    [Test]
    public void AdventureNode_AddPositiveAnswerNode()
    {
        AdventureNode node = new("aa");
        node.AddPositiveAnswerNode("bb");

        Assert.That(node.PositiveAnswerNode, Is.Not.Null);
        Assert.That(node.PositiveAnswerNode.NodeMessage, Is.EqualTo("bb"));
    }

    [Test]
    public void AdventureNode_AddNegativeAnswerNode()
    {
        AdventureNode node = new("aa");
        node.AddNegativeAnswerNode("bb");

        Assert.That(node.NegativeAnswerNode, Is.Not.Null);
        Assert.That(node.NegativeAnswerNode.NodeMessage, Is.EqualTo("bb"));
    }

    [Test]
    public void AdventureNode_Validate_Balanced()
    {
        AdventureNode node = new("start");
        node.AddPositiveAnswerNode("start->positive");
        node.AddNegativeAnswerNode("start->negative");

        node.PositiveAnswerNode.AddPositiveAnswerNode("start->positive->positive");
        node.PositiveAnswerNode.AddNegativeAnswerNode("start->positive->negative");

        ValidationException result = node.Validate();

        Assert.That(result, Is.Null);
    }

    [Test]
    public void AdventureNode_Validate_Unbalanced_EndNode()
    {
        AdventureNode node = new("start");
        node.AddPositiveAnswerNode("start->positive");
        node.AddNegativeAnswerNode("start->negative");

        node.PositiveAnswerNode.AddPositiveAnswerNode("start->positive->positive");
        node.PositiveAnswerNode.AddNegativeAnswerNode("start->positive->negative");

        node.PositiveAnswerNode.PositiveAnswerNode.AddPositiveAnswerNode("start->positive->positive->positive");

        ValidationException result = node.Validate();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Messages, Is.Not.Null);
        Assert.That(result.Messages.Any(m => m.Code == ErrorCodes.NodeMustBeBalanced), Is.EqualTo(true));
    }

    [Test]
    public void AdventureNode_Validate_Unbalanced_MiddleNode()
    {
        AdventureNode node = new("start");
        node.AddPositiveAnswerNode("start->positive");
        node.AddNegativeAnswerNode("start->negative");

        node.PositiveAnswerNode.AddPositiveAnswerNode("start->positive->positive");

        node.PositiveAnswerNode.PositiveAnswerNode.AddPositiveAnswerNode("start->positive->positive->positive");
        node.PositiveAnswerNode.PositiveAnswerNode.AddNegativeAnswerNode("start->positive->positive->negative");

        ValidationException result = node.Validate();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Messages, Is.Not.Null);
        Assert.That(result.Messages.Any(m => m.Code == ErrorCodes.NodeMustBeBalanced), Is.EqualTo(true));
    }

    [Test]
    public void AdventureNode_Validate_Unbalanced_FirstNode()
    {
        AdventureNode node = new("start");
        node.AddPositiveAnswerNode("start->positive");

        ValidationException result = node.Validate();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Messages, Is.Not.Null);
        Assert.That(result.Messages.Any(m => m.Code == ErrorCodes.NodeMustBeBalanced), Is.EqualTo(true));
    }
}