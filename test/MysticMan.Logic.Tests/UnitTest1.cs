using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheMysteryMan.Logic.Tests
{
    [TestClass]
    public class RunTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Func<int, int, int> randomGenerator = (x,y) =>  5;
            GameEngine engine = new GameEngine(randomGenerator);
            engine.Run();
            Assert.IsNotNull(engine.Map);
            Assert.AreEqual("A1", engine.Map.GetPosition(0, 0));
            Assert.AreEqual("E5", engine.Map.GetPosition(4, 4));
            Assert.AreEqual("F6", engine.Man.Position);
            
        }

        [TestMethod]
        public void TestMoves()
        {
            int pos = 5;
            Func<int, int, int> randomGenerator = (x, y) => {
                int current = pos++;
                return current; };
            GameEngine engine = new GameEngine(randomGenerator);
            engine.Run();
            Assert.IsNotNull(engine.Map);
            Assert.AreEqual("F7", engine.Man.Position);

            engine.MoveUp();

            String current2 = engine.Man.Position;
            Map map = engine.Map;

            Assert.AreEqual("F6", engine.Man.Position);
            engine.MoveDown();
            Assert.AreEqual("F7", engine.Man.Position);
            engine.MoveLeft();
            Assert.AreEqual("E7", engine.Man.Position);
            engine.MoveRight();
            Assert.AreEqual("F7", engine.Man.Position);




        }

        [TestMethod]
        public void TestMoveOutOfRange()
        {
            int called = 0;
            Func<int, int, int> randomGenerator = (x, y) => {
                int current =0;
                return current;
            };
            GameEngine engine = new GameEngine(randomGenerator);
            engine.WallReachedEvent += (sender, args) => { called++; };

            engine.Run();
            Assert.IsNotNull(engine.Map);
            Assert.AreEqual("A1", engine.Man.Position);

            engine.MoveUp();

            Assert.AreEqual(1, called);
            
        }

    }
}
