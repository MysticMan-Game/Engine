namespace TheMysteryMan.Logic
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public string Id
        {
            get
            {

                return $"{(char)(X+65)}{Y+1}";
            }
        }
    }
}