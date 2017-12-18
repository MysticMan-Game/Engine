namespace MysticMan.Logic.Internals{
  /// <summary>
  /// Represents a position describe the left and top boundaries
  /// </summary>
  internal class Position {
    public Position() {

    }

    public Position(int left, int top) {
      Left = left;
      Top = top;
    }

    public int Left { get; set; }
    public int Top { get; set; }

  }
}
