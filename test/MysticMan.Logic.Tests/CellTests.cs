using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheMysteryMan.Logic.Tests {
  [TestClass]
  public class CellTests {
    [TestMethod]
    public void TestX0Y0Cell() {

      Assert.AreEqual("A1", new Cell(0, 0).Id);
    }
  }
}
