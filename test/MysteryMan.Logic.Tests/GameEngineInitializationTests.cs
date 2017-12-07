using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheMysteryMan.Logic.Tests {
  [TestClass]
  public class GameEngineInitializationTests {
    [TestMethod]
    public void MapAndManAreInitialized() {
      Func<int, int, int> randomGenerator = (x, y) => 5;
      GameEngine engine = new GameEngine(randomGenerator);
      engine.Run();
      Assert.IsNotNull(engine.Map);
      Assert.AreEqual("A1", engine.Map.GetPosition(0, 0));
      Assert.AreEqual("E5", engine.Map.GetPosition(4, 4));
      Assert.AreEqual("F6", engine.Man.Position);

    }

    [TestMethod]
    public void MoveInsideTheMap() {
      int pos = 5;
      Func<int, int, int> randomGenerator = (x, y) => {
        int current = pos++;
        return current;
      };
      GameEngine engine = new GameEngine(randomGenerator);
      engine.Run();

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
      int called = 0;
      Func<int, int, int> randomGenerator = (x, y) => {
        int current = 0;
        return current;
      };

      GameEngine engine = new GameEngine(randomGenerator);
      engine.WallReachedEvent += (sender, args) => { called++; };

      engine.Run();
      Assert.IsNotNull(engine.Map);
      Assert.AreEqual("A1", engine.Man.Position, "Start on A1");

      engine.MoveUp();

      Assert.AreEqual("A1", engine.Man.Position, "Did not move from A1");
      Assert.AreEqual(1, called);
    }

  }
}
