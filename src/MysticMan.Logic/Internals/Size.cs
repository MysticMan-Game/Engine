namespace MysticMan.Logic.Internals{
  /// <summary>
  /// Represents a dimension of width and height in the Map
  /// </summary>
  internal class Size {
    public Size() {

    }

    public Size(int width, int height) {
      Width = width;
      Height = height;
    }

    public int Width { get; set; }
    public int Height { get; set; }
  }
}
