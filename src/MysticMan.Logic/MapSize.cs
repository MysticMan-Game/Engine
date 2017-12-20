using MysticMan.Logic.Internals;

namespace MysticMan.Logic{
  public class MapSize {
    private readonly Size _size;

    public static MapSize New5x5() {
      return new MapSize(new Size(5, 5));
    }


    internal MapSize(Size size) {
      _size = size;
    }

    internal Size Size => _size;

    public int Width => _size.Width;

    public int Height => _size.Height;
  }
}
