using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;

namespace Adventuring.Contexts.AdventureManager.Test.Unit._Game.Domain;

public class NodeAnswerTests : BaseGameTestFixture
{
    [Test]
    public void NodeAnswer_Create_Success()
    {
        bool choosenPath = true;

        NodeAnswer nodeAnswer = new(choosenPath);

        Assert.That(nodeAnswer.ChoosenPath, Is.EqualTo(choosenPath));
    }
}