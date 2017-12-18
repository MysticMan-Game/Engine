namespace MysticMan.Logic.Internals
{
  internal class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public string Id => $"{(char)(X+65)}{Y+1}";
    }
}
