using MysticMan.Logic.Internals;

namespace MysticMan.Logic{
  public class MapSize {
    private readonly Size _size;

    internal MapSize(Size size) {
      _size = size;
    }

    internal Size Size => _size;

    public int Width => _size.Width;

    public int Height => _size.Height;
  }
}
