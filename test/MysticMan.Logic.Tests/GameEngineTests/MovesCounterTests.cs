using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MysticMan.Logic.Internals;

namespace MysticMan.Logic.Tests.GameEngineTests {
  [TestClass]
  public class MovesCounterTests {
    private Mock<IGameConfiguration> _configurationMock;
    private Mock<IRandomizer> _randomizerMock;
    private GameEngineOptions _gameEngineOptions;

    [TestInitialize]
    public void TestInitialize() {
      _configurationMock = new Mock<IGameConfiguration>(MockBehavior.Strict);
      _configurationMock.SetupGet(_ => _.MapSize).Returns(new MapSize(new Size(3, 3)));
      _configurationMock.SetupGet(_ => _.Moves).Returns(() => 2);
      _configurationMock.SetupGet(_ => _.CanReachBorder).Returns(() => true);
      _randomizerMock = new Mock<IRandomizer>(MockBehavior.Strict);
      _randomizerMock.Setup(_ => _.GetRandomPosition(It.IsAny<Size>())).Returns(new Position(1, 1));

      _gameEngineOptions = new GameEngineOptions {
        Configuration = _configurationMock.Object,
        Randomizer = _randomizerMock.Object
      };

    }

    [TestMethod]
    public void MovesCountUntilZero() {

      GameEngine engine = new GameEngine(_gameEngineOptions);
      engine.Initialize();
      engine.Start();

      Assert.AreEqual(2, engine.MovesLeft);

      engine.MoveLeft();
      Assert.AreEqual(1, engine.MovesLeft);
      engine.MoveLeft();
      Assert.AreEqual(0, engine.MovesLeft);
      Assert.AreEqual(GameEngineState.WaitingForResolving, engine.State);
      engine.MoveLeft();
      Assert.AreEqual(0, engine.MovesLeft);
    }


    [TestMethod]
    public void MovesToBorderShouldDecrementMovesCounter() {

      GameEngine engine = new GameEngine(_gameEngineOptions);
      engine.Initialize();
      engine.Start();
      Assert.AreEqual("B2", engine.CurrentPosition);
      Assert.AreEqual(2, engine.MovesLeft);
      engine.MoveLeft();
      Assert.AreEqual(1, engine.MovesLeft);
      engine.MoveLeft();
      Assert.AreEqual(0, engine.MovesLeft);
    }

    [TestMethod]
    public void MovesToBorderShouldNotDecrementMovesCounter() {
      _configurationMock.SetupGet(_ => _.CanReachBorder).Returns(false);
      GameEngine engine = new GameEngine(_gameEngineOptions);
      engine.Initialize();
      engine.Start();
      Assert.AreEqual("B2", engine.CurrentPosition);
      Assert.AreEqual(2, engine.MovesLeft);
      engine.MoveLeft();
      Assert.AreEqual(1, engine.MovesLeft);
      engine.MoveLeft();
      Assert.AreEqual(1, engine.MovesLeft);
      engine.MoveUp();
      Assert.AreEqual(0, engine.MovesLeft);
    }
  }
}
