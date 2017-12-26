using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MysticMan.Logic.Internals;

namespace MysticMan.Logic.Tests {
  [TestClass]
  public class GameEngineInitializationTests {
    private GameEngineOptions _options;
    private Mock<IRandomizer> _randomizerMock;
    private Mock<IGameConfiguration> _configurationMock;

    [TestInitialize]
    public void TestInitialize() {
      _randomizerMock = new Mock<IRandomizer>(MockBehavior.Strict);
      _configurationMock = new Mock<IGameConfiguration>(MockBehavior.Strict);
      _options = new GameEngineOptions {
        Randomizer = _randomizerMock.Object,
        Configuration = _configurationMock.Object
      };
      _configurationMock.SetupGet(_ => _.MapSize).Returns(new MapSize(new Size(10, 12)));
      _configurationMock.SetupGet(_ => _.Moves).Returns(5);
      _configurationMock.SetupGet(_ => _.Level).Returns(1);
      _configurationMock.SetupGet(_ => _.CanReachBorder).Returns(true);
      _randomizerMock.Setup(_ => _.GetRandomPosition(It.IsAny<Size>())).Returns(new Position(5, 5));
    }

    [TestMethod]
    public void MapAndManAreInitialized() {
      GameEngine engine = new GameEngine(_options);
      engine.Initialize();
      engine.Start();
      Assert.IsNotNull(engine.Map);
      Assert.AreEqual("A1", engine.Map.GetPosition(0, 0));
      Assert.AreEqual("E5", engine.Map.GetPosition(4, 4));
      Assert.AreEqual("F6", engine.Man.Position);

    }

    [TestMethod]
    public void MoveInsideTheMap() {
      _randomizerMock.Setup(_ => _.GetRandomPosition(It.IsAny<Size>())).Returns(new Position(5, 6));

      GameEngine engine = new GameEngine(_options);
      engine.Initialize();
      engine.Start();

      Assert.IsNotNull(engine.Map);
      Assert.AreEqual("F7", engine.Man.Position, "Starting on Position E7");

      engine.MoveUp();
      Assert.AreEqual("F6", engine.Man.Position, "Move to F6");

      engine.MoveDown();
      Assert.AreEqual("F7", engine.Man.Position, "Move to F7");

      engine.MoveLeft();
      Assert.AreEqual("E7", engine.Man.Position, "Move to E7");

      engine.MoveRight();
      Assert.AreEqual("F7", engine.Man.Position, "Move to F7");
    }

    [TestMethod]
    public void MoveOutOfRange() {
      _randomizerMock.Setup(_ => _.GetRandomPosition(It.IsAny<Size>())).Returns(new Position(0, 0));
      int called = 0;
      GameEngine engine = new GameEngine(_options);
      engine.WallReachedEvent += (sender, args) => { called++; };
      engine.Initialize();
      engine.Start();
      Assert.IsNotNull(engine.Map);
      Assert.AreEqual("A1", engine.Man.Position, "Start on A1");

      engine.MoveUp();

      Assert.AreEqual("A1", engine.Man.Position, "Did not move from A1");
      Assert.AreEqual(1, called);
    }

  }
}
