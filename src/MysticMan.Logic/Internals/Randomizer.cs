using System;

namespace MysticMan.Logic.Internals{
  internal class Randomizer : IRandomizer {
    /// <inheritdoc />
    public Position GetRandomPosition(Size size) {
      int left = new Random().Next(0, size.Width);
      int top = new Random().Next(0, size.Height);
      return new Position(left, top);
    }
  }
}
